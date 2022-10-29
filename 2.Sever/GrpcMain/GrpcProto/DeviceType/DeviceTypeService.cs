using Grpc.Core;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
using static GrpcMain.DeviceType.DTODefine.Types;

namespace GrpcMain.DeviceType
{
    /// <summary>
    /// TODO 审计
    /// </summary>
    public class DeviceTypeServiceImp : DeviceTypeService.DeviceTypeServiceBase
    {
        public const int MaxType = 3000;

        ITimeUtility _timeutility;
        public DeviceTypeServiceImp(ITimeUtility time)
        {
            _timeutility = time;
        }


        public override async Task<Response_GetTypeInfos> GetTypeInfos(Request_GetTypeInfos request, ServerCallContext context)
        {
            var res = new Response_GetTypeInfos();

            using (MainContext ct = new MainContext())
            {
                long id = (long)context.UserState["CreatorId"];
                if (request.Ids.Count == 0)
                {//获取全部
                    int maxcount = MaxType * 2 + 1;
                    var ls = await ct.Device_Types.GetEntityOfAccessible(ct, id, maxcount, request.Cursor,
                        true, true, false, filter: (it) => it.Include(it => it.ThingModels));
                    if (ls.Count == maxcount)
                    {
                        res.Cursor = ls.Last().Id;
                        res.TypeInfos.AddRange(ls.Take(ls.Count - 1).Select(it =>
                        {
                            var res = new TypeInfo
                            {
                                Id = it.Id,
                                Name = it.Name,
                                Script = it.Script,
                            };
                            res.ThingModels.AddRange(it.ThingModels.Select(it => new ThingModel
                            {
                                Id = it.Id,
                                MaxValue = it.MaxValue,
                                MinValue = it.MinValue,
                                Name = it.Name,
                                Remark = it.Remark,
                                Unit = it.Unit,
                                ValueType = (int)it.Type,
                                AlertHighValue=it.AlertHighValue,
                                AlertLowValue=it.AlertLowValue,
                            }));
                            return res;
                        }));
                        return res;
                    }
                    else
                    {
                        res.Cursor = 0;
                        res.TypeInfos.AddRange(ls.Select(it =>
                        {
                            var res = new TypeInfo
                            {
                                Id = it.Id,
                                Name = it.Name,
                                Script = it.Script,
                            };
                            res.ThingModels.AddRange(it.ThingModels.Select(it => new ThingModel
                            {
                                Id = it.Id,
                                MaxValue = it.MaxValue,
                                MinValue = it.MinValue,
                                Name = it.Name,
                                Remark = it.Remark,
                                Unit = it.Unit,
                                ValueType = (int)it.Type,
                            }));
                            return res;
                        }));
                        return res;
                    }

                }
                else
                {//获取部分
                    var ls = await ct.Device_Types.GetEntityOfAccessible(ct, id, -1, request.Cursor,
                        true, true, false, request.Ids, filter: (it) => it.Include(it => it.ThingModels));
                    res.Cursor = 0;
                    res.TypeInfos.AddRange(ls.Select(it =>
                    {
                        var res = new TypeInfo
                        {
                            Id = it.Id,
                            Name = it.Name,
                            Script = it.Script,
                        };
                        res.ThingModels.AddRange(it.ThingModels.Select(it => new ThingModel
                        {
                            Id = it.Id,
                            MaxValue = it.MaxValue,
                            MinValue = it.MinValue,
                            Name = it.Name,
                            Remark = it.Remark,
                            Unit = it.Unit,
                            ValueType = (int)it.Type,
                        }));
                        return res;
                    }));
                    return res;
                }

            }
        }

        [GrpcRequireAuthority(true, "UpdateDeviceTypeInfo")]
        public override async Task<CommonResponse?> UpdateTypeInfo(Request_UpdateTypeInfo request, ServerCallContext context)
        {//需要审计
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                var type = await ct.Device_Types
                    .Where(it => it.Id == request.Info.Id).Include(it => it.ThingModels)
                    .FirstOrDefaultAsync();
                if (type == null)
                {
                    //没有权限
                    context.Status = new Status(StatusCode.PermissionDenied, "");
                    return null;
                }

                var ot = await type.GetOwnerTypeAsync(ct, id);
                if (ot == AuthorityUtility.OwnerType.Non)
                {
                    return new CommonResponse()
                    {
                        Success = false,
                        Message = "没有该实例的权限",
                    };
                }
                else if (ot == AuthorityUtility.OwnerType.Creator
                    || ot == AuthorityUtility.OwnerType.FatherOfCreator)
                {
                    //自己访问或者父用户访问 不要审计
                    if (type.ThingModels.Count > 0)
                    {
                        //只能插入 不能删除
                        var old = type.ThingModels.Select(it => it.Id).ToList();
                        var _new = request.Info.ThingModels.Select(it => it.Id).ToList();
                        if (old.Except(_new).Count() != 0)
                        {
                            return new CommonResponse()
                            {
                                Success = false,
                                Message = "物模型只能插入不能删除",
                            };
                        }

                        //todo 修改变成添加
                        type.ThingModels.Clear();
                        foreach (var item in
                            request.Info.ThingModels.Select(
                            it => new MyDBContext.Main.ThingModel
                            {
                                Abandonted = it.Abandonted,
                                DeviceTypeId = type.Id,
                                Id = old.Contains(it.Id) ? it.Id : 0,
                                MaxValue = it.MaxValue,
                                MinValue = it.MinValue,
                                Name = it.Name,
                                Remark = it.Remark,
                                Type = (ThingModelValueType)it.ValueType,
                                Unit = it.Unit,
                                AlertHighValue = it.AlertHighValue,
                                AlertLowValue = it.AlertLowValue,
                            }))
                        {
                            type.ThingModels.Add(item);
                        }

                    }
                    if (request.Info.HasName)
                    {
                        type.Name = request.Info.Name;
                    }
                    if (request.Info.HasScript)
                    {
                        type.Script = request.Info.Script;
                    }
                    await ct.SaveChangesAsync();
                    return new CommonResponse()
                    {
                        Success = true,
                    };
                }
                else if (ot == AuthorityUtility.OwnerType.SonOfCreator)
                {
                    //自己是子用户 要审计 
                    context.Status = Status.DefaultCancelled;
                    return new CommonResponse()
                    {
                        Success = true,
                        Message = "提交完成,等待审计",
                    };
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public override Task<Response_AddTypeInfo> AddTypeInfo(Request_AddTypeInfo request, ServerCallContext context)
        {//todo 鉴定数量
            throw new Exception();
        }

    }
}
