using GrpcMain.Device.AutoControl;
using MyDBContext.Main;
using MyUtility;
using XNYAPI.Model.AutoControl;

namespace GrpcMain.MQTT
{
    static public class DeviceAutoControlUtility
    {
        static TimeUtility tu = new TimeUtility();

        ///  <summary>
        ///   创建一个星期定时任务
        ///  </summary>
        ///  <param name="week">[周日,周一,...]是否生效</param>
        static public DeviceAutoControlSetting Creat
            (string name, long ownerID, string cmd, long start, long end, bool[] week, int timeZone)
        {
            var re = new DeviceAutoControlSetting()
            {
                Name = name,
                TriggerType = (int)TimeTriggerType.EveryWeek,
                TimeStart = start,
                TimeEnd = end,
                OwnerID = ownerID,
                Cmd = cmd,
                TimeZone = timeZone,
                Open = true
            };
            re.Week = 0;
            for (int i = 0; i < week.Length; i++)
            {
                if (week[i])
                    re.Week |= (byte)(1 << i);
            }
            return re;
        }

        /// <summary>
        /// 创建一个时间任务
        /// </summary>
        static public DeviceAutoControlSetting Creat
            (string name, long ownerID, string cmd, long start, long end, int timeZone)
        {
            return new DeviceAutoControlSetting()
            {
                Name = name,
                TriggerType = (int)TimeTriggerType.Once,
                TimeStart = start,
                TimeEnd = end,
                OwnerID = ownerID,
                Cmd = cmd,
                Open = true,
                TimeZone = timeZone,
            };
        }

        /// <summary>
        /// 创建一个一直生效的任务 
        /// </summary>
        static public DeviceAutoControlSetting Creat
            (string name, long ownerID, string cmd)
        {
            return new DeviceAutoControlSetting()
            {
                Name = name,
                TriggerType = (int)TimeTriggerType.ALL,
                OwnerID = ownerID,
                Cmd = cmd,
                Open = true
            };
        }

        /// <summary>
        /// 给定时间是否在此时间内
        /// </summary>
        /// <param name="utcTime"></param>
        /// <returns></returns>
        static public bool IsTimeIn(this DeviceAutoControlSetting item, DateTime utcTime)
        {

            switch ((TimeTriggerType)item.TriggerType)
            {
                case TimeTriggerType.ALL:
                    return true;
                case TimeTriggerType.Once:
                    var tic = tu.GetTicket(utcTime.AddHours(item.TimeZone));
                    return (tic >= item.TimeStart) && (tic <= item.TimeEnd);
                case TimeTriggerType.EveryWeek:
                    utcTime = utcTime.AddHours(item.TimeZone);
                    var week = (int)utcTime.DayOfWeek;
                    long t = tu.GetTickDiffer(utcTime, utcTime.Date);
                    return (t >= item.TimeStart) && (t <= item.TimeEnd) && (item.Week & (1 << week)) > 0;
                default:
                    return false;
            }
        }

        static public string? GetCmd(this List<DeviceAutoControlSetting> item, DateTime timeUtc)
        {
            item = item.OrderBy(it => it.Order).ToList();
            for (int i = item.Count - 1; i >= 0; i--)
            {
                if (item[i].IsTimeIn(timeUtc))
                {
                    return item[i].Cmd;
                }
            }
            return null;
        }



        const long TicketADay = 24L * 60 * 60 * 1000;
        /// <summary>
        /// 校验时间信息是否合法
        /// </summary>
        /// <returns></returns>
        static public bool Check(this DeviceAutoControlSetting item)
        {
            switch ((TimeTriggerType)item.TriggerType)
            {
                case TimeTriggerType.ALL:
                    return true;
                case TimeTriggerType.Once:
                    return true;
                case TimeTriggerType.EveryWeek:

                    return (item.TimeStart >= 0 && item.TimeStart < TicketADay) && (item.TimeEnd >= 0 && item.TimeEnd < TicketADay);
                default:
                    return true;
            }
        }


    }
}
