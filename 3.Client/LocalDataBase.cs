using GrpcMain.Device;
using GrpcMain.DeviceType;
using GrpcMain.UserDevice;
using static GrpcMain.Account.DTODefine.Types;
using GrpcMain.Device ;
using GrpcMain.UserDevice ;
using GrpcMain.Account;

namespace MyClient
{
    public  class LocalDataBase {
        public long UserId;
        public Dictionary<long, Device> devices = new();

        public Dictionary<long, UserInfo> Users = new();
        public List<TypeInfo> TypeInfo = new();
        public List<DeviceWithUserDeviceInfo> DeviceWithUserDeviceInfos = new();
        public List<User_Device_Group> User_Device_Groups = new();

        AccountService.AccountServiceClient _accountServiceClient;
        DeviceService.DeviceServiceClient _deviceServiceClient;
        UserDeviceService.UserDeviceServiceClient _userDeviceServiceClient;
        public LocalDataBase(DeviceService.DeviceServiceClient deviceServiceClient, UserDeviceService.UserDeviceServiceClient userDeviceServiceClient, AccountService.AccountServiceClient accountServiceClient)
        {
            _deviceServiceClient = deviceServiceClient;
            _userDeviceServiceClient = userDeviceServiceClient;
            _accountServiceClient = accountServiceClient;
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
        public void Save(Device value)
        {
            devices[value.Id] = value;  
        }

        public UserInfo UserInfo(long id)
        {
            if (Users.ContainsKey(id))
                return Users[id];
            try
            {
                var dv = _accountServiceClient.GetUserInfo (new Request_GetUserInfo()
                {
                    UserId=id,
                    SubUser=false,
                });
                if (dv.UserInfo.Count==0  )
                    return null;
                Users[id] = dv.UserInfo[0];
                return Users[id];
            }
            catch (Exception)
            {
                return null;
            }

        }
        public void Save(UserInfo value) {
            Users[value.ID] = value;
        }

    }
}
