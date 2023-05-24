using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
using Sever.ColdData.Imp;
using TimerMvcWeb.Filters;

namespace Sever.ColdData
{
    record A(long id, long type);
    /// <summary>
    /// 用于管理自动
    /// </summary>
    [AutoTask(Name = "ColdDataManager", OnTimeCall = "Run", IntervalSeconds = 60*24)]
    public class ColdDataManager
    {
        static ITimeUtility tu = new TimeUtility();
        static IDeviceColdDataService service = new DeviceColdDataServiceImp();

        static bool runing = false;

        private static async Task ForOneModel(DeviceColdDataSettings settings,
            long deviceid, long modelid, MainContext ct
        )
        {
            int maxcount = (int)settings.MinCount;
            long cursor1 = 0;

            //分批获取设备和数据点信息 并处理
            while (true)
            {
                await ct.Database.BeginTransactionAsync();
                try
                {
                    IQueryable<Device_DataPoint> bd = ct.Device_DataPoints;
                    //按时间筛选
                    var maxtime = tu.GetTicket(DateTime.Now) - settings.ColdDownTime * 60 * 60 * 24;
                    bd = bd.Where(it =>
                        it.StreamId == modelid && it.DeviceId == deviceid
                        && it.Time > cursor1
                        && it.Time < maxtime
                    );
                    bd = bd.OrderBy(it => it.Time);
                    //先选择
                    var ls = await bd.AsNoTracking().Take(maxcount).ToListAsync();
                    if (ls.Count < maxcount)
                    {
                        break;
                    }
                    Device_DataPoint_Cold cold = new Device_DataPoint_Cold()
                    {
                        Count = ls.Count(),
                        CreatTime = tu.GetTicket(),
                        StartTime = ls.First().Time,
                        EndTime = ls.Last().Time,
                        DeviceId = deviceid,
                        StreamId = modelid,
                    };
                    using MemoryStream ms = new MemoryStream(ls.Count() * (4 + 4));
                    using BinaryWriter binaryWriter = new BinaryWriter(ms);
                    foreach (var point in ls)
                    {
                        binaryWriter.Write((int)(point.Time - cold.StartTime));
                        binaryWriter.Write(point.Value);
                    }

                    cold.TreeId = settings.TreeId;
                    cold.status = 5;
                    ct.Add(cold);
                    await ct.SaveChangesAsync();
                    cold.ManagerName = settings.ManagerName;
                    await service.DoStore(cold, ms.GetBuffer());
                    ct.RemoveRange(ls);
                    cold.status = 1;
                    await ct.SaveChangesAsync();
                    await ct.Database.CommitTransactionAsync();
                    cursor1 = ls.Last().Time;
                }
                catch (Exception ex)
                {
                    await ct.Database.RollbackTransactionAsync();
                    return;
                }
            }
        }
        private static async Task ForOneDevice(DeviceColdDataSettings settings, A device, MainContext ct)
        {
            var types = await ct.ThingModels.Where(it => it.Id == device.type).AsNoTracking().ToListAsync();
            if (types.Count == 0)
                return;
            foreach (var item in types)
            {
                try
                {
                    await ForOneModel(settings, device.id, item.Id, ct);
                }
                catch (Exception)
                {
                }

            }

        }
        private static async Task ForOneTenant(DeviceColdDataSettings settings)
        {
            if (!settings.Open)
                return;
            using MainContext ct = new MainContext();

            var devices = await ct.Devices.Where(it => it.UserTreeId == settings.TreeId)
                .AsNoTracking().Select(it => new A(it.Id, it.DeviceTypeId)).ToListAsync();
            foreach (var device in devices)
            {
                try
                {
                    await ForOneDevice(settings, device, ct);
                }
                catch (Exception)
                {
                }
            }
        }

        public static async void Run()
        {
            if (runing)
            {
                return;
            }
            runing = true;
            try
            {
                using MainContext ct = new MainContext();
                var tenants = await ct.ColdDataSettings.AsNoTracking().ToListAsync();
                foreach (var tenant in tenants)
                {
                    try
                    {
                        await ForOneTenant(tenant);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                runing = false;
            }
        }
    }
}
