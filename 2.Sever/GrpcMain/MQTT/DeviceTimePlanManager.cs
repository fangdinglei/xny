using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using Sever.DeviceProto;
using TimerMvcWeb.Filters;

namespace GrpcMain.MQTT
{
    /// <summary>
    /// 设备定时控制管理器 负责检查和发送命令
    /// </summary>
    [AutoTask(Name = "DeviceTimePlanManager", OnTimeCall = "Run", IntervalSeconds = 1000*60*5)]
    public class DeviceTimePlanManager
    {
        static IProto _proto;
        static DeviceUtility _du;

        public static void InitTimePlan(IProto _proto, DeviceUtility _du)
        {
            DeviceTimePlanManager._proto = _proto;
            DeviceTimePlanManager._du = _du;
        }
        volatile static bool running = false;
        public static async void Run()
        {
            if (running)
            {
                return;
            }
            running = true;
            try
            {
                using var ct = new MainContext();
                //TODO 性能优化
                foreach (var dvid in ct.Devices.Select(it => it.Id).ToList())
                {
                    var settings = await ct.Device_AutoControl_Settings_Items.AsNoTracking()
                       .Where(it => it.OwnerID == dvid && it.Open == true)
                       .OrderBy(it => it.Name)
                       .ThenBy(it => it.Order).ToListAsync();
                    var names = settings.Select(it => it.Name).Distinct().ToList();
                    var dic = new Dictionary<string, List<Device_AutoControl_Settings_Item>>();
                    names.ForEach(it => dic[it] = new List<Device_AutoControl_Settings_Item>());
                    settings.ForEach(it => dic[it.Name].Add(it));
                    foreach (var kv in dic)
                    {
                        var cmd = kv.Value.GetCmd(DateTime.Now);
                        if (string.IsNullOrWhiteSpace(cmd))
                            continue;
                        await Task.Run(async () =>
                        {
                            try
                            {
                                await _du.SendCmd(dvid, cmd, DeviceCmdSenderType.System, 0);
                            }
                            catch (Exception ex)
                            {

                                throw;
                            }
                        });
                    }

                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                running = false;
            }
        }
    }
}
