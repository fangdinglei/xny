using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;

namespace Sever.ColdData.Imp
{

    public class DeviceColdDataServiceImp : IDeviceColdDataService
    {
        public bool UsingColdData => true;
        IDeviceColdDataHandleManager mgr = new DeviceColdDataHandleManagerImp();

        public async Task<List<(long, float)>> DeCompressDeviceData(long starttime, long endtime, long deviceid, long streamid, long Cursor, int count, Action<long> setcursor)
        {
            List<(long, float)> res = new List<(long, float)>();
            using (var ct = new MainContext())
            {
                var cur = Cursor;
                var cds = await ct.Device_DataPoint_Colds.AsNoTracking().Where(it => it.Id >= cur && it.status != 4 && it.StartTime <= endtime && it.EndTime > starttime
                && it.DeviceId == deviceid && it.StreamId == streamid).OrderBy(it => it.StartTime).Take(count + 1).ToListAsync();
                if (cds.Count == count + 1)
                    setcursor(cds.Last().Id);
                else
                    setcursor(0);
                foreach (var cd in cds)
                {
                    byte[] bytes = await mgr.DoLoad(cd);
                    if (bytes == null)
                        throw new Exception("冷数据加载失败");
                    if (bytes.Length != 8 * cd.Count)
                        throw new Exception("冷数据损坏");
                    using (BinaryReader br = new BinaryReader(new MemoryStream(bytes)))
                    {
                        for (int i = 0; i < cd.Count; i++)
                        {
                            var time = cd.StartTime + br.ReadUInt32();
                            var data = br.ReadSingle();
                            res.Add((time, data));
                        }
                    }
                }
            }
            return res;
        }

        public Task<bool> DoCombine(long id1, long id2)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DoDelet(long id)
        {
            using (var ct = new MainContext())
            {
                var cd = await ct.Device_DataPoint_Colds.Where(it => it.Id == id).FirstOrDefaultAsync();
                if (cd == null)
                {
                    throw new Exception("已删除");
                }
                cd.status = 3;
                await ct.SaveChangesAsync();
                var suc = await mgr.DoDelet(cd);
                if (suc)
                {
                    //cd.status = 4;
                    ct.Remove(cd);
                    await ct.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("删除失败");
                }
            }
            return true;
        }

        public async Task<List<Device_DataPoint_Cold>> GetDataInfo(long? deviceid, long? streamid, long? starttime, long? endtime, long Cursor, int count, Action<long> setcursor)
        {
            using (var ct = new MainContext())
            {
                var cur = Cursor;
                var q = ct.Device_DataPoint_Colds.AsNoTracking().Where(it => it.Id >= cur && it.status != 4);
                if (endtime.HasValue)
                    q.Where(it => it.StartTime <= endtime);
                if (starttime.HasValue)
                    q.Where(it => it.EndTime > starttime);
                if (deviceid.HasValue)
                    q.Where(it => it.DeviceId == deviceid);
                if (streamid.HasValue)
                    q.Where(it => it.StreamId == streamid);
                var cds = await q.OrderBy(it => it.StartTime).Take(count + 1).ToListAsync();

                if (cds.Count == count + 1)
                    setcursor(cds.Last().Id);
                else
                    setcursor(0);
                cds.ForEach(it => mgr.DoGetStatus(it));
                return cds;
            }
        }

        public async Task<bool> DoStore(Device_DataPoint_Cold data, byte[] data2)
        {
            try
            {
                await mgr.DoStore(data, data2, null);
                return true;
            }
            catch (Exception)
            {
                throw new Exception("保存失败");
            }
        }

        public List<string> GetManagerNames()
        {
            return mgr.GetManagerNames();
        }
    }
}
