using Grpc.Core;
using GrpcMain.Common;
using GrpcMain.DeviceData;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
using static GrpcMain.DeviceData.DeviceDataTypes.Types;

namespace GrpcMain.DeviceData
{
    public class DeviceDataServiceImp :  DeviceDataService.DeviceDataServiceBase
    { 
        ITimeUtility _timeutility;
        public DeviceDataServiceImp(  ITimeUtility time)
        { 
            _timeutility = time;
        }

        [GrpcRequireAuthority]
        public override async Task<Response_GetDataPoints?> GetDataPoints( Request_GetDataPoints request, ServerCallContext context)
        {
            int maxcount = 1000; 
            if (request.HasMaxCount)
                maxcount = request.MaxCount;
            long id = (long)context.UserState["CreatorId"];
            Response_GetDataPoints res = new Response_GetDataPoints();
            using (MainContext ct = new MainContext())
            {
                var ud = await ct.User_Devices
                    .Where(it => it.DeviceId == request.Dvid&&it.UserId==id)
                     .AsNoTracking().FirstOrDefaultAsync();
                if (ud  == null||!ud.PData)
                {
                    //没有权限
                    context.Status = new Status(StatusCode.PermissionDenied, "没有数据读取权限");
                    return null;
                }

                var bd= ct.Device_DataPoints.Where(it => it.DeviceId == request.Dvid);
              
                if (request.HasCursor)
                    bd = bd.Where(it => it.Id >= request.Cursor);
                if (request.HasStarttime)
                    bd = bd.Where(it => it.Time >=request. Starttime);
                if (request.HasEndtime)
                    bd = bd.Where(it => it.Time < request.Endtime); 
                bd = bd.Take(maxcount);
                bd = bd.AsNoTracking();
                var ls = await bd.ToListAsync();
                if (maxcount == ls.Count)
                    res.Cursor = ls.Last().Id;
                else
                    res.Cursor = 0; 
                Dictionary<long, List<DataPoinet>> points = new(); 
                foreach (var item in ls)
                {
                    if (!points.ContainsKey(item.StreamId))
                    {
                        points[item.StreamId] = new();
                    }
                    points[item.StreamId].Add(new DataPoinet() { 
                        Dvid = item.DeviceId,
                        Time = item.Time,
                        Value=item.Value,
                    }); 
                }
                foreach (var item in points)
                {
                    var dp = new DataStream()
                    {
                        StreamId = item.Key
                    };
                    dp.Points.AddRange( item.Value );
                    dp.StreamId = item.Key;
                    dp.StreamName = "TODO";
                    res.Streams.Add(dp);
                } 
            }
            return res;
        }

        [GrpcRequireAuthority]
        public override Task< Response_GetLatestData> GetLatestData( Request_GetLatestData request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }
    }
}
