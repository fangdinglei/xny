using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using Sever.DeviceProto;

namespace GrpcMain.MQTT
{
    /// <summary>
    /// 设备定时控制管理器 负责检查和发送命令
    /// </summary>
    public class DeviceTimePlanManager
    {
        IProto _proto;
        DeviceUtility _du;

        public DeviceTimePlanManager(IProto proto, DeviceUtility du)
        {
            _proto = proto; _du = du;
            var runc = async () =>
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

            };
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
#if AutoControlDebug
                        await Task.Delay(1000*10);
#else
                        await Task.Delay(1000*60*5);
#endif
                        await runc();
                    }
                    catch (Exception ex)
                    {

                    }
                }


            });
        }
    }
}
