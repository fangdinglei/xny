namespace MyClient.Utility
{
    public class Utility
    {
        /// <summary>
        /// 判断ID是否是组的ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        static public bool IsGroup(uint id)
        {
            return id >= 1073741830;
        }

        /// <summary>
        /// 将字符串连接,如果超过长度则截取符合长度的部分
        /// </summary>
        /// <param name="sarr"></param>
        /// <param name="maxlen"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        static public string BuildLongString(IEnumerable<string> sarr, int maxlen, string split = ",")
        {
            string s = "";
            foreach (var item in sarr)
            {
                s += item + split;
                if (s.Length > maxlen)
                {
                    break;
                }
            }
            if (s.Length > maxlen)
                s = s.Substring(0, maxlen - 3) + "...";
            else
                s = s.Substring(0, s.Length - 1);
            return s;
        }
    }
}
