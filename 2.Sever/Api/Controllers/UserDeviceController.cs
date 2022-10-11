using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using XNYAPI.DAL;
using XNYAPI.Model;
using XNYAPI.Model.Account;
using XNYAPI.Model.Device;
using XNYAPI.Model.UserDevice;
using XNYAPI.Response;
using XNYAPI.Utility;

namespace XNYAPI.Controllers
{
    [TokenCheckFilter(new string[] { })]
    public class UserDeviceController : Controller
    { 

        /// <summary>
        /// 添加用户设备 
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="dvids"></param>
        ///   <exception cref="NetFailedException"/>
        public string AddUserDevice(uint uid, string dvids)
        {

            var dvs = Utility.Utility.PraseIDS(dvids);
            UserPayLoad payload = this.ViewBag.payload;
            try
            {
                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    //不是上级无法添加设备
                    if (!AccountDAL.IsFather(payload.UserID, uid, cmd))
                        return this.Error(XNYResponseBase.EErrorCode.PermissionDenied);
                    foreach (var dvid in dvs)
                    {
                        if (UserDeviceDAL.HasDevice(payload.UserID, dvid, cmd))
                        {
                            UserDeviceDAL.SetUserDevice(uid, dvid, false, cmd);
                        }

                    }
                }
                return this.Error(XNYResponseBase.EErrorCode.Non);
            }
            catch (Exception)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }

        }

        /// <summary>
        /// 删除用户设备 
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="dvids"></param>
        /// <exception cref="NetFailedException"/>
        public string DeletUserDevice(uint uid, string dvids)
        {
            UserPayLoad payload = this.ViewBag.payload;
            try
            {
                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    //不是上级无法删除设备
                    if (!AccountDAL.IsFatherOrFatherFather(payload.UserID, uid, cmd))
                        return this.Error(XNYResponseBase.EErrorCode.PermissionDenied);
                    var ids = Utility.Utility.PraseIDS(dvids);
                    foreach (var item in ids)
                    {
                        UserDeviceDAL.SetUserDevice(uid, item, true, cmd);
                    } 

                }
                return this.Error(XNYResponseBase.EErrorCode.Non);
            }
            catch (Exception)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }

        }

        /// <summary>
        /// 获取用户设备分组信息
        /// </summary>
        /// <returns></returns>
        public string GetGroupInfo() {
            UserPayLoad payload = this.ViewBag.payload;
            try
            {
                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    var devicegroups= UserDeviceDAL.GetAllDeviceGroup(payload.UserID);

                    return JsonConvert.SerializeObject(new DataListResponse<DeviceGroup>(devicegroups));
                } 
            }
            catch (Exception)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }
        }

        /// <summary>
        /// 获取用户的所有设备的信息
        /// </summary>
        /// <returns></returns>
        public string GetUserAllDeviceInfo() {
            UserPayLoad payload = this.ViewBag.payload;
            try
            {
                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    var res = UserDeviceDAL.GetUserAllDeviceInfo(payload.UserID); 
                    return JsonConvert.SerializeObject(new DataListResponse<DeviceInfo>(res));
                }
            }
            catch (Exception)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }
        }

        /// <summary>
        /// 获取用户所有设备的ID
        /// </summary>
        /// <returns></returns>
        public string GetUserAllDeviceID(uint uid=0) {
            UserPayLoad payload = this.ViewBag.payload;
            try
            {
                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    if (uid==0 || uid == payload.UserID)
                    {
                        uid = payload.UserID;
                    }
                    else
                    {
                        if (!AccountDAL.IsFatherOrFatherFather(payload.UserID, uid,cmd))
                        {
                            return this.Error(XNYResponseBase.EErrorCode.PermissionDenied);
                        }
                    } 
                    var res = UserDeviceDAL.GetUserAllDeviceID(uid,cmd);
                    return JsonConvert.SerializeObject(new DataResponse<ValueTuple<uint,List<uint>>> ((uid, res)));
                }
            }
            catch (Exception)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }
        }

        /// <summary>
        /// 设置设备的分组 
        /// </summary>
        /// <param name="dvids"></param>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public string SetDeviceGroup(string dvids, uint groupid=0) {
            UserPayLoad payload = this.ViewBag.payload;
            var dvs = Utility.Utility.PraseIDS(dvids);
            try
            {
                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    
                    if (groupid>0&&!UserDeviceDAL.HasGroup(payload.UserID,groupid,cmd))
                        return this.Error(XNYResponseBase.EErrorCode.PermissionDenied);
                    List<uint> res = new List<uint>();
                    foreach (var dv in dvs)
                    {
                        if (UserDeviceDAL.HasDevice(payload.UserID, dv,cmd))
                        {
                            var ok = UserDeviceDAL.SetUserDeviceGroup(payload.UserID, dv, groupid, cmd);
                            if (ok)
                            {
                                res.Add(dv);
                            } 
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
        /// 新的用户设备分组
        /// </summary>
        /// <returns></returns>
        public string NewGroup(string name) {
            if (!name.IsSqlSafeString())
                return this.Error( XNYResponseBase.EErrorCode.ParameterNotSafe);

            UserPayLoad payload = this.ViewBag.payload; 
            try
            {
                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    if (UserDeviceDAL.HasGroup(payload.UserID, name))
                        return JsonConvert.SerializeObject(new TextResponse(false,"分组已经存在"));
                    var gid= UserDeviceDAL.AddGroup(payload.UserID, name);
                    return JsonConvert.SerializeObject(new TextResponse (true,gid+""));
                }
            }
            catch (Exception ex)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }
        }

        public string UpdateGroupInfo(uint groupid,string groupname="",bool delet=false) {
            groupname = groupname == null ? "" : groupname;
            if (!groupname.IsSqlSafeString())
                return this.Error(XNYResponseBase.EErrorCode.ParameterNotSafe);
            UserPayLoad payload = this.ViewBag.payload;
            try
            {
                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    var has= UserDeviceDAL.HasGroup(payload.UserID,groupid,cmd);
                    if (!has)
                    return this.Error(XNYResponseBase.EErrorCode.PermissionDenied);
                    if (delet)
                    {
                        if (UserDeviceDAL.HasDeviceInGroup(groupid))
                            return JsonConvert.SerializeObject(new TextResponse(false, "还有设备在分组中"));
                        UserDeviceDAL.DeletDeviceGroup(groupid, cmd);
                    }
                    else
                    {
                        UserDeviceDAL.UpdateDeviceGroup(new DeviceGroup(groupid, groupname), cmd);
                    }
               
                return JsonConvert.SerializeObject(new TextResponse(true,""));
                   
                }
            }
            catch (Exception)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }
        }
    }

}
