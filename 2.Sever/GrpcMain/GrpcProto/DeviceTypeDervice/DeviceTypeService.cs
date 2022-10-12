using Grpc.Core;
using GrpcMain.Common;
using GrpcMain.DeviceType;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
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
                if (request.Ids.Count==0)
                {//获取全部
                    int maxcount = 1000 + 1;
                    var ls=await ct.Device_Types.Join(ct.User_SFs,dt=>dt.CreaterId,us=>us.FatherId,
                        (dt,us)=>new {us.FatherId,dt })
                        .Where(it =>it.FatherId==it.dt.CreaterId&&it.dt.Id>request.Cursor)
                        .Take(maxcount).AsNoTracking().ToListAsync();
                    if (ls.Count==maxcount)
                    {
                        res.Cursor = ls.Last().dt.Id;
                        res.TypeInfos.AddRange(ls.Take(ls.Count - 1).Select(it => {
                            return new DeviceTypeTypes.Types.TypeInfo
                            {
                                 DataPoints = it.dt.DataPoints,
                                 Id = it.dt.Id,
                                 Name = it.dt.Name,
                                 Script = it.dt.Script,
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
                                DataPoints = it.dt.DataPoints,
                                Id = it.dt.Id,
                                Name = it.dt.Name,
                                Script = it.dt.Script,
                            };
                        }));
                        return res;
                    }
                    
                }
                else
                {//获取部分
                    var ls = await ct.Device_Types.Join(ct.User_SFs, dt => dt.CreaterId, us => us.FatherId,
                        (dt, us) => new { us.FatherId, dt })
                        .Where(it => it.FatherId == it.dt.CreaterId && request.Ids.Contains(it.dt.Id))
                    .AsNoTracking().ToListAsync();

                    res.Cursor = 0;
                    res.TypeInfos.AddRange(ls.Select(it => {
                        return new DeviceTypeTypes.Types.TypeInfo
                        {
                            DataPoints = it.dt.DataPoints,
                            Id = it.dt.Id,
                            Name = it.dt.Name,
                            Script = it.dt.Script,
                        };
                    }));
                    return res;
                }
              
            } 
        }

        [GrpcRequireAuthority(true,"")]
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
                var sf = await ct.User_SFs
                  .Where(it => it.SonId==id&&it.FatherId==type.CreaterId
                  || it.FatherId == id && it.SonId == type.CreaterId)
                   .AsNoTracking().FirstOrDefaultAsync();
                if (sf == null)
                {
                    //没有权限
                    context.Status = new Status(StatusCode.PermissionDenied, "");
                    return null;
                }


                if (sf.SonId == sf.FatherId
                    || sf.FatherId == id)
                {//自己访问或者父用户访问 不要审计
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
                        type. Script = request.Info. Script;
                    }
                    await ct.SaveChangesAsync();
                    return new CommonResponse()
                    {
                        Success = true,
                        Message = null,
                    };
                }
                else if (sf.SonId == id)
                {//自己是子用户 要审计
                    //TODO 审计
                    context.Status= Status.DefaultCancelled;
                    return new CommonResponse()
                    {
                        Success = true,
                        Message = "提交完成,等待审计",
                    };
                } 
                else
                {
                    throw new Exception("错误的分支路径");
                }
            }
        }


    }
}
