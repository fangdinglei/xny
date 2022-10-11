 
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using System;
using System.Collections.Generic; 
using XNYAPI.DAL; 
using XNYAPI.Model.Device;
using XNYAPI.Response; 
using XNYAPI.Response.Device;
using XNYAPI.Utility;

namespace XNYAPI.Controllers
{
    [TokenCheckFilter(new string[] { })]
    public partial class DeviceController : Controller
    {

        /// <summary>
        /// 获取用户所有设备状态
        /// </summary>
        /// <returns></returns>
        public string GetAllDeviceStatus()
        {
            UserPayLoad payload = this.ViewBag.payload;
            var rsp = new GetAllDeviceStatusRsp();
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                cmd.CommandText = $"SELECT  deviceinfo.DeviceID,DeviceRealID FROM userdeviceinfo ,deviceinfo" +
                    $" WHERE  UserID='{payload.UserID}' and deviceinfo.DeviceID=userdeviceinfo.DeviceID ";
                var res = cmd.ExecuteReader();
                rsp.Data = new List<DeviceStatusData>();
                while (res.Read())
                {
                    DeviceStatusData dt = new DeviceStatusData();
                    //todo 一次性执行全部查询
                    var dvid = res.GetString(0);
                    var userdvid = res.GetString(1);
                    var r1 = OneNetUtility.GetDeviceStatus(new List<string> { dvid });
                    if (!r1.ContainsKey(dvid))
                    {
                        var r = new XNYResponseBase(XNYResponseBase.EErrorCode.InvalidQuery);
                        return JsonConvert.SerializeObject(r);
                    }
                    dt.DeviceID = userdvid;
                    dt.Status = r1[dvid] ? "Online" : "Offline";
                    rsp.Data.Add(dt);
                }
                res.Close();
            }
            return JsonConvert.SerializeObject(rsp);
        }
        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="dvids"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public string SendCMD(string dvids, string cmd)
        {
            if (dvids==null)
                return this.Error(XNYResponseBase.EErrorCode.ParameterWrong);
            if (!cmd.IsSqlSafeString())
                return this.Error(XNYResponseBase.EErrorCode.ParameterNotSafe);
            List<uint> dvidsx = Utility.Utility.PraseIDS(dvids);

            UserPayLoad payload = this.ViewBag.payload;
           
            try
            {
                var res = DeviceUtility.SendCMD(dvidsx, cmd, payload.UserID); 
                return JsonConvert.SerializeObject(new DataListResponse<uint>(res));
            }
            catch (Exception ex)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }
        }


        //[HttpPost]//todo 参数重新设计 返回值重新设计
        //public string CreatDevice()
        //{
        //    UserPayLoad payload = this.ViewBag.payload;
        //    try
        //    {
        //        using (var cnn = DBCnn.GetCnn())
        //        {
        //            var cmd = cnn.CreateCommand();
        //            //权限校验
        //            var ua = AccountDAL.GetUserAuthority(payload.UserName, cmd);
        //            if (!ua.HasFlag(UserAuthority.CreatDevice))
        //                return this.Error(XNYResponseBase.EErrorCode.PermissionDenied);
        //            //获取参数
        //            string s = Utility.GetUTF8String(HttpContext.Request.Body);
        //            List<DeviceInfoData> dts = JsonConvert.DeserializeObject<List<DeviceInfoData>>(s);
        //            //数据校验
        //            foreach (var dt in dts)
        //            {
        //                if (!dt.DeviceName.IsSqlSafeString()
        //                    || !dt.Location.IsSqlSafeString())
        //                    return this.Error(XNYResponseBase.EErrorCode.ParameterNotSafe);
        //            }
        //            //分批后一起插入
        //            StringBuilder sb = new StringBuilder();
        //            string sysname = AccountDAL.GetSystemUserName();

        //            cmd.CommandText = "SELECT MAX(DeviceID) FROM deviceinfo";
        //            var res = cmd.ExecuteScalar();
        //            if (res == null)
        //                res = 0;
        //            var nid = Convert.ToUInt32(res);
        //            for (int i = 0; i < dts.Count; i++)
        //            {
        //                var dt = dts[i];
        //                string dvid;
        //                try
        //                {
        //                    dvid = OneNetUtility.CreatDevices(dt.DeviceName);
        //                }
        //                catch (Exception)
        //                {
        //                    continue;
        //                }
        //                nid++;
        //                dt.DeviceID = nid;
        //                DeviceUtility.CreatDevice(dt, dvid, payload.UserName);
        //            }
        //            return this.Error(XNYResponseBase.EErrorCode.Non);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return this.Error(XNYResponseBase.EErrorCode.InternalError);
        //    }

        //}


        /// <summary>
        /// 删除设备 返回删除成功的id
        /// </summary>
        /// <param name="dvids"></param>
        /// <returns></returns>
        public string DeletDevice(string dvids)
        {
            //删除平台设备
            //删除用户所拥有的设备
            //删除设备记录
            if (dvids == null)
                return this.Error(XNYResponseBase.EErrorCode.ParameterWrong);
            UserPayLoad payload = this.ViewBag.payload;
            var dvs = Utility.Utility.PraseIDS(dvids);
            List<uint> res = new List<uint>();
            try
            {
                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    foreach (var dv in dvs)
                    {

                        try
                        {
                            if (!UserDeviceDAL.HasDevice(payload.UserID, dv, cmd))
                                continue;
                            DeviceDAL.DeletDevicInfoAndUserDevice(dv, cmd);
                            var realid = DeviceDAL.GetRealID(dv, cmd);
                            bool ok = OneNetUtility.DeletDevice(realid);
                            if (!ok)
                                continue;
                            res.Add(dv);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                    return JsonConvert.SerializeObject(new DataListResponse<uint>(res));
                }
            }
            catch (Exception)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }
        }
        /// <summary>
        /// 更新设备信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string UpdateDeviceInfo()
        {
            List<uint> res = new List<uint>();
            UserPayLoad payload = this.ViewBag.payload;
            try
            {
                string s = Utility.Utility.GetUTF8String(HttpContext.Request.Body);
                if (string.IsNullOrWhiteSpace(s))
                    return this.Error( XNYResponseBase.EErrorCode.ParameterWrong);
                List<DeviceInfo> dts = JsonConvert.DeserializeObject<List<DeviceInfo>>(s);
                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    foreach (var item in dts)
                    {
                        try
                        {
                            if (!UserDeviceDAL.HasDevice(payload.UserID, item.ID)
                           || !item.Name.IsSqlSafeString() || !item.Location.IsSqlSafeString())
                            {
                                continue;
                            }
                            DeviceDAL.UpdateDeviceInfo(item, cmd);
                            res.Add(item.ID);
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                       
                    }
                }
                return JsonConvert.SerializeObject(new DataListResponse<uint>(res));
                //todo update
            }
            catch (Exception ex)
            {
                return this.Error( XNYResponseBase.EErrorCode.InternalError);
            } 
        }

    }
}
