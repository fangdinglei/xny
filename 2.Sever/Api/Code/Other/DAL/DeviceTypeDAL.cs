using MySqlConnector;
using System;
using System.Collections.Generic;
using XNYAPI.Model.Device;
using XNYAPI.Utility;

namespace XNYAPI.DAL
{
    public class DeviceTypeDAL
    {

        /// <summary>
        /// 获取设备类型信息
        /// </summary>
        /// <param name="typeid"></param>
        /// <returns>null if not find</returns>
        /// <exception cref="Exception" />
        static public DeviceTypeInfo GetTypeInfo(uint typeid)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                return GetTypeInfo(typeid, cmd);
            }
        }
        static public DeviceTypeInfo GetTypeInfo(uint typeid, MySqlCommand cmd)
        {
            cmd.CommandText = $"SELECT  Name,Script,DataNames  FROM devicetype WHERE ID={typeid}; ";
            using (var rd = cmd.ExecuteReader())
            {
                if (rd.Read())
                {
                    return new DeviceTypeInfo(typeid,
                        rd.GetString(0),
                       rd.GetString(1)
                        , rd.GetString(2));
                }
                else
                {
                    return null;
                }
            }

        }

        static public List<DeviceTypeInfo> GetTypeInfos()
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                return GetTypeInfos(cmd);
            }
        }
        static public List<DeviceTypeInfo> GetTypeInfos(MySqlCommand cmd)
        {
            List<DeviceTypeInfo> res = new List<DeviceTypeInfo>();
            cmd.CommandText = $"SELECT  ID,Name,Script,DataNames FROM devicetype  ; ";
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    var info = new DeviceTypeInfo(rd.GetUInt32(0), rd.GetString(1),
                 rd.GetString(2)
                        , rd.GetString(3));
                    res.Add(info);
                }
                return res;
            }
        }
        /// <summary>
        /// 设置设备类型信息
        /// </summary> 
        /// <returns> </returns>
        /// <exception cref="Exception" />
        static public void SetTypeInfo(uint ID, string Name, string b64ScriptString, string datanames)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                SetTypeInfo(ID, Name, b64ScriptString, datanames, cmd);
            }
        }

        static public void AddTypeInfo(uint ID, string Name, string b64ScriptString, string datanames, MySqlCommand cmd)
        {
            cmd.CommandText = $"INSERT devicetype " +
                    $"VALUES({ID},'{Name}' ,'{(string.IsNullOrWhiteSpace(b64ScriptString) ? "" : b64ScriptString)}' ,'{datanames}' )";
            cmd.ExecuteNonQuery();
        }
        static public void SetTypeInfo(uint ID, string Name, string b64ScriptString, string datanames, MySqlCommand cmd)
        {
            var s = b64ScriptString == "*" ? "" : $", Script='{(string.IsNullOrWhiteSpace(b64ScriptString) ? "" : b64ScriptString)}'  ";

            cmd.CommandText = $"UPDATE devicetype " +
                $"SET Name='{Name}' {s} , DataNames =  '{datanames}'" +
                $"WHERE ID = {ID}";
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// 是否拥有该类型的全部设备
        /// </summary>
        /// <param name="typeid"></param>
        /// <param name="uid"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        static public bool CanSetTypeInfo(uint typeid, uint uid, MySqlCommand cmd)
        {
            cmd.CommandText = $"SELECT COUNT(*) FROM deviceinfo WHERE Type = {typeid}";
            var total = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.CommandText = $"SELECT COUNT(*) " +
                $"FROM deviceinfo inner join userdevice ON deviceinfo.DeviceID= userdevice.DeviceID  WHERE Type = {typeid}";
            var total2 = Convert.ToInt32(cmd.ExecuteScalar());
            return total == total2;
        }
    }
}
