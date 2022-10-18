
using System;

using XNYAPI.DAL;

namespace XNYAPI.Utility
{
    [TimerMvcWeb.Filters.AutoTask(Name = "RefreshDevices", OnLoadCall = "RefreshDevices")]
    public class DataServiceUtility
    {

        /// <summary>
        /// 从平台拉取数据 
        /// 设备表添加新设备 system用户添加设备映射
        /// </summary>
        /// <returns></returns>
        static public bool RefreshDevices()
        {

            try
            {


                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();



                    try
                    {
                        var pdvs = OneNetUtility.GetDevices();

                        //cmd.Transaction = cnn.BeginTransaction();
                        var sysid = AccountDAL.GetSystemUserID();
                        foreach (var dv in pdvs)
                        {
                            if (!dv.Id.IsSqlSafeString() || !dv.Title.IsSqlSafeString())
                                continue;
                            if (!DeviceDAL.HasDeviceOfRealID(dv.Id, cmd))
                            {
                                var iddv = DALUtility.GetID(2, cmd);
                                cmd.CommandText = $"INSERT IGNORE INTO deviceinfo (DeviceID,DeviceName,Location,DeviceRealID,Type)VALUES({iddv} ,'{dv.Title}','未知','{dv.Id}',0);" +
                                $"INSERT IGNORE INTO userdevice (UserID,DeviceID,GroupID)VALUES({sysid},{iddv},0); ";

                                cmd.ExecuteNonQuery();
                            }

                        }
                        //cmd.Transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        //cmd.Transaction.Rollback();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        ///// <summary>
        ///// 获取设备电量
        ///// </summary>
        ///// <returns></returns>
        //public string GetDeviceSoc(string ids)
        //{
        //    try
        //    {
        //        var dvs = Utility.Utility.PraseIDS(ids);
        //        var res = new Response.Data.GetDeviceSocResponse() { Data = new List<Response.Data.DeviceSoc>() };
        //        foreach (var id in dvs)
        //        {
        //            bool has;
        //            double max, current;
        //            (has, current, max) = PowerManager.Instance.GetSocInfo(id);
        //            if (has)
        //            {
        //                res.Data.Add(new XNYAPI.Response.Data.DeviceSoc(id, max, current));
        //            }
        //        }
        //        return JsonConvert.SerializeObject(res);
        //    }
        //    catch (Exception)
        //    {
        //        return this.Error(XNYResponseBase.EErrorCode.InternalError);
        //    }

        //}

        ///// <summary>
        ///// 获取设备电量
        ///// </summary>
        ///// <returns></returns>
        //public string GetDeviceSocAll()
        //{
        //    try
        //    {
        //        UserPayLoad payload = this.ViewBag.payload;
        //        var dvs = DeviceDAL.GetAllDeviceIDOfUser(payload.UserID);
        //        var res = new Response.Data.GetDeviceSocResponse() { Data = new List<Response.Data.DeviceSoc>() };
        //        foreach (var id in dvs)
        //        {
        //            bool has;
        //            double max, current;
        //            (has, current, max) = PowerManager.Instance.GetSocInfo(id);
        //            if (has)
        //            {
        //                res.Data.Add(new XNYAPI.Response.Data.DeviceSoc(id, max, current));
        //            }
        //        }
        //        return JsonConvert.SerializeObject(res);
        //    }
        //    catch (Exception)
        //    {
        //        return this.Error(XNYResponseBase.EErrorCode.InternalError);
        //    }
        //}

    }
}
