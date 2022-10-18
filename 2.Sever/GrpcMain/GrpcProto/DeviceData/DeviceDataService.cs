using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
using static GrpcMain.DeviceData.DTODefine.Types;

namespace GrpcMain.DeviceData
{
    public class DeviceDataServiceImp : DeviceDataService.DeviceDataServiceBase
    {
        ITimeUtility _timeutility;
        public DeviceDataServiceImp(ITimeUtility time)
        {
            _timeutility = time;
        }


        public override async Task<Response_GetDataPoints?> GetDataPoints(Request_GetDataPoints request, ServerCallContext context)
        {
            int maxcount = 1000;
            if (request.HasMaxCount)
                maxcount = request.MaxCount;
            long id = (long)context.UserState["CreatorId"];
            Response_GetDataPoints res = new Response_GetDataPoints();
            using (MainContext ct = new MainContext())
            {
                var ud = await ct.User_Devices
                    .Where(it => it.DeviceId == request.Dvid && it.UserId == id)
                     .AsNoTracking().FirstOrDefaultAsync();
                if (ud == null || !ud.PData)
                {
                    //没有权限
                    context.Status = new Status(StatusCode.PermissionDenied, "没有数据读取权限");
                    return null;
                }

                var bd = ct.Device_DataPoints.Where(it => it.DeviceId == request.Dvid);

                if (request.HasCursor)
                    bd = bd.Where(it => it.Id >= request.Cursor);
                if (request.HasStarttime)
                    bd = bd.Where(it => it.Time >= request.Starttime);
                if (request.HasEndtime)
                    bd = bd.Where(it => it.Time < request.Endtime);
                bd = bd.Take(maxcount);
                bd = bd.AsNoTracking();
                var ls = await bd.ToListAsync();
                IEnumerable<Device_DataPoint> lsx;
                if (maxcount == ls.Count)
                {
                    res.Cursor = ls.Last().Id;
                    lsx = ls.Take(maxcount - 1);
                }

                else
                {
                    res.Cursor = 0;
                    lsx = ls;
                }

                Dictionary<long, List<DataPoinet>> points = new();
                foreach (var item in lsx)
                {
                    if (!points.ContainsKey(item.StreamId))
                    {
                        points[item.StreamId] = new();
                    }
                    points[item.StreamId].Add(new DataPoinet()
                    {
                        Dvid = item.DeviceId,
                        Time = item.Time,
                        Value = item.Value,
                    });
                }
                foreach (var item in points)
                {
                    var dp = new DataStream()
                    {
                        StreamId = item.Key
                    };
                    dp.Points.AddRange(item.Value);
                    dp.StreamId = item.Key;
                    dp.StreamName = "TODO";
                    res.Streams.Add(dp);
                }
            }
            return res;
        }


        public override async Task<Response_GetLatestData> GetLatestData(Request_GetLatestData request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            Response_GetLatestData res = new Response_GetLatestData();
            using (MainContext ct = new MainContext())
            {
                var ud = await ct.User_Devices
                    .Where(it => it.DeviceId == request.Dvid && it.UserId == id)
                     .AsNoTracking().FirstOrDefaultAsync();
                if (ud == null || !ud.PStatus)
                {
                    //没有权限
                    context.Status = new Status(StatusCode.PermissionDenied, "没有数据读取权限");
                    return null;
                }

                var dt = await ct.Devices.Where(it => it.Id == request.Dvid)
                    .Select(it => it.LatestData)
                    .FirstOrDefaultAsync();
                return new Response_GetLatestData()
                {
                    Dvid = request.Dvid,
                    Streams = dt
                };
            }
            return res;
        }
    }
}
