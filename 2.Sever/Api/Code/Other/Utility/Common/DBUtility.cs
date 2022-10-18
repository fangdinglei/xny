using Newtonsoft.Json;

using System;
using System.Collections.Generic;

namespace XNYAPI.Utility
{
    public class DBUtility
    {
        /// <summary>
        /// 获取键值对的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        static public string GetKV(string key)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                cmd.CommandText = $"SELECT Value FROM t_kvs WHERE KeyName='{key}' LIMIT 1";
                string s = (string)cmd.ExecuteScalar();
                if (s != null)
                    return s;
                throw new Exception("没有该键值对");
            }
        }
        /// <summary>
        /// 获取键值对的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        static public string GetKV(string key, string defaultvalue)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                cmd.CommandText = $"SELECT Value FROM t_kvs WHERE KeyName='{key}' LIMIT 1";
                string s = (string)cmd.ExecuteScalar();
                if (s != null)
                    return s;
                return defaultvalue;
            }
        }
        /// <summary>
        /// 设置键值对的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="Exception"/>
        static public void SetKV(string key, string value)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                cmd.CommandText = $"SELECT 1 FROM t_kvs WHERE KeyName='{key}' LIMIT 1";
                var has = cmd.ExecuteScalar() != null;
                if (has)
                    cmd.CommandText = $"UPDATE t_kvs SET Value='{value}' WHERE  KeyName='{key}'";
                else
                    cmd.CommandText = $"INSERT INTO t_kvs VALUES('{key}','{value}')";
                cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 设置键值对的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="kv"></param>
        /// <exception cref="Exception"/>
        static public void SetKV(string key, Dictionary<string, string> kv)
        {
            SetKV(key, JsonConvert.ToString(kv));
        }


    }
}
