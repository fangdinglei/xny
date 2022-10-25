using Grpc.Core;
using GrpcMain.Common;
using GrpcMain.DeviceData.Cold;
using GrpcMain.Managers;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
using static GrpcMain.Managers.ColdDataManagerBase;

namespace GrpcMain.DeviceData
{
    public class ColdDataServiceImp : ColdDataService.ColdDataServiceBase
    {
        record A(long Id, long DeviceId, long StreamId);
        ITimeUtility _timeutility;
        IGrpcCursorUtility _grpcCursorUtility;
        public ColdDataServiceImp(ITimeUtility time, IGrpcCursorUtility grpcCursorUtility)
        {
            _timeutility = time;
            _grpcCursorUtility = grpcCursorUtility;
        }


        async Task<List<Device_DataPoint_Cold>> CompressDeviceData(int maxcout, IEnumerable<A> data, IQueryable<Device_DataPoint> selector)
        {
            List<Device_DataPoint_Cold> res = new List<Device_DataPoint_Cold>();
            foreach (var item in data)
            {
                long cursor = 0;
                do
                {
                    var bd = selector.Where(it => it.Id >= cursor && item.DeviceId == it.DeviceId && item.StreamId == it.StreamId);
                    bd = bd.AsNoTracking().Take(maxcout);
                    cursor = 0;
                    var ls = _grpcCursorUtility.Run(await bd.ToListAsync(), maxcout, (it) => cursor = it.Id);
                    if (ls.Count() == 0)
                        continue;
                    Device_DataPoint_Cold cold = new Device_DataPoint_Cold()
                    {
                        Count = ls.Count(),
                        CreatTime = _timeutility.GetTicket(),
                        StartTime = ls.First().Time,
                        EndTime = ls.Last().Time,
                        DeviceId = item.DeviceId,
                        StreamId = item.StreamId,
                    };
                    using MemoryStream ms = new MemoryStream(ls.Count() * (4 + 4));
                    using BinaryWriter binaryWriter = new BinaryWriter(ms);
                    foreach (var point in ls)
                    {
                        binaryWriter.Write((int)(point.Time - cold.CreatTime));
                        binaryWriter.Write(point.Value);
                    }
                    cold.Data = ms.GetBuffer();
                    res.Add(cold);

                } while (cursor > 0);

            }
            return res;
        }
        [GrpcRequireAuthority("SystemUser")]
        public override async Task<CommonResponse> CompressDeviceData(Request_CompressDeviceData request, ServerCallContext context)
        {
            int maxcout1 = 100 + 1;
            if (!request.HasMaxCountOneTime ||
                request.MaxCountOneTime == 0L)
            {
                request.MaxCountOneTime = 2000;
            }
            using (MainContext ct = new MainContext())
            {
                long cursor1 = 0;
                IQueryable<Device_DataPoint> bd = ct.Device_DataPoints;
                //按时间筛选
                bd = bd.Where(it => it.Time >= request.StartTime && it.Time < request.EndTime);
                bd = bd.OrderBy(it => it.Time);
                var bd2 = bd.Select(it => new A(it.Id, it.DeviceId, it.StreamId));
                //分批获取设备和数据点信息 并处理
                do
                {
                    var bdx = bd2.Where(it => it.Id >= cursor1);
                    //先选择
                    var ls = await bdx.AsNoTracking().Take(maxcout1).ToListAsync();
                    cursor1 = 0;
                    var newlist = _grpcCursorUtility.Run(ls, maxcout1, (it) => cursor1 = it.Id);

                    List<Device_DataPoint_Cold> reses = await CompressDeviceData(request.MaxCountOneTime + 1, newlist, bd);
                    foreach (var item in newlist)
                    {
                        var all = reses.Where(it => it.DeviceId == item.Id && it.StreamId == item.Id);
                        //进行压缩
                        foreach (var colditem in all)
                        {
                            colditem.ManagerName = "TODO";
                            await ColdDataManagerBase.DoStore(colditem);
                            //TODO 执行压缩 添加冷数据条目
                            ct.Add(colditem);
                            await ct.SaveChangesAsync();
                        }
                        if (all.Count() != 0)
                        {
                            //删除数据点
                            await ct.DeleteRangeAsync<Device_DataPoint>(
                                it => it.Time >= request.StartTime && it.Time < request.EndTime &&
                              it.DeviceId == item.DeviceId && it.StreamId == item.StreamId
                            );
                        }
                    }
                } while (cursor1 > 0);
                return new CommonResponse()
                {
                    Success = true,
                };
            }
        }

        public async Task<List<(long, double)>> DeCompressDeviceData(long starttime, long endtime, long deviceid, long streamid)
        {
            List<(long, double)> res = new List<(long, double)>();
            using (MainContext ct = new MainContext())
            {
                //时间有交叉部分
                var colds = await ct.Device_DataPoint_Colds.Where(it => it.StartTime > endtime && it.EndTime <= starttime
                     && it.DeviceId == deviceid && it.StreamId == streamid
                 ).OrderBy(it => it.StartTime).AsNoTracking().ToListAsync();
                foreach (var cold in colds)
                {
                    var bytes = await DoLoad(cold);
                    var br = new BinaryReader(new MemoryStream(bytes));
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        var t = br.ReadInt32();
                        var v = br.ReadSingle();
                        if (t >= starttime && t < endtime)
                        {
                            res.Add((starttime + t, v));
                        }
                    }
                }
            }
            return res;
        }
    }
}
