using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using Sever.DeviceProto;

namespace GrpcMain.MQTT
{

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
                foreach (var dvid in ct.Devices.Select(it => it.Id))
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
                        Task.Run(() =>
                        {
                            _du.SendCmd(dvid, cmd, DeviceCmdSenderType.System, 0);
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
                        await Task.Delay(1000 * 60 * 5);
                        await runc();
                    }
                    catch (Exception)
                    {

                    }
                }


            });
        }
    }
}
