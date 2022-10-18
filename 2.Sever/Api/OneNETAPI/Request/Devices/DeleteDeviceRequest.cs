﻿using OneNET.Api.Response;
using OneNET.Api.Util;
using System;
using System.Collections.Generic;

namespace OneNET.Api.Request
{
    /// <summary>
    /// 删除设备
    /// 删除设备会删除该设备下所有数据流和数据点。删除设备动作是异步的，系统会在后续逐步删除该设备下的数据流和数据点
    /// </summary>
    public class DeleteDeviceRequest : IOneNETRequest<CommonResponse>
    {
        private const String URI = "<scheme>://<API_ADDRESS>/devices/<device_id>";
        private const String DEVICE_ID = "device_id";
        /// <summary>
        /// 删除设备的ID参数
        /// </summary>
        public string DeviceID;

        private IDictionary<string, string> otherParameters;

        /// <summary>
        /// 删除设备
        /// 删除设备会删除该设备下所有数据流和数据点。删除设备动作是异步的，系统会在后续逐步删除该设备下的数据流和数据点
        /// </summary>
        public DeleteDeviceRequest()
        {
            otherParameters = new Dictionary<string, string>();
        }

        public string GetURL(OneNetContext context)
        {
            context.setContext(DEVICE_ID, DeviceID);
            var url = URIUtils.fmtURI(URI, context);
            var webUtils = new WebUtils();
            return webUtils.BuildGetUrl(url, otherParameters);
        }

        public HttpRequestMethod RequestMethod()
        {
            return HttpRequestMethod.Delete;
        }

        public IDictionary<string, object> GetParameters()
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
            //if (DeviceID <= 0)
            //{
            //    throw new OneNETException("请输入要删除的设备ID");
            //}
        }

        public bool IsRequestForByte()
        {
            return false;
        }
    }
}
