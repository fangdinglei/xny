using Grpc.Core;
using GrpcMain.Common;
using GrpcMain.DeviceType;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
using PgGrpcMain;
using System.Reflection;
using static GrpcMain.DeviceType.DeviceTypeTypes.Types;

namespace GrpcMain.DeviceType
{
    /// <summary>
    /// TODO 审计
    /// </summary>
    public class DeviceTypeServiceImp :  DeviceTypeService.DeviceTypeServiceBase
    { 
        ITimeUtility _timeutility;
        public DeviceTypeServiceImp(  ITimeUtility time)
        { 
            _timeutility = time;
        }

        [GrpcRequireAuthority]
        public override async Task<Response_GetTypeInfos> GetTypeInfos( Request_GetTypeInfos request, ServerCallContext context)
        {
            var res = new Response_GetTypeInfos();
          
            using (MainContext ct=new MainContext())
            {
                long id = (long)context.UserState["UserId"];
                if (request.Ids.Count==0)
                {//获取全部
                    int maxcount = 1000 + 1;
                    var ls = await ct.Device_Types.GetEntityOfAccessible(ct, id, maxcount, request.Cursor, true, true, false);
                    if (ls.Count==maxcount)
                    {
                        res.Cursor = ls.Last() .Id;
                        res.TypeInfos.AddRange(ls.Take(ls.Count - 1).Select(it => {
                            return new DeviceTypeTypes.Types.TypeInfo
                            {
                                 DataPoints = it .DataPoints,
                                 Id = it .Id,
                                 Name = it .Name,
                                 Script = it .Script,
                            };
                        }) );
                        return res;
                    }
                    else
                    {
                        res.Cursor = 0;
                        res.TypeInfos.AddRange(ls.Select(it => {
                            return new DeviceTypeTypes.Types.TypeInfo
                            {
                                DataPoints = it. DataPoints,
                                Id = it. Id,
                                Name = it. Name,
                                Script = it. Script,
                            };
                        }));
                        return res;
                    }
                    
                }
                else
                {//获取部分
                    var ls = await ct.Device_Types.GetEntityOfAccessible(ct, id, -1, request.Cursor, true, true, false,request.Ids); 
                    res.Cursor = 0;
                    res.TypeInfos.AddRange(ls.Select(it => {
                        return new DeviceTypeTypes.Types.TypeInfo
                        {
                            DataPoints = it. DataPoints,
                            Id = it. Id,
                            Name = it. Name,
                            Script = it. Script,
                        };
                    }));
                    return res;
                }
              
            } 
        }

        [GrpcRequireAuthority(true, "UpdateDeviceTypeInfo")]
        public override async Task<CommonResponse?> UpdateTypeInfo(Request_UpdateTypeInfo request, ServerCallContext context)
        {//需要审计
            long id = (long)context.UserState["UserId"];
            using (MainContext ct = new MainContext())
            {
                var type = await ct.Device_Types 
                    .Where(it => it.Id == request.Info.Id)
                     .AsNoTracking().FirstOrDefaultAsync();
                if (type == null)
                {
                    //没有权限
                    context.Status = new Status(StatusCode.PermissionDenied, "");
                    return null;
                }

                var ot=await type.GetOwnerTypeAsync(ct, id); 
                if (ot== AuthorityUtility.OwnerType.Non )
                {
                    //没有权限
                    context.Status = new Status(StatusCode.PermissionDenied, "");
                    return null;
                }
                else if (ot== AuthorityUtility.OwnerType.Creator
                    ||ot== AuthorityUtility.OwnerType.FatherOfCreator)
                {
                    //自己访问或者父用户访问 不要审计
                    if (request.Info.HasDataPoints)
                    {
                        type.DataPoints = request.Info.DataPoints;
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
                        Message = null,
                    };
                }
                else if ( ot== AuthorityUtility.OwnerType.SonOfCreator)
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


    }
}
