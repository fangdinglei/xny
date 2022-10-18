using OneNET.Api.Response;
using OneNET.Api.Util;
using System;
using System.Collections.Generic;

namespace OneNET.Api.Request
{  /// <summary>
   /// 获取设备在线状态
   /// api: [API_Host]/devices/status
   /// @author 方定磊
   /// </summary>
    public class GetMultipleDeviceStatusRequest : IOneNETRequest<GetMultipleDevicceStatusRsp>
    {

        private const String URI = "<scheme>://<API_ADDRESS>/devices/status";
        private const string DEVIDS = "devIds";
        /// <summary>
        /// 查询具体设备的ID参数
        /// </summary>
        public List<string> DeviceIDs;

        public Scheme Protocol;

        private IDictionary<string, string> otherParameters;

        /// <summary>
        /// 获取设备在线状态
        /// </summary>
        public GetMultipleDeviceStatusRequest(List<string> dvs)
        {
            otherParameters = new Dictionary<string, string>();
            DeviceIDs = dvs;
        }

        #region IOneNETRequest Members

        public HttpRequestMethod RequestMethod()
        {
            return HttpRequestMethod.Get;
        }

        public String GetURL(OneNetContext context)
        {
            string devids = "";
            foreach (var id in DeviceIDs)
                devids += id + ",";
            devids = devids.Substring(0, devids.Length - 1);
            otherParameters.Add(DEVIDS, devids);
            var url = URIUtils.fmtURI(URI, context);
            var webUtils = new WebUtils();
            return webUtils.BuildGetUrl(url, otherParameters);
        }

        public IDictionary<string, Object> GetParameters()
        {
            var parameters = new OneNETDictionary();
            parameters.AddAll(this.otherParameters);
            return parameters;
        }

        public object GetPostContent()
        {
            return null;
        }

        public void Validate()
        {
            if (DeviceIDs == null || DeviceIDs.Count == 0)
            {
                throw new OneNETException("设备id不能为空");
            }
        }

        public bool IsRequestForByte()
        {
            return false;
        }

        #endregion

        public void AddOtherParameter(string key, string value)
        {
            this.otherParameters.Add(key, value);
        }
    }
}
