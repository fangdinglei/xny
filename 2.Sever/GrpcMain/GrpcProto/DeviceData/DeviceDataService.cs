using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;

namespace GrpcMain.DeviceData
{
    public class DeviceDataServiceImp : DeviceDataService.DeviceDataServiceBase
    {
        ITimeUtility _timeutility;
        IDeviceColdDataHandle _deviceColdDataHandle;
        public DeviceDataServiceImp(ITimeUtility time, IDeviceColdDataHandle deviceColdDataHandle)
        {
            _timeutility = time;
            _deviceColdDataHandle = deviceColdDataHandle;
        }


        public override async Task<Response_GetDataPoints?> GetDataPoints(Request_GetDataPoints request, ServerCallContext context)
        {
            if (request.ColdData)
            {
                context.Status = new Status(StatusCode.PermissionDenied, nameof(request.ColdData) + "=true不应当使用该接口");
                return null;
            }

            int maxcount = 1000 + 1;
            if (request.HasMaxCount)
                maxcount = request.MaxCount + 1;
            long id = (long)context.UserState["CreatorId"];
            Response_GetDataPoints res = new Response_GetDataPoints()
            {
                Dvid = request.Dvid,
                Stream = new DataStream
                {
                    StreamId = request.StreamId,
                }
            };
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
                bd = bd.Where(it => it.StreamId == request.StreamId);
                if (request.HasCursor)
                    bd = bd.Where(it => it.Id >= request.Cursor);
                if (request.HasStarttime)
                    bd = bd.Where(it => it.Time >= request.Starttime);
                if (request.HasEndtime)
                    bd = bd.Where(it => it.Time < request.Endtime);
                bd = bd.Take(maxcount);
                bd = bd.AsNoTracking().OrderBy(it => it.Time);
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
                res.Stream.Points.Add(lsx.Select(it => new DataPoinet { Time = it.Time, Value = it.Value }));
            }
            return res;
        }
        [GrpcRequireAuthority("ColdData")]
        public override async Task<Response_GetDataPoints> GetColdDataPoints(Request_GetDataPoints request, ServerCallContext context)
        {
            if (!request.ColdData)
            {
                context.Status = new Status(StatusCode.PermissionDenied, nameof(request.ColdData) + "=false不应当使用该接口");
                return null;
            }

            int maxcount = 1000 + 1;
            if (request.HasMaxCount)
                maxcount = request.MaxCount + 1;
            long id = (long)context.UserState["CreatorId"];
            Response_GetDataPoints res = new Response_GetDataPoints()
            {
                Dvid = request.Dvid,
                Stream = new DataStream
                {
                    StreamId = request.StreamId,
                }
            };
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

                //不能使用冷数据 返回空集合
                if (!_deviceColdDataHandle.UsingColdData)
                {
                    return res;
                }
                var cursor = request.Cursor;
                var r = await _deviceColdDataHandle.DeCompressDeviceData(request.Starttime, request.Endtime, request.Dvid, request.StreamId, ref cursor, maxcount);
                res.Stream.Points.Add(r.Select(it => new DataPoinet { Time = it.Item1, Value = it.Item2 }));
                if (cursor > 0)
                {
                    request.Cursor = cursor;
                    request.ColdData = true;
                }
                else
                {
                    request.Cursor = 0;
                    request.ColdData = false;
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
