
using System;
using System.Collections.Generic;

namespace XNYAPI.Utility
{
    public class OneNetUtility
    {
        private const string url = "api.heclouds.com";   //平台网址
        private const string appkey = "midxzfmM0ENWzlBNhFha6fY1N9Y=";   //平台网址
        static public string fmtDateTime(DateTime dt)
        {
            return dt.ToString("s");
        }

        static public OneNET.Api.DefaultOneNETClient GetClient()
        {
            return new OneNET.Api.DefaultOneNETClient(url, appkey);
        }
        /// <summary>
        /// 获取设备状态
        /// </summary>
        /// <param name="devicesids"></param>
        /// <returns></returns>
        /// <exception cref="OneNET.Api.OneNETException"/>
        static public Dictionary<string, bool> GetDeviceStatus(List<string> devicesids)
        {
            Dictionary<string, bool> re = new Dictionary<string, bool>();
            var client = GetClient();
            var rq = new OneNET.Api.Request.GetMultipleDeviceStatusRequest(devicesids);
            var rsp = client.Execute(rq);
            if (rsp.IsError)
            {
                throw new OneNET.Api.OneNETException(rsp.ErrCode, rsp.Error);
            }
            rsp.Data.Devices.ForEach((it) =>
            {
                re.Add(it.Id, it.Online);
            });
            return re;
        }
        /// <summary>
        /// 获取设备状态
        /// </summary>
        /// <param name="devicesid"></param>
        /// <returns></returns>
        /// <exception cref="OneNET.Api.OneNETException"/>
        static public bool GetDeviceStatus(string devicesid)
        {
            Dictionary<string, bool> re = new Dictionary<string, bool>();
            var client = GetClient();
            var rq = new OneNET.Api.Request.GetMultipleDeviceStatusRequest(new List<string> { devicesid });
            var rsp = client.Execute(rq);
            if (rsp.IsError)
            {
                throw new OneNET.Api.OneNETException(rsp.ErrCode, rsp.Error);
            }
            if (rsp.Data.Total_Count == 0)
            {
                return false;
            }
            return true;
        }
        static public bool SendCMD(string deviceid, string cmd)
        {
            try
            {
                var res = GetClient().Execute(new OneNET.Api.Request.SendCmdRequest()
                {
                    CmdContent = cmd,
                    DeviceID = deviceid,
                    IsByte = false
                });
                if (res.Errno > 0)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
        static public List<OneNET.Api.Entity.DeviceBasicInfo> GetDevices()
        {
            OneNET.Api.DefaultOneNETClient cnt = new OneNET.Api.DefaultOneNETClient(url, appkey);
            var rsp = cnt.Execute(new OneNET.Api.Request.GetAllDeviceInfoRequest());
            return rsp.Data.Devices;
        }
        static public string CreatDevices(string name, string location = "", string describe = "")
        {
            OneNET.Api.DefaultOneNETClient cnt = new OneNET.Api.DefaultOneNETClient(url, appkey);
            var rsp = cnt.Execute(new OneNET.Api.Request.CreateDeviceRequest()
            {
                NewDevice = new OneNET.Api.Entity.DeviceBasicInfo()
                {
                    Private = false,
                    Title = name,
                    Desc = describe,
                }
            });
            return "" + rsp.Data.Device_Id;
        }

        static public bool DeletDevice(string dvid)
        {
            OneNET.Api.DefaultOneNETClient cnt = new OneNET.Api.DefaultOneNETClient(url, appkey);
            var rsp = cnt.Execute(new OneNET.Api.Request.DeleteDeviceRequest()
            {
                DeviceID = dvid
            });
            return !rsp.IsError;

        }

    }
}
