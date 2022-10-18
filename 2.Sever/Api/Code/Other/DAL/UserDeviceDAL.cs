
using MySqlConnector;
using System;
using System.Collections.Generic;
using XNYAPI.Model.Device;
using XNYAPI.Model.UserDevice;
using XNYAPI.Utility;

namespace XNYAPI.DAL
{

    public class UserDeviceDAL
    {
        public const uint DeviceGroupPtrID = 4;


        /// <summary>
        /// 获取用户是否有指定的设备
        /// </summary> 
        /// <returns> </returns>
        /// <exception cref="Exception"/>
        static public bool HasDevice(uint uid, uint id)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                return HasDevice(uid, id, cmd);
            }
        }

        static public bool HasDevice(uint uid, uint id, MySqlCommand cmd)
        {
            cmd.CommandText = $"SELECT 1 FROM userdevice " +
                 $" WHERE  DeviceID= {id}  and UserID={uid} LIMIT 1 ";
            var res = cmd.ExecuteScalar();
            if (res == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 将设备给用户或者删除
        /// </summary> 
        /// <returns> </returns>
        /// <exception cref="Exception"/>
        static public void SetUserDevice(uint uid, uint dvid, bool delet = false)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                SetUserDevice(uid, dvid, delet, cmd);
            }
        }

        static public void SetUserDevice(uint uid, uint dvid, bool delet, MySqlCommand cmd)
        {
            if (delet)
            {
                cmd.CommandText = $"DELETE FROM userdevice " +
                    $"WHERE UserID={uid} AND DeviceID={dvid};";
                //级联删除其子用户的设备
                cmd.CommandText += $"DELETE FROM userdevice " +
                 $"WHERE DeviceID={dvid} AND UserID IN( SELECT Son FROM user_sf WHERE Creator={uid});";
            }
            else
            {
                cmd.CommandText = $"INSERT IGNORE INTO userdevice(UserID,DeviceID,GroupID)  VALUES(" +
                      $"{uid}, {dvid},0 )";
            }

            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 删除用户所有设备，设备分组，设备分组信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="cmd"></param>
        /// <returns>  </returns>
        /// <exception cref="Exception"/>
        static public int DeletUserAllDevice(uint uid, MySqlCommand cmd)
        {
            cmd.CommandText = $"DELETE FROM userdevice_group WHERE UserID={uid};"
                + $"DELETE FROM userdevice WHERE UserID={uid};";
            cmd.CommandText += $"DELETE FROM userdevice " +
               $"WHERE  UserID IN( SELECT Son FROM user_sf WHERE Creator={uid});";
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 设置用户设备的分组
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="groupid"></param>
        /// <param name="cmd"></param>
        /// <return ></return>>
        /// <exception cref="Exception"/> 
        static public bool SetUserDeviceGroup(uint uid, uint deviceid, uint groupid, MySqlCommand cmd)
        {
            cmd.CommandText = $"UPDATE userdevice SET GroupID={groupid} " +
                $"WHERE UserID={uid} AND DeviceID={deviceid};";
            return cmd.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// 获取用户所有的分组信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        ///  <exception cref="Exception"/>
        static public List<DeviceGroup> GetAllDeviceGroup(uint uid)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                return GetAllDeviceGroup(uid, cmd);
            }
        }
        static public List<DeviceGroup> GetAllDeviceGroup(uint uid, MySqlCommand cmd)
        {
            List<DeviceGroup> res = new List<DeviceGroup>();
            cmd.CommandText = $"SELECT ID,Name FROM userdevice_group WHERE UserID={uid} ";
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    res.Add(new DeviceGroup(rd.GetUInt32(0), rd.GetString(1)));
                }
            }
            return res;
        }



        /// <summary>
        /// 更新设备分组信息 
        /// </summary> 
        /// <param name="group"></param> 
        ///  <exception cref="Exception"/>

        static public void UpdateDeviceGroup(DeviceGroup group, MySqlCommand cmd)
        {

            cmd.CommandText = $"Update userdevice_group SET Name='{group.Name}' WHERE ID={group.GroupID}";
            cmd.ExecuteNonQuery();
            return;
        }
        /// <summary>
        /// 删除设备分组
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="cmd"></param>
        static public void DeletDeviceGroup(uint gid, MySqlCommand cmd)
        {
            cmd.CommandText = $"DELETE FROM userdevice_group WHERE ID={gid}";
            cmd.ExecuteNonQuery();
            return;
        }
        /// <summary>
        /// 是否有设备在用户设备分组中
        /// </summary>
        /// <param name="groupid"></param> 
        /// <returns></returns>
        /// <exception cref="Exception"/>
        static public bool HasDeviceInGroup(uint groupid)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                return HasDeviceInGroup(groupid, cmd);
            }
        }
        static public bool HasDeviceInGroup(uint groupid, MySqlCommand cmd)
        {

            cmd.CommandText = $"SELECT 1 FROM  userdevice WHERE GroupID={groupid} LIMIT 1";
            if (cmd.ExecuteScalar() == null)
                return false;
            return true;
        }


        /// <summary>
        /// 是否拥有分组
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="groupname"></param>
        /// <returns></returns>
        static public bool HasGroup(uint userid, string groupname)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                return HasGroup(userid, groupname, cmd);
            }
        }
        static public bool HasGroup(uint userid, string groupname, MySqlCommand cmd)
        {
            cmd.CommandText = $"SELECT 1 FROM userdevice_group WHERE Name='{groupname}' AND UserID={userid}  ";
            if (cmd.ExecuteScalar() == null)
                return false;
            return true;
        }
        static public bool HasGroup(uint userid, uint groupid, MySqlCommand cmd)
        {
            cmd.CommandText = $"SELECT 1 FROM userdevice_group WHERE UserID={userid} AND   ID={groupid}  ";
            if (cmd.ExecuteScalar() == null)
                return false;
            return true;
        }


        /// <summary>
        /// 添加分组
        /// </summary>
        /// <param name="groupname"></param>
        /// <returns></returns>
        /// <exception cref="Exception" />
        static public uint AddGroup(uint uid, string groupname)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                return AddGroup(uid, groupname, cmd);
            }
        }
        static public uint AddGroup(uint uid, string groupname, MySqlCommand cmd)
        {
            var id = DALUtility.GetID(DeviceGroupPtrID, cmd);
            cmd.CommandText = $"INSERT INTO userdevice_group(ID,Name,UserID) VALUES({id},'{groupname}',{uid})";
            cmd.ExecuteNonQuery();
            return id;
        }
        static public List<DeviceInfo> GetUserAllDeviceInfo(uint uid)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                return GetUserAllDeviceInfo(uid, cmd);
            }
        }
        static public List<DeviceInfo> GetUserAllDeviceInfo(uint uid, MySqlCommand cmd)
        {
            List<DeviceInfo> res = new List<DeviceInfo>();
            cmd.CommandText = $"SELECT userdevice.DeviceID,GroupID,Location,DeviceName,Type FROM userdevice inner join deviceinfo on userdevice.DeviceID= deviceinfo.DeviceID  WHERE UserID={uid} ";
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    res.Add(new DeviceInfo(rd.GetUInt32(0), rd.GetUInt32(1)
                        , rd.GetString(3), rd.GetString(2), rd.GetUInt32(4)));
                }
            }
            return res;
        }



        static public List<uint> GetUserAllDeviceID(uint uid, MySqlCommand cmd)
        {
            List<uint> res = new List<uint>();
            cmd.CommandText = $"SELECT  DeviceID FROM userdevice WHERE UserID={uid} ";
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    res.Add(rd.GetUInt32(0));
                }
            }
            return res;
        }

    }

}
