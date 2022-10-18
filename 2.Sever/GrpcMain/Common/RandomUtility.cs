namespace GrpcMain.Common
{
    public class RandomUtility
    {
        static public void FisherYatesShuffle<T>(List<T> list)
        {
            Random r = new Random();
            List<T> cache = new List<T>();
            int currentIndex;
            while (list.Count > 0)
            {
                currentIndex = r.Next(0, list.Count);
                cache.Add(list[currentIndex]);
                list.RemoveAt(currentIndex);
            }
            for (int i = 0; i < cache.Count; i++)
            {
                list.Add(cache[i]);
            }
        }
        /// <summary>
        /// 红包1.68 6.68
        /// </summary>
        /// <param name="count"></param>
        /// <param name="total"></param>
        /// <param name="sample"></param>
        /// <returns></returns>
        static public List<int> GetRandomMoneys(int count, int total)
        {
            int y = (total - 168 * count) / 500;
            y = Math.Clamp(y, 0, count);
            int x = count - y;
            List<int> res = new List<int>();
            for (int i = 0; i < x; i++)
            {
                res.Add(168);
            }
            for (int i = 0; i < y; i++)
            {
                res.Add(668);
            }
            FisherYatesShuffle(res);
            return res;
        }


        /// <summary>
        /// 红包
        /// <br/>0.66*0.81   +   0.88*0.1   +   1.68*0.05   +   8.88*0.04 
        /// <br/>0.66*0.3   +   0.88*0.3   +   1.68*0.275   +   8.88*0.125
        /// <br/>0.66*0.25   +   0.88*0.25   +   1.68*0.25   +   8.88*0.25
        /// <br/>0.66*0.1   +   0.88*0.2   +   1.68*0.34   +   8.88*0.36
        /// <br/>0.66*0.05   +   0.88*0.15   +   1.68*0.31   +   8.88*0.49
        /// </summary>
        /// <param name="count"></param>
        /// <param name="avg"></param> 
        /// <returns></returns>
        static public List<int> GetRandomMoneys2(int count, int avg)
        {
            List<int> res = new List<int>();
            Random random = new Random((int)DateTime.Now.Ticks);
            int r = 0;
            int p1, p2, p3, p4;

            if (avg == 1)
            {
                p1 = 81;
                p2 = 10;
                p3 = 5;
                p4 = 4;
            }
            else if (avg == 2)
            {
                p1 = 30;
                p2 = 30;
                p3 = 27;
                p4 = 13;
            }
            else if (avg == 3)
            {
                p1 = 25;
                p2 = 25;
                p3 = 25;
                p4 = 25;

            }
            else if (avg == 4)
            {
                p1 = 10;
                p2 = 20;
                p3 = 34;
                p4 = 36;

            }
            else if (avg == 5)
            {
                p1 = 5;
                p2 = 15;
                p3 = 31;
                p4 = 49;
            }
            else
            {
                throw new Exception("均值只支持12345");
            }


            for (int i = 0; i < count; i++)
            {
                r = random.Next(100);
                if (r < p1)
                {
                    res.Add(66);
                }
                else if (r < p1 + p2)
                {
                    res.Add(88);
                }
                else if (r < p1 + p2 + p3)
                {
                    res.Add(168);
                }
                else if (r < p1 + p2 + p3 + p4)
                {
                    res.Add(888);
                }
                else
                {
                    throw new Exception("未知异常");
                }
            }

            return res;
        }
        static public string GetRandomString(int len)
        {
            char[] cs = new char[len];
            for (int i = 0; i < len; i++)
            {
                cs[i] = chars[random.Next(0, chars.Length)];
            }
            return new string(cs);
        }
        static public string GetRandomHexString(int len)
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

        static public Random random = new Random();
        static public char[] chars = new char[26 + 26 + 10];
        //static public

        static RandomUtility()
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
}
