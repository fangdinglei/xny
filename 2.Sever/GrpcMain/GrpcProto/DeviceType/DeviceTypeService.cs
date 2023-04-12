using Grpc.Core;
using GrpcMain.Attributes;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
using static GrpcMain.DeviceType.DTODefine.Types;

namespace GrpcMain.DeviceType
{
    static public class Ext
    {
        static public MyDBContext.Main.ThingModel AsDBObj(this GrpcMain.DeviceType.ThingModel model, long typeid, long usertreeid)
        {
            return new MyDBContext.Main.ThingModel
            {
                Id=model.Id,
                Abandonted = model.Abandonted,
                AlertHighValue = model.AlertHighValue,
                AlertLowValue = model.AlertLowValue,
                DeviceTypeId = typeid,
                MaxValue = model.MaxValue,
                MinValue = model.MinValue,
                Name = model.Name,
                Remark = model.Remark,
                Unit = model.Unit,
                UserTreeId = usertreeid,
                Type = (byte)model.ValueType,
            };
        }
        /// <summary>
        /// 将请求对象转换为新的DB对象
        /// </summary>
        /// <returns></returns>
        static public MyDBContext.Main.Device_Type AsDBObj(this GrpcMain.DeviceType.TypeInfo type, User creator)
        {
            var res = new Device_Type()
            {
                CreatorId = creator.Id,
                Name = type.Name,
                Script = type.Script,
                UserTreeId = creator.UserTreeId,
            };
            return res;
        }
    }

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
                User us = (User)context.UserState["user"];
                if (request.Ids.Count == 0)
                {//获取全部
                    int maxcount = MaxType * 2 + 1;
                    var ls = await ct.Device_Types.GetEntityOfAccessible(ct, us, maxcount, request.Cursor,
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
                                AlertHighValue = it.AlertHighValue,
                                AlertLowValue = it.AlertLowValue,
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
                                AlertHighValue = it.AlertHighValue,
                                AlertLowValue = it.AlertLowValue,
                            }));
                            return res;
                        }));
                        return res;
                    }

                }
                else
                {//获取部分
                    var ls = await ct.Device_Types.GetEntityOfAccessible(ct, us, -1, request.Cursor,
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
                            AlertHighValue = it.AlertHighValue,
                            AlertLowValue = it.AlertLowValue,
                            ValueType = (int)it.Type,
                        }));
                        return res;
                    }));
                    return res;
                }

            }
        }

        [MyGrpcMethod( "devicetype:save",NeedDB =true, NeedTransaction=true)]
        public override async Task<CommonResponse?> UpdateTypeInfo(Request_UpdateTypeInfo request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            User us = (User)context.UserState["user"];
            var ct=(MainContext)context.UserState[nameof(MainContext)];
            var type = await ct.Device_Types
                .Where(it => it.Id == request.Info.Id).Include(it => it.ThingModels)
                .FirstOrDefaultAsync();
            if (type == null
                || type.UserTreeId != us.UserTreeId)
            {
                //没有权限
                return new CommonResponse()
                {
                    Success = false,
                    Message = "没有该实例的权限",
                };
            }

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
                it => it.AsDBObj(request.Info.Id, type.UserTreeId)).ToList())
            {
                type.ThingModels.Add(item);
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

        public override async Task<Response_AddTypeInfo> AddTypeInfo(Request_AddTypeInfo request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            User us = (User)context.UserState["user"];
            try
            {
                using (MainContext ct = new MainContext())
                {
                    using (var trans = await ct.Database.BeginTransactionAsync())
                    {
                        //校验设备类型数量
                        var typecount = await ct.Device_Types.Where(it => it.UserTreeId == us.UserTreeId).CountAsync();
                        if (typecount > MaxType)
                        {
                            return new Response_AddTypeInfo()
                            {
                                Status = new CommonResponse()
                                {
                                    Success = false,
                                    Message = $"设备类型只能有{MaxType}个",
                                }
                            };
                        }

                        var obj = request.Info.AsDBObj(us);
                        ct.Add(obj);
                        await ct.SaveChangesAsync();
                        var ls = request.Info.ThingModels.Select(it => it.AsDBObj(obj.Id, us.UserTreeId))
                            .ToList();
                        ct.AddRange(ls);
                        await ct.SaveChangesAsync();
                        await trans.CommitAsync();
                    }
                }
                return new Response_AddTypeInfo()
                {
                    Status = new CommonResponse()
                    {
                        Success = true,
                        Message = "",
                    }
                };
            }
            catch (Exception ex)
            {
                return new Response_AddTypeInfo()
                {
                    Status = new CommonResponse()
                    {
                        Success = false,
                        Message = ex.Message,
                    }
                };
            }

        }

    }
}
