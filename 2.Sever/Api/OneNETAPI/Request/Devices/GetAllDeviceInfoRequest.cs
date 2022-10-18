using OneNET.Api.Entity;
using OneNET.Api.Response;
using OneNET.Api.Util;
using System;
using System.Collections.Generic;

namespace OneNET.Api.Request
{
    /// <summary>
    /// 查询所有的设备的基本信息  管理员权限
    /// </summary>
    public class GetAllDeviceInfoRequest : SearchDeviceReq, IOneNETRequest<GetAllDeviceInfoRsp>
    {
        public const String URI = "<scheme>://<API_ADDRESS>/devices";

        public Scheme Protocol;

        private IDictionary<string, string> otherParameters;

        /// <summary>
        /// 批量查看是指模糊查找设备
        /// </summary>
        public GetAllDeviceInfoRequest()
        {
            otherParameters = new Dictionary<string, string>();
        }

        public String GetURL(OneNetContext context)
        {
            otherParameters.Add(Key_WORDS, Keywords);
            if (OnLine.HasValue)
            {
                otherParameters.Add(ONLINE, OnLine.ToString());
            }
            if (Private.HasValue)
            {
                otherParameters.Add(PRIVATE, Private.ToString());
            }
            otherParameters.Add(PAGE, Page.ToString());
            otherParameters.Add(PER_PAGE, PageSize.ToString());
            otherParameters.Add(TAG, Tag);

            context.setContext(DEVICE_ID, DeviceIds);

            var url = URIUtils.fmtURI(URI, context);
            var webUtils = new WebUtils();
            return webUtils.BuildGetUrl(url, otherParameters);
        }

        public object GetPostContent()
        {
            return null;
        }

        public HttpRequestMethod RequestMethod()
        {
            return HttpRequestMethod.Get;
        }

        public IDictionary<string, Object> GetParameters()
        {
            var parameters = new OneNETDictionary();
            parameters.AddAll(otherParameters);
            return parameters;
        }

        public void Validate()
        {
        }

        public bool IsRequestForByte()
        {
            return false;
        }
    }
}