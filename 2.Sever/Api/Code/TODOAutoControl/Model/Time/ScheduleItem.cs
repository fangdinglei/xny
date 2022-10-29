
using System;

namespace XNYAPI.Model.AutoControl
{
    public class ScheduleItem
    {
        public uint ID;
        public ServiceType Type;
        /// <summary>
        /// 类型
        /// </summary>
        public TimeTriggerType TriggerType;
        /// <summary>
        /// 时间开始 java 时间戳 周定时中为 1970.1.1.8+时长，反序列化时时间-8
        /// </summary>
        public long TimeStart;
        /// <summary>
        /// 时间结束 java 时间戳 周定时中为 1970.1.1.8+时长，反序列化时时间-8
        /// </summary>
        public long TimeEnd;
        /// <summary>
        /// 关联的设备的ID
        /// </summary>
        public uint OwnerID;
        /// <summary>
        ///   周信息 1位代表1天  低位为周日
        /// </summary>
        public byte Week;
        /// <summary>
        /// 存储的值
        /// </summary>
        public int Value;
        /// <summary>
        /// 该记录更新的时间
        /// </summary>
        public DateTime UpdateTime;
        /// <summary>
        /// 更新者
        /// </summary>
        public uint Updater;

        public byte Priority;

        private ScheduleItem() { }
        public ScheduleItem(ServiceType type, uint iD, TimeTriggerType triggertype, long timeStart, long timeEnd, byte week, uint ownerID, int value, byte priority, DateTime updateTime, uint updater)
        {
            ID = iD;
            Type = type;
            TriggerType = triggertype;
            TimeStart = timeStart;
            TimeEnd = timeEnd;
            OwnerID = ownerID;
            Value = value;
            Week = week;
            Priority = priority;
            UpdateTime = updateTime;
            Updater = updater;
        }

        ///  <summary>
        ///   创建一个星期定时任务
        ///  </summary>
        ///  <param name="ownerid"></param>
        ///  <param name="value"></param>
        ///  <param name="start"></param>
        ///  <param name="end"></param>
        ///  <param name="week">[周日,周六]是否生效</param>
        /// <param name="userid"></param>
        /// <returns></returns>
        static public ScheduleItem Creat(ServiceType type, uint ownerid, int value, long start, long end, bool[] week, uint userid)
        {
            var re = new ScheduleItem()
            {
                Type = type,
                TriggerType = TimeTriggerType.EveryWeek,
                TimeStart = start,
                TimeEnd = end,
                OwnerID = ownerid,
                Value = value,
                UpdateTime = DateTime.Now,
                Updater = userid
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
        /// <param name="type"></param>
        /// <param name="ownerid"></param>
        /// <param name="value"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        static public ScheduleItem Creat(ServiceType type, uint ownerid, int value, long start, long end, uint userid)
        {
            return new ScheduleItem()
            {
                Type = type,
                TriggerType = TimeTriggerType.Once,
                TimeStart = start,
                TimeEnd = end,
                OwnerID = ownerid,
                Value = value,
                UpdateTime = DateTime.Now,
                Updater = userid
            };
        }
        /// <summary>
        /// 创建一个一直生效的任务 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ownerid"></param>
        /// <param name="value"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        static public ScheduleItem Creat(ServiceType type, uint ownerid, int value, uint userid)
        {
            return new ScheduleItem()
            {
                Type = type,
                TriggerType = TimeTriggerType.ALL,
                OwnerID = ownerid,
                Value = value,
                UpdateTime = DateTime.Now,
                Updater = userid
            };
        }

        /// <summary>
        /// 给定时间是否在此时间内
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool IsTimeIn(DateTime time)
        {
            switch (TriggerType)
            {
                case TimeTriggerType.ALL:
                    return true;
                case TimeTriggerType.Once:
                    return (time >= TimeStart.JavaTicketToBeijingTime()) && (time <= TimeEnd.JavaTicketToBeijingTime());
                case TimeTriggerType.EveryWeek:
                    time = time.AddHours(8);
                    long t = time.BeijingTimeToJavaTicket() - time.Date.BeijingTimeToJavaTicket();
                    return (t >= TimeStart) && (t <= TimeEnd) && (Week & (1 << (int)time.DayOfWeek)) > 0;
                default:
                    return false;
            }
        }

        public DateTime GetTimeStart()
        {
            switch (TriggerType)
            {

                case TimeTriggerType.Once:
                    return TimeStart.JavaTicketToBeijingTime();
                case TimeTriggerType.EveryWeek:
                    return TimeStart.JavaTicketToBeijingTime().AddHours(-8);
                default:
                    return DateTime.Now;
            }
        }
        public DateTime GetTimeEnd()
        {
            switch (TriggerType)
            {
                case TimeTriggerType.Once:
                    return TimeEnd.JavaTicketToBeijingTime();
                case TimeTriggerType.EveryWeek:
                    return TimeEnd.JavaTicketToBeijingTime().AddHours(-8);
                default:
                    return DateTime.Now;
            }

        }
        /// <summary>
        /// 获取存储的真实值
        /// </summary>
        /// <returns></returns>
        public int GetValue()
        {
            return Value;
        }
        /// <summary>
        /// 获取计划中在某个时间的值
        /// </summary>
        /// <param name="val"></param>
        public void SetValue(int val)
        {
            Value = val;
        }

        const long TicketADay = 24L * 60 * 60 * 1000;
        /// <summary>
        /// 校验时间信息是否合法
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            switch (TriggerType)
            {
                case TimeTriggerType.ALL:
                    return true;
                case TimeTriggerType.Once:
                    return true;
                case TimeTriggerType.EveryWeek:

                    return (TimeStart >= 0 && TimeStart < TicketADay) && (TimeEnd >= 0 && TimeEnd < TicketADay);
                default:
                    return true;
            }
        }

        /// <summary>
        /// 校验数据是否合法
        /// </summary>
        /// <returns></returns>
        public bool Validate_Json()
        {
            if (!Enum.IsDefined(typeof(TimeTriggerType), TriggerType))
                return false;
            if (Week >= 128)
                return false;
            return Check();
        }
    }
}

