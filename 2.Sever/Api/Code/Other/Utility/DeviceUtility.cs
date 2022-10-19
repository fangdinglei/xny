//当启用时api不向平台发送数据
//#define SEVERONLY

using FDL.Program;
using System;
using System.Collections.Generic;
using XNYAPI.DAL;

namespace XNYAPI.Utility
{
    public class DeviceUtility
    {
        /// <summary>
        /// 发送命令给设备并写入此记录到数据库
        /// </summary>
        /// <param name="dvids"></param>
        /// <param name="cmd"></param>
        /// <param name="senderusername"></param>
        /// <return> success dv </return>
        /// <exception cref="Exception" />
        ///  <exception cref="NetFailedException" />
        static public List<uint> SendCMD(List<uint> dvids, string cmdstr, uint sender)
        {
            List<uint> re = new List<uint>();
            using (var cnn = DBCnn.GetCnn())
            {

                var cmd = cnn.CreateCommand();

                foreach (var id in dvids)
                {
                    if (sender != 1 && !UserDeviceDAL.HasDevice(sender, id))
                        continue;
                    string realid = DeviceDAL.GetRealID(id, cmd);
                    bool suc;
                    try
                    {
#if SEVERONLY
                        suc = true;
#else
                        //suc = OneNetUtility.SendCMD(realid, cmdstr);
#endif

                    }
                    catch (Exception)
                    {
                        suc = false;
                    }
                    //if (suc)
                    //{
                    //    re.Add(id);
                    //    DeviceDAL.AddCMD(id, cmdstr, cmd);
                    //}
                }
            }
            return re;
        }


        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="dv"></param>
        ///// <exception cref="Exception" />
        //static public void DeletDevice(uint dv)
        //{
        //    using (var cnn = DBCnn.GetCnn())
        //    {

        //        var cmd = cnn.CreateCommand();
        //        cmd.CommandText = $"DELETE FROM userdeviceinfo WHERE DeviceID={dv};";
        //        cmd.CommandText += $"DELETE FROM deviceinfo WHERE DeviceID={dv};";
        //        cmd.CommandText += $"DELETE FROM device_datapoints WHERE DeviceID={dv};";
        //        cmd.ExecuteNonQuery();

        //    }
        //}
        ///// <summary>
        ///// <para>创建设备，仅在数据库中相关记录 </para>
        ///// <para>应当先鉴权</para>
        ///// </summary>
        ///// <param name="info"></param>
        ///// <exception cref="Exception" />
        //static public void CreatDevice(DeviceInfoData info, string realid, string username)
        //{
        //    using (var cnn = DBCnn.GetCnn())
        //    {

        //        var cmd = cnn.CreateCommand();
        //        StringBuilder sb = new StringBuilder();
        //        //sb.Append("INSERT INTO deviceinfo(?) VALUES(?)");
        //        //插入数据
        //        sb.Append("INSERT INTO deviceinfo(");
        //        sb.Append("DeviceID,Location,DeviceName,DeviceRealID) VALUSES(  ");
        //        sb.Append(info.DeviceID);
        //        sb.Append(" ,'");
        //        sb.Append(info.Location);
        //        sb.Append("','");
        //        sb.Append(info.DeviceName);
        //        sb.Append("','");
        //        sb.Append(realid);
        //        sb.Append("');");
        //        //交给自己 
        //        //交给自己的父用户
        //        string f = username;
        //        while (f != null && f != "system")
        //        {
        //            sb.Append("INSERT IGNORE INTO userdeviceinfo(");
        //            sb.Append("UserName,DeviceID) VALUSES( '");
        //            sb.Append(f);
        //            sb.Append("', ");
        //            sb.Append(info.DeviceID);
        //            sb.Append(" );");
        //            f = AccountDAL.GetUserFather(username);
        //        }
        //        cmd.CommandText = sb.ToString();
        //        cmd.ExecuteNonQuery();
        //    }
        //}
    }
}
