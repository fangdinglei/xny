namespace OneNET.Api.Response
{
    public class CreateDeviceRsp : OneNETResponse
    {
        public NewDeviceResult Data;

        public CreateDeviceRsp()
        {
            Data = new NewDeviceResult();
        }
    }

    public class NewDeviceResult
    {
        public int Device_Id;
    }

    public class DeviceRegisterRsp : OneNETResponse
    {
        public DeviceRegisterResult Data;

        public DeviceRegisterRsp()
        {
            Data = new DeviceRegisterResult();
        }
    }

    public class DeviceRegisterResult : NewDeviceResult
    {
        public string Key;
    }
}
