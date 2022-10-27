using GrpcMain.Account;
using GrpcMain.Device;
using GrpcMain.DeviceType;
using GrpcMain.UserDevice;
using static GrpcMain.Account.DTODefine.Types;
using TypeInfo = GrpcMain.DeviceType.TypeInfo;

namespace MyClient
{
    public class LocalDataBase
    {
        public UserInfo User => Users[UserId];
        public long UserId;
        public Dictionary<long, Device> devices = new();

        public Dictionary<long, UserInfo> Users = new();
        public List<DeviceWithUserDeviceInfo> DeviceWithUserDeviceInfos = new();
        public List<User_Device_Group> User_Device_Groups = new();
        public Dictionary<long, TypeInfo> TypeInfos = new();

        DeviceTypeService.DeviceTypeServiceClient _deviceTypeServiceClient;
        AccountService.AccountServiceClient _accountServiceClient;
        DeviceService.DeviceServiceClient _deviceServiceClient;
        UserDeviceService.UserDeviceServiceClient _userDeviceServiceClient;
        public LocalDataBase(DeviceService.DeviceServiceClient deviceServiceClient, UserDeviceService.UserDeviceServiceClient userDeviceServiceClient, AccountService.AccountServiceClient accountServiceClient, DeviceTypeService.DeviceTypeServiceClient deviceTypeServiceClient)
        {
            _deviceServiceClient = deviceServiceClient;
            _userDeviceServiceClient = userDeviceServiceClient;
            _accountServiceClient = accountServiceClient;
            _deviceTypeServiceClient = deviceTypeServiceClient;
        }

        public Device GetDevice(long id, bool cache = true)
        {
            if (cache && devices.ContainsKey(id))
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

        public UserInfo GetUserInfo(long id, bool cache = true)
        {
            if (cache && Users.ContainsKey(id))
                return Users[id];
            try
            {
                var dv = _accountServiceClient.GetUserInfo(new Request_GetUserInfo()
                {
                    UserId = id,
                    SubUser = false,
                });
                if (dv.UserInfo.Count == 0)
                    return null;
                Users[id] = dv.UserInfo[0];
                return Users[id];
            }
            catch (Exception)
            {
                return null;
            }

        }
        public void Save(UserInfo value)
        {
            Users[value.ID] = value;
        }

        public TypeInfo GetTypeInfo(long id, bool cache = true)
        {
            if (cache && TypeInfos.ContainsKey(id))
                return TypeInfos[id];
            try
            {
                var req = new GrpcMain.DeviceType.DTODefine.Types.Request_GetTypeInfos { };
                req.Ids.Add(id);
                var rsp = _deviceTypeServiceClient.GetTypeInfos(req);
                if (rsp.TypeInfos.Count == 0)
                {
                    throw new Exception();
                }
                if (rsp.TypeInfos.Count == 0)
                    return null;
                TypeInfos[rsp.TypeInfos[0].Id] = rsp.TypeInfos[0];
                return rsp.TypeInfos[0];
            }
            catch (Exception)
            {
                return null;
            }

        }
        internal void Save(TypeInfo typeinfo)
        {
            TypeInfos[typeinfo.Id] = typeinfo;
        }
    }
}
