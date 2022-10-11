using System.Collections.Generic;

namespace XNYAPI.Response.Device
{
    public class CMDSendResult
    {
        public uint DeviceID;
        public bool Success;
        public CMDSendResult()
        {
            
        }
        public CMDSendResult(uint deviceID, bool success)
        {
            DeviceID = deviceID;
            Success = success;
        }
    }
    public class SendCMDRsp : XNYResponseBase {
        public List<CMDSendResult> Data;
    }
}
