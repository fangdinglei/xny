 
using MySqlConnector;
using System;
using System.Collections.Generic;
using XNYAPI.Model;
using XNYAPI.Model.Account;
using XNYAPI.Model.Device;
using XNYAPI.Utility;

namespace XNYAPI.DAL
{
    public class DeviceDAL {

        /// <summary>
        /// 获取设备的真实ID
        /// </summary> 
        /// <returns>null if not find</returns>
        /// <exception cref="Exception"/> 
        static public string GetRealID(uint id, MySqlCommand cmd)
        {
            cmd.CommandText = $"SELECT  DeviceRealID FROM deviceinfo " +
               $" WHERE  DeviceID={id}  ";
            var res = cmd.ExecuteScalar();
            if (res == null)
                return null;
            else
                return Convert.ToString(res);
        }

        /// <summary>
        /// 向命令记录表中添加一条命令记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cmdstr"></param> 
        /// <exception cref="Exception"/>
        static public void AddCMD(uint id, string cmdstr, MySqlCommand cmd)
        {
            cmd.CommandText = $"INSERT INTO cmd_send( OwnerID,CMDString,SendTime)" +
                $"VALUES({id},'{cmdstr}',{DateTime.Now.BeijingTimeToJavaTicket()})";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 获取系统中所有的设备的ID
        /// </summary> 
        /// <returns> </returns>
        /// <exception cref="Exception"/>
        static public List<uint> GetAllDevice_ID() {
            using (var cnn = DBCnn.GetCnn())
            {
                List<uint> res = new List<uint>();
                   var cmd = cnn.CreateCommand();
                cmd.CommandText = $"SELECT DeviceID FROM deviceinfo;" ;
                var rd=cmd.ExecuteReader();
                while (rd.Read())
                {
                    res.Add(rd.GetUInt32(0));
                }
                return res;
            }
        }
        /// <summary>
        /// 获取系统中所有的设备的ID和类型ID
        /// </summary> 
        /// <returns> <设备ID,设备类型></returns>
        /// <exception cref="Exception"/>
        static public List<ValueTuple<uint,uint>> GetAllDevice_ID_TypeID(  MySqlCommand cmd)
        {
            List<ValueTuple<uint, uint>> res = new List<ValueTuple<uint, uint>>();
            cmd.CommandText = $"SELECT DeviceID,Type FROM deviceinfo  ";
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    res.Add((rd.GetUInt32(0), rd.GetUInt32(1)) );
                }
            }
            return res;
        }

        ///// <summary>
        ///// 获取系统中所有的设备 
        ///// </summary> 
        ///// <returns><设备ID，设备类型,平台设备ID> </returns>
        ///// <exception cref="Exception"/>
        //static public List<ValueTuple<uint,uint, string> > GetAllDeviceRealIDOfSystem()
        //{
        //    using (var cnn = DBCnn.GetCnn())
        //    {
        //        List<ValueTuple<uint, uint, string>>  res = new  List<(uint, uint, string)>();
        //        var cmd = cnn.CreateCommand();
        //        cmd.CommandText = $"SELECT DeviceID,DeviceRealID,Type FROM deviceinfo;";
        //        var rd = cmd.ExecuteReader();
        //        while (rd.Read())
        //        {
        //            res.Add((rd.GetUInt32(0), rd.GetUInt32(2), rd.GetString(1)));
        //        }
        //        return res;
        //    }
        //}
        
        /// <summary>
        /// 获取系统中是否有指定的设备
        /// </summary> 
        /// <returns> </returns>
        /// <exception cref="Exception"/>
        static public bool HasDevice(  uint id, MySqlCommand cmd)
        {
            cmd.CommandText = $"SELECT 1 FROM deviceinfo " +
                 $" WHERE  DeviceID= {id}  LIMIT 1 ";
            var res = cmd.ExecuteScalar();
            if (res == null)
                return false;
            else
                return true;
        }

        static public bool HasDeviceOfRealID(string realid, MySqlCommand cmd)
        {
            cmd.CommandText = $"SELECT 1 FROM deviceinfo " +
                 $" WHERE  DeviceRealID='{realid}'  LIMIT 1 ";
            var res = cmd.ExecuteScalar();
            if (res == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 删除设备信息  以及用户设备
        /// </summary>
        /// <param name="dv"></param>
         static public void DeletDevicInfoAndUserDevice(uint dv, MySqlCommand cmd)
        {
            cmd.CommandText =$"DELETE FROM  deviceinfo WHERE DeviceID={dv};" +
                $"DELETE FROM userdevice WHERE DeviceID={dv}";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 更新设备的名称和位置信息
        /// </summary>
        /// <param name="dv"></param>
        /// <param name="cmd"></param>
         static public void UpdateDeviceInfo(DeviceInfo dv, MySqlCommand cmd)
        {
            /*
        public uint  ID;
        public uint GroupID;
        public string Name;
        public string Location;
             */ 
            cmd.CommandText = $" " +
                $"UPDATE deviceinfo SET Location='{dv.Location}',DeviceName='{dv.Name}' WHERE  DeviceID={dv.ID};";
            cmd.ExecuteNonQuery();
        }
    }

   
}
