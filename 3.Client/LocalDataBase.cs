using GrpcMain.Device;
using GrpcMain.DeviceType;
using GrpcMain.UserDevice;
using static GrpcMain.Account.DTODefine.Types;
using GrpcMain.Device ;
using GrpcMain.UserDevice ;

namespace MyClient
{
    public  class LocalDataBase {
        public long UserId;
        public Dictionary<long, Device> devices = new();

        public List<UserInfo> Users = new();
        public List<TypeInfo> TypeInfo = new();
        public List<DeviceWithUserDeviceInfo> DeviceWithUserDeviceInfos = new();
        public List<User_Device_Group> User_Device_Groups = new();


        DeviceService.DeviceServiceClient _deviceServiceClient;
        UserDeviceService.UserDeviceServiceClient _userDeviceServiceClient;
        public LocalDataBase(DeviceService.DeviceServiceClient deviceServiceClient, UserDeviceService.UserDeviceServiceClient userDeviceServiceClient)
        {
            _deviceServiceClient = deviceServiceClient;
            _userDeviceServiceClient = userDeviceServiceClient;
        }

        public Device GetDevice(long id ) {
            if (devices.ContainsKey(id))
                return devices[id]; 
            try
            {
                var dv = _userDeviceServiceClient.GetDevices_2(new Request_GetDevices_2()
                {
                    DeviceId = id
                });
                if (dv.Device == null)
                    return null;
                devices[id] = dv.Device;
                return devices[id];
            }
            catch (Exception)
            {
                return null;
            }
           
        }
    }
}
