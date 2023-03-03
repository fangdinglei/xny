using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
using Sever.DeviceProto;
using System.ComponentModel;
using System.Text;

namespace GrpcMain.MQTT
{
    /// <summary>
    /// 设备工具 负责设备通讯相关操作
    /// </summary>
    public class DeviceUtility
    {
        ITimeUtility _tu;
        IProto _proto;
        public DeviceUtility(ITimeUtility tu, IProto proto)
        {
            _tu = tu;
            _proto = proto;
        }

        /// <summary>
        /// 向设备发送命令并记录操作到数据库<br/>该方法不鉴权
        /// </summary>
        /// <param name="deviceid"></param>
        /// <param name="cmd"></param>
        /// <param name="sendertype"></param>
        /// <param name="senderid"></param>
        public async Task<bool> SendCmd(long deviceid, string cmd, DeviceCmdSenderType sendertype, long senderid)
        {
            using var ct = new MainContext();
            var record = new DeviceCmdHistory(deviceid, cmd, sendertype, senderid, _tu.GetTicket(DateTime.Now));
            ct.DeviceCmdHistorys.Add(record);
            await ct.SaveChangesAsync();
            var suc = await _proto.SendCmd(deviceid.ToString(), cmd);
            if (suc)
            {
                record.Success = true;
                await ct.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class DeviceMessageManager : IDeviceMessageHandle
    {
        ITimeUtility tu;
        public DeviceMessageManager(ITimeUtility tu)
        {
            this.tu = tu;
        }
        public void OnMsg(string topic, byte[] data)
        {
            Task.Run(async  () => {
                var sec = topic.Split("/");
                if (sec.Length != 2)
                    return;
                long dvid = 0;
                if (!long.TryParse(sec[0], out dvid))
                    return;
                if (sec[1] == "cmd")
                    return;
                if (sec[1] == "data")
                {
                    var datastr = UTF8Encoding.UTF8.GetString(data);
                    var datasec = datastr.Split(',');
                    if (datasec.Length % 2 == 1)//数据包含名称和值 只能为偶数个
                        return;
                    //将数据转换为可接收的格式
                    var ls = new List<ValueTuple<string, float>>();
                    for (int i = 0; i < datasec.Length / 2; i++)
                    {
                        float f;
                        if (string.IsNullOrEmpty(datasec[i * 2]))
                            continue;
                        if (!float.TryParse(datasec[i * 2 + 1], out f))
                            continue;
                        ls.Add((datasec[i * 2], f));
                    }
                    using (MainContext ct = new MainContext())
                    {
                        //获取基础信息
                        var dv = await ct.Devices.Where(it => it.Id == dvid)
                           .FirstOrDefaultAsync();
                        if (dv == null)
                            return;
                        var type = await ct.Device_Types.Where(it => it.Id == dv.DeviceTypeId)
                            .AsNoTracking().FirstOrDefaultAsync();
                        if (type == null)
                            return;
                        var thingmodels = await ct.ThingModels.Where(it => it.DeviceTypeId == type.Id).AsNoTracking().ToDictionaryAsync(it => it.Name, it => it);
                        string lateststr = "";
                        //检查并插入
                        foreach (var item in ls)
                        {
                            if (!thingmodels.ContainsKey(item.Item1))
                                continue;
                            lateststr += "{" + thingmodels[item.Item1].Id + ":" + item.Item2 + "}";
                            ct.Add(new Device_DataPoint()
                            {
                                DeviceId = dvid,
                                StreamId = thingmodels[item.Item1].Id,
                                Time = tu.GetTicket(DateTime.Now),
                                Value = item.Item2,
                            }); ;
                        }
                        //批量提交 新数据和修改最新数据
                        await ct.SaveChangesAsync();
                    }

                }
            });
        }
    }
}
