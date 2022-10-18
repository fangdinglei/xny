
using MySqlConnector;
using System;

namespace XNYAPI.DAL
{
    public class DALUtility
    {

        /// <summary>
        /// 获取一个id
        /// </summary>
        /// <param name="ptrID"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        static public uint GetID(uint ptrID, MySqlCommand cmd)
        {
            while (true)
            {
                cmd.CommandText = $"SELECT Max,Current FROM id_allocator WHERE ID={ptrID}";
                uint max, curr;
                using (var rd = cmd.ExecuteReader())
                {
                    rd.Read();
                    max = rd.GetUInt32(0);
                    curr = rd.GetUInt32(1);
                }
                if (max < curr)
                    throw new Exception("OUT of ID " + ptrID);
                var res = curr;
                cmd.CommandText = $"UPDATE id_allocator SET Current=Current+1 WHERE ID={ptrID} AND Current={curr}";
                if (cmd.ExecuteNonQuery() != 0)
                    return res;
            }

        }
        /// <summary>
        /// 获取一些id
        /// </summary>
        /// <param name="ptrID"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        static public uint GetIDs(uint ptrID, int count, MySqlCommand cmd)
        {
            while (true)
            {
                cmd.CommandText = $"SELECT Max,Current FROM id_allocator WHERE ID={ptrID}";
                uint max, curr;
                using (var rd = cmd.ExecuteReader())
                {
                    rd.Read();
                    max = rd.GetUInt32(0);
                    curr = rd.GetUInt32(1);
                }
                if (max < curr + count - 1)
                    throw new Exception("OUT of ID " + ptrID);
                var res = curr;
                cmd.CommandText = $"UPDATE id_allocator SET Current=Current+{count} WHERE ID={ptrID} AND Current={curr}";
                if (cmd.ExecuteNonQuery() != 0)
                    return res;
            }

        }
    }

}
