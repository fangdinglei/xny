﻿namespace MyUtility
{
    public interface IRandomUtility
    {
        public string GetRandomString(int len);
        public string GetRandomHexString(int len);

    }
    public interface ITimeUtility
    {
        public int GetTicket_2(DateTime dt);
        public DateTime GetDateTime_2(int dt);
        public long GetTicket(DateTime dt);
        /// <summary>
        /// 获取当前的时间戳
        /// </summary>
        /// <returns></returns>
        public long GetTicket();

        /// <summary>
        /// 获取时间 
        /// </summary>
        /// <param name="dt">时间戳/1000</param>
        /// <param name="local">是否使用本地时间，否则使用utc时间</param>
        /// <returns></returns>
        public DateTime GetDateTime(long dt, bool local = true);

        /// <summary>
        /// 获取两个时间的时间戳差值，此方法不考虑kind
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public long GetTickDiffer(DateTime date1, DateTime date2);

        /// <summary>
        /// 获取时间基址 当前为utc时间的1970.1.1 受到不同时区的影响
        /// </summary>
        /// <param name="local">是否使用本地时间，否则返回一个kind为utc的时间</param>
        /// <returns></returns>
        public DateTime GetDateRoot(bool local = true);

        public long TicketToSeconds(long ticket);
    }

    public class RandomUtility : IRandomUtility
    {
        public string GetRandomString(int len)
        {
            char[] cs = new char[len];
            for (int i = 0; i < len; i++)
            {
                cs[i] = chars[random.Next(0, chars.Length)];
            }
            return new string(cs);
        }
        public string GetRandomHexString(int len)
        {
            char[] cs = new char[len];
            for (int i = 0; i < len; i++)
            {
                var t = random.Next(0, 16);
                if (t <= 9)
                {
                    cs[i] = (char)('0' + t);
                }
                else
                {
                    cs[i] = (char)('A' + t - 10);
                }
            }
            return new string(cs);
        }

        public Random random = new Random();
        public char[] chars = new char[26 + 26 + 10];
        public RandomUtility()
        {
            int i = 0;
            for (char c = 'a'; c <= 'z'; c++)
            {
                chars[i++] = c;
            }
            for (char c = 'A'; c <= 'Z'; c++)
            {
                chars[i++] = c;
            }
            for (char c = '0'; c <= '9'; c++)
            {
                chars[i++] = c;
            }
        }

    }
    public class TimeUtility : ITimeUtility
    {
        /// <summary>
        /// 获取时间戳 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetTicket_2(DateTime dt)
        {                                                                                                                 //此方法可用
            DateTime startTime = TimeZoneInfo.ConvertTime(new System.DateTime(1970, 1, 1), TimeZoneInfo.Utc, TimeZoneInfo.Local);  // 当地时区
            var timeStamp = (int)(dt - startTime).TotalSeconds; // 相差秒数
            return timeStamp;
        }
        /// <summary>
        /// 获取时间 
        /// </summary>
        /// <param name="dt">时间戳/1000</param>
        /// <returns></returns>
        public DateTime GetDateTime_2(int dt)
        {                                                                                                                 //此方法可用
            DateTime startTime = TimeZoneInfo.ConvertTime(new System.DateTime(1970, 1, 1), TimeZoneInfo.Utc, TimeZoneInfo.Local);  // 当地时区 
            return new DateTime(startTime.Ticks + (long)dt * 10000 * 1000);
        }

        /// <summary>
        /// 获取时间戳 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public long GetTicket(DateTime dt)
        {
            DateTime startTime = GetDateRoot(dt.Kind != DateTimeKind.Utc);  // 当地时区
            var timeStamp = (long)(dt - startTime).TotalSeconds; // 相差秒数
            return timeStamp;
        }

        public DateTime GetDateTime(long dt, bool local = true)
        {                                                                                                                 //此方法可用
            DateTime startTime = GetDateRoot(local);  // 当地时区 
            return new DateTime(startTime.Ticks + dt * 10000 * 1000, local? DateTimeKind.Local: DateTimeKind.Utc );
        }

        public long GetTicket()
        {
            return GetTicket(DateTime.Now);
        }

        public long GetTickDiffer(DateTime date1, DateTime date2)
        {
            return (long)(date1 - date2).TotalSeconds;
        }

        public DateTime GetDateRoot(bool local = true)
        {
            if (local)
                return TimeZoneInfo.ConvertTime(new System.DateTime(1970, 1, 1), TimeZoneInfo.Utc, TimeZoneInfo.Local);
            return new System.DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        public long TicketToSeconds(long ticket)
        {
            return ticket;
        }
    }

}