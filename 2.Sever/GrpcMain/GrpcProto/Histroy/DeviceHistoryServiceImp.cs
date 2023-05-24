using Grpc.Core;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;

namespace GrpcMain.History
{
    public class DeviceHistoryServiceImp : DeviceHistoryService.DeviceHistoryServiceBase
    {
        IGrpcAuthorityHandle _handle;
        ITimeUtility _timeutility;
        public DeviceHistoryServiceImp(IGrpcAuthorityHandle handle, ITimeUtility time)
        {
            _handle = handle;
            _timeutility = time;
        }

        public override async Task<Response_GetHistory?> GetHistory(Request_GetHistory request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            Response_GetHistory res = new Response_GetHistory();
            res.Status = new CommonResponse()
            {
                Success = true,
            };
            using (MainContext ct = new MainContext())
            {
                if (!request.HasDeviceId)
                {
                    throw new Exception("需要指定设备ID");
                }
                var ud=await  ct.User_Devices.Where(it => it.UserId == id && it.DeviceId == request.DeviceId).AsNoTracking().FirstOrDefaultAsync();
                if (ud == null || (ud.Authority &(int)UserDeviceAuthority.Read_Cmd) == 0)
                {
                    throw new RpcException(new Status( StatusCode.PermissionDenied,"权限不足"));
                }

               var query= ct.DeviceHistorys.Where(it => it.DeviceId == request.DeviceId);
               
                if (request.HasStartTime)
                {
                    query = query.Where(it => it.Time >= request.StartTime);
                }
                if (request.HasEndTime)
                {
                    query = query.Where(it => it.Time < request.EndTime);
                }
                if (request.HasMaxCount)
                {
                    query = query.Take(request.MaxCount);
                }
                else
                {
                    query = query.Take(400);
                }
                query = query.AsNoTracking();
                res.Historys.Add((await query.ToListAsync()).Select(it =>
                {
                    return new DeviceHistory
                    {
                        Data = it.Data,
                        Id = it.Id,
                        Success = it.Success,
                        Time = it.Time,
                        Type = it.Type,
                        DeviceId=it.DeviceId,
                    };
                }));
                return res;
            }
        }


        public override async Task<CommonResponse> DeletHistory(Request_DeletHistory request, ServerCallContext context)
        {
            throw new Exception("敬请期待");
            //long id = (long)context.UserState["CreatorId"];
            //using (MainContext ct = new MainContext())
            //{
            //    if (request.Id == id)
            //    {
            //        return new CommonResponse()
            //        {
            //            Message = "无法删除自己的日志",
            //            Success = false,
            //        };
            //    }
            //    var history = await ct.DeviceHistorys.Where(it => it.Id == request.Id).FirstOrDefaultAsync();
            //    if (history == null)
            //    {
            //        return new CommonResponse()
            //        {
            //            Message = "不存在",
            //            Success = false,
            //        };
            //    }

            //    var sf = await ct.User_SFs.Where(it => it.User1Id == id && it.User2Id == history.CreatorId && it.IsFather)
            //      .AsNoTracking().FirstOrDefaultAsync();
            //    if (sf == null)
            //    {
            //        return new CommonResponse()
            //        {
            //            Message = "只能删除自己的子用户的日志",
            //            Success = false,
            //        };
            //    }
            //    ct.Remove(history);
            //    await ct.SaveChangesAsync();
            //    return new CommonResponse
            //    {
            //        Success = true,
            //    };
            //}
        }

        //public override async Task<CommonResponse> DeletHistorys(Request_DeletHistorys request, ServerCallContext context)
        //{
        //    long id = (long)context.UserState["CreatorId"];
        //    using (MainContext ct = new MainContext())
        //    {
        //        if (request.UserId == id)
        //        {
        //            return new CommonResponse()
        //            {
        //                Message = "无法删除自己的日志",
        //                Success = false,
        //            };
        //        }
        //        var sf = await ct.User_SFs.Where(it => it.User1Id == id && it.User2Id == request.UserId && it.IsFather)
        //        .AsNoTracking().FirstOrDefaultAsync();
        //        if (sf == null)
        //        {
        //            return new CommonResponse()
        //            {
        //                Message = "只能删除自己的子用户的日志",
        //                Success = false,
        //            };
        //        }

        //        await ct.DeleteRangeAsync<MyDBContext.Main.AccountHistory>(it =>
        //        it.CreatorId == request.UserId && it.Time >= request.StartTime && it.Time < request.EndTime);
        //        return new CommonResponse
        //        {
        //            Success = true,
        //        };
        //    }
        //}

    }

}
