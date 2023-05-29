using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyEmailUtility;
using MyUtility;
using Newtonsoft.Json;
using System.Text;

namespace Sever.DeviceProto
{
    public class DeviceMessageManager : IDeviceMessageHandle
    {
        record LastData(long thingModelId, long time, float value, long alertSeconds);
        ITimeUtility tu;
        IMyEmailUtility emailUtility;
        public DeviceMessageManager(ITimeUtility tu, IMyEmailUtility emailUtility)
        {
            this.tu = tu;
            this.emailUtility = emailUtility;
        }
        public void OnMsg(string topic, byte[] data)
        {
            Task.Run(async () =>
            {
                var sec = topic.Split("/", StringSplitOptions.RemoveEmptyEntries);
                if (sec.Length != 2)
                    return;
                long dvid = 0;
                if (!long.TryParse(sec[0], out dvid))
                    return;
                if (sec[1] == "cmd")
                    return;
                if (sec[1] == "data")
                {
                    var datastr = UTF8Encoding.UTF8.GetString(MyDecode(data));
                    var datasec = datastr.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    if (datasec.Length % 2 == 1)//数据包含名称和值 只能为偶数个
                        return;
                    //将数据转换为可接收的格式
                    var ls = new List<ValueTuple<long, float>>();
                    for (int i = 0; i < datasec.Length / 2; i++)
                    {
                        long tid;
                        float f;
                        if (!long.TryParse(datasec[i * 2], out tid))
                            continue;
                        if (!float.TryParse(datasec[i * 2 + 1], out f))
                            continue;
                        ls.Add((tid, f));
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
                        var thingmodels = await ct.ThingModels.Where(it => it.DeviceTypeId == type.Id).AsNoTracking().ToDictionaryAsync(it => it.Id, it => it);

                        List<LastData> lastDatas;
                        try
                        {
                            lastDatas = JsonConvert.DeserializeObject<List<LastData>>(dv.LatestData);
                            if (lastDatas == null)
                                lastDatas = new List<LastData>();
                        }
                        catch (Exception)
                        {
                            lastDatas = new List<LastData>();
                        }

                        //检查并插入
                        var nowTime = tu.GetTicket(DateTime.Now);
                        var isFirstAlerting = !dv.Alerting;
                        dv.Alerting = false;
                        foreach (var item in ls)
                        {
                            if (!thingmodels.TryGetValue(item.Item1, out var thingModel))
                                continue;
                            LastData? lastData, newData;
                            lastData = lastDatas.Find(it => it.thingModelId == item.Item1);

                            var alertingSecond =
                                (lastData != null && (item.Item2 < thingModel.AlertLowValue || item.Item2 > thingModel.AlertHighValue))
                                ? tu.TicketToSeconds(nowTime - lastData.time) / 60 / 60 / 24 > 0
                                    ? nowTime - lastData.time
                                    : lastData.alertSeconds + (nowTime - lastData.time)
                                : -1;

                            lastDatas.RemoveAll(lsitem => lastData != null && lsitem.thingModelId == lastData.thingModelId);
                            lastDatas.Add(new LastData(item.Item1, nowTime, item.Item2,
                                alertingSecond
                                ));
                            dv.Alerting = dv.Alerting || alertingSecond >= 0 && (alertingSecond / 60) >= thingModel.AlertTime;
                            ct.Add(new Device_DataPoint()
                            {
                                DeviceId = dvid,
                                StreamId = thingmodels[item.Item1].Id,
                                Time = nowTime,
                                Value = item.Item2,
                            });
                        }
                        dv.LatestData = JsonConvert.SerializeObject(lastDatas);
                        //批量提交 新数据和修改最新数据
                        await ct.SaveChangesAsync();

                        if (dv.Alerting && isFirstAlerting && !string.IsNullOrEmpty(dv.AlertEmail))
                        {
                            try
                            {
                                _ = emailUtility.Send(dv.AlertEmail, "警告", $"设备：{dv.Name} 所处的环境数据超出预设范围");
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                    }
                }
            });
        }


        static short CRC16(byte[] data, int start, int end)
        {
            uint CRC = 0xFFFF;
            int i = 0, j = 0;
            for (i = start; i < end; i++)
            {
                CRC = CRC ^ data[i];
                for (j = 0; j < 8; j++)
                {
                    if ((CRC & 0x01) != 0)
                        CRC = ((CRC >> 1) ^ 0xA001);
                    else
                        CRC = CRC >> 1;
                }
            }
            return (short)(CRC);
        }
        static public byte[] MyEncode(byte[] data)
        {
            byte[] res = new byte[2 + data.Length];
            var crc = CRC16(data, 0, data.Length);
            res[0] = (byte)(crc & 0xFF);
            res[1] = (byte)((crc >> 8) & 0xFF);
            Array.Copy(data, 0, res, 2, data.Length);
            return res;
        }
        static public byte[] MyDecode(byte[] data)
        {
            if (data.Length < 2)
            {
                throw new Exception("内容长度异常");
            }
            byte[] res = new byte[data.Length - 2];
            var crc = CRC16(data, 2, data.Length);
            if ((crc & 0xFFFF) != (data[0] & 0xFF | (data[1] & 0xFF) << 8))
            {
                throw new Exception("数据无法通过校验");
            }
            Array.Copy(data, 2, res, 0, data.Length - 2);
            return res;
        }
    }
}
