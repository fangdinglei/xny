using System.Collections.Generic;

namespace OneNET.Api.Response
{
    /// <summary>
    /// 批量查询在线情况的返回体
    /// </summary>
    public class GetMultipleDevicceStatusRsp : OneNETResponse
    {
        /// <summary>
        /// 响应数据
        /// </summary>
        public MultipleDevicceStatusData Data;
    }

    public class MultipleDevicceStatusData
    {
        public class DevicceStatus
        {
            public string Title;
            public bool Online;
            public string Id;
        }
        public int Total_Count;
        public List<DevicceStatus> Devices;
    }
}
