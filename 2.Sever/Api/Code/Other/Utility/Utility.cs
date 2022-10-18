
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XNYAPI.Utility
{
    public class Utility
    {
        static public List<uint> PraseIDS(string dvidstr)
        {
            List<uint> dvids = new List<uint>(); uint dvid;
            if (dvidstr == null)
                return dvids;
            var ids = dvidstr.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in ids)
                if (uint.TryParse(item, out dvid))
                    dvids.Add(dvid);
            return dvids;
        }
        /// <summary>
        /// 获取utf8编码的字符串
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>null if error</returns>
        static public string GetUTF8String(Stream stream)
        {
            try
            {
                using (var buffer = new MemoryStream())
                {
                    stream.CopyTo(buffer);
                    buffer.Position = 0;
                    var arr = new byte[buffer.Length];
                    buffer.Read(arr, 0, (int)buffer.Length);
                    string s = Encoding.UTF8.GetString(arr);
                    return s;
                }

            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
