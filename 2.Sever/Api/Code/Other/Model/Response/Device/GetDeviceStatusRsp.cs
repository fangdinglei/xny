using System.Collections.Generic;

namespace XNYAPI.Response.Device
{
    public class DeviceStatusData
    {
        public string DeviceID;
        public string Status;
    }
    public class GetSingleDeviceStatusRsp : XNYResponseBase
    {
        public DeviceStatusData Data;
    }

    public class GetAllDeviceStatusRsp : XNYResponseBase
    {
        public List<DeviceStatusData>  Data;
    }

  
}
