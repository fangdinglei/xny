
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using XNYAPI.DAL;
using XNYAPI.Model;
using XNYAPI.Model.AutoControl;
using XNYAPI.Response;
using XNYAPI.Utility;

namespace XNYAPI.Controllers
{
    /// <summary>
    /// TODO 暂不支持分组操作
    /// </summary>
    [TokenCheckFilter(new string[] { })]
    public class DeviceLedController : Controller
    {
        const int AutoControlTimeSheduleLimit = 100;//max 255


        /// <summary>
        /// 设置自动控制配置信息
        /// </summary>
        /// <param name="dvids"></param>
        /// <param name="TimeScheduleEnabled"></param>
        /// <param name="AdvancedControlEnabled"></param>
        /// <returns></returns>
        public string SetAutoControlSetting(string dvids, bool TimeScheduleEnabled, bool AdvancedControlEnabled, bool isgroup = false, uint groupid = 0)
        {
            if (isgroup)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError, "敬请期待");
            }
            UserPayLoad payload = this.ViewBag.payload;
            var dvsls = Utility.Utility.PraseIDS(dvids);
            try
            {
                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    var vd = dvsls.Where((id) =>
                    {
                        if (isgroup)
                        {
                            return ServiceDAL.HasGroup(payload.UserID, id, cmd);
                        }
                        else
                        {
                            return UserDeviceDAL.HasDevice(payload.UserID, id, cmd);
                        }

                    }).ToList();
                    cmd.Transaction = cnn.BeginTransaction();
                    try
                    {
                        LedServiceDAL.SetAutoControlSetting(vd,
                            new AutoControlSettings(0, groupid, TimeScheduleEnabled, AdvancedControlEnabled, isgroup, ServiceType.DeviceLEDControl)
                            , isgroup, cmd);
                        cmd.Transaction.Commit();
                        return JsonConvert.SerializeObject(new DataListResponse<uint>(vd));
                    }
                    catch (Exception exx)
                    {
                        cmd.Transaction.Rollback();
                        throw;
                    }

                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new DataListResponse<uint>(new List<uint>()));
            }
        }
        /// <summary>
        /// 获取自动控制信息
        /// </summary>
        /// <param name="dvids"></param>
        /// <param name="TimeScheduleEnabled"></param>
        /// <param name="AdvancedControlEnabled"></param>
        /// <returns></returns>
        public string GetAutoControlSetting(string dvids, bool isgroup = false)
        {
            if (isgroup)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError, "敬请期待");
            }
            UserPayLoad payload = this.ViewBag.payload;
            List<AutoControlSettings> res = new List<AutoControlSettings>();
            var ids_uint = Utility.Utility.PraseIDS(dvids);
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                for (int i = 0; i < ids_uint.Count; i++)
                {
                    try
                    {
                        res.Add(LedServiceDAL.GetAutoControlSetting(ids_uint[i], isgroup));
                    }
                    catch (Exception ex)
                    {
                    }
                }

            }
            return JsonConvert.SerializeObject(new DataListResponse<AutoControlSettings>(res));

        }

        /// <summary>
        /// 获取定时控制信息
        /// </summary>
        /// <param name="dvids"></param>
        /// <returns></returns>
        public string GetAutoControlScheduleData(string dvids, bool isgroup = false)
        {
            if (isgroup)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError, "敬请期待");
            }
            UserPayLoad payload = this.ViewBag.payload;
            List<ScheduleInfo> res = new List<ScheduleInfo>();
            var ids_uint = Utility.Utility.PraseIDS(dvids);
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                for (int i = 0; i < ids_uint.Count; i++)
                {
                    try
                    {
                        res.Add(LedServiceDAL.GetAutoControlScheduleData(ids_uint[i], isgroup, ServiceType.DeviceLEDControl));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return JsonConvert.SerializeObject(new DataListResponse<ScheduleInfo>(res));
        }

        /// <summary>
        /// 设置定时控制信息
        /// </summary> 
        /// <returns></returns>
        [HttpPost]
        public string SetAutoControlScheduleData()
        {
            try
            {
                UserPayLoad payload = this.ViewBag.payload;
                string s = Utility.Utility.GetUTF8String(HttpContext.Request.Body);
                if (s == null)
                    return this.Error(XNYResponseBase.EErrorCode.ParameterWrong);
                List<ScheduleInfo> dts = JsonConvert.DeserializeObject<List<ScheduleInfo>>(s);
                //检查时间是否在限定的范围内
                foreach (var item in dts)
                {
                    if (item.Data != null && item.Data.Count >= AutoControlTimeSheduleLimit)
                        return this.Error(XNYResponseBase.EErrorCode.ParameterWrong);
                    if (!item.Validate_Json())
                        return this.Error(XNYResponseBase.EErrorCode.ParameterWrong);
                }

                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();

                    Func<uint, bool, bool> hasit = (id, isgroup) =>
                    {
                        if (isgroup)
                        {
                            return ServiceDAL.HasGroup(payload.UserID, id, cmd);
                        }
                        else
                        {
                            return UserDeviceDAL.HasDevice(payload.UserID, id, cmd);
                        }

                    };
                    cmd.Transaction = cnn.BeginTransaction();
                    try
                    {
                        foreach (var schedule in dts)
                        {
                            if (schedule.IsGroup)
                            {
                                return this.Error(XNYResponseBase.EErrorCode.InternalError, "敬请期待");
                            }
                            if (!hasit(schedule.OwnerID, schedule.IsGroup) || schedule.Data == null)
                                continue;
                            LedServiceDAL.SetAutoControlScheduleData(schedule, payload.UserID, cmd);
                        }
                        cmd.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        cmd.Transaction.Rollback();
                        throw;
                    }

                    return this.Error(XNYResponseBase.EErrorCode.Non);
                }
            }
            catch (Exception ex)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }

        }
    }

}
