namespace MyUtility
{
    public interface IRandomUtility {
        public string GetRandomString(int len);
        public string GetRandomHexString(int len);

    }
    public interface ITimeUtility
    {
        public int GetTicket_2(DateTime dt);
        public DateTime GetDateTime_2(int dt);
        public long GetTicket(DateTime dt);
        public DateTime GetDateTime(long dt);
    }

    public class RandomUtility: IRandomUtility
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
         RandomUtility()
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
    public class TimeUtility: ITimeUtility
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
        public long GetTicket (DateTime dt)
        {                                                                                                                 //此方法可用
            DateTime startTime = TimeZoneInfo.ConvertTime(new System.DateTime(1970, 1, 1), TimeZoneInfo.Utc, TimeZoneInfo.Local);  // 当地时区
            var timeStamp = (long)(dt - startTime).TotalSeconds; // 相差秒数
            return timeStamp;
        }
        /// <summary>
        /// 获取时间 
        /// </summary>
        /// <param name="dt">时间戳/1000</param>
        /// <returns></returns>
        public DateTime GetDateTime (long dt)
        {                                                                                                                 //此方法可用
            DateTime startTime = TimeZoneInfo.ConvertTime(new System.DateTime(1970, 1, 1), TimeZoneInfo.Utc, TimeZoneInfo.Local);  // 当地时区 
            return new DateTime(startTime.Ticks +  dt * 10000 * 1000);
        }
    }
}