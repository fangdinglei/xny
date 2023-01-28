using MyDBContext.Main;
using MyUtility;
using XNYAPI.Model.AutoControl;

namespace GrpcMain.MQTT
{
    static public class DeviceAutoControlUtility
    {
        static TimeUtility tu = new TimeUtility();

        /// <summary>
        /// 给定时间是否在此时间内
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        static public bool IsTimeIn(this Device_AutoControl_Settings_Item item, DateTime time)
        {
            switch ((TimeTriggerType)item.TriggerType)
            {
                case TimeTriggerType.ALL:
                    return true;
                case TimeTriggerType.Once:
                    return time.Ticks >= item.TimeStart && time.Ticks <= item.TimeEnd;
                case TimeTriggerType.EveryWeek:
                    //time = time.AddHours(8);
                    long t = tu.GetTicket(time) - tu.GetTicket(time.Date);
                    return t >= item.TimeStart && t <= item.TimeEnd && (item.Week & 1 << (int)time.DayOfWeek) > 0;
                default:
                    return false;
            }
        }

        static public string? GetCmd(this List<Device_AutoControl_Settings_Item> item, DateTime time)
        {
            item = item.OrderBy(it => it.Order).ToList();
            for (int i = item.Count - 1; i >= 0; i--)
            {
                if (item[i].IsTimeIn(time))
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
        static public bool Check(this Device_AutoControl_Settings_Item item)
        {
            switch ((TimeTriggerType)item.TriggerType)
            {
                case TimeTriggerType.ALL:
                    return true;
                case TimeTriggerType.Once:
                    return true;
                case TimeTriggerType.EveryWeek:

                    return item.TimeStart >= 0 && item.TimeStart < TicketADay && item.TimeEnd >= 0 && item.TimeEnd < TicketADay;
                default:
                    return true;
            }
        }
    }
}
