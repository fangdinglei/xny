﻿using GrpcMain.Account;
using GrpcMain.Device;
using GrpcMain.DeviceType;
using GrpcMain.UserDevice;
using MyClient.Grpc;
using MyDBContext.Main;
using MyUtility;
using System.Collections;
using System.Reflection;
using static GrpcMain.Account.DTODefine.Types;
using TypeInfo = GrpcMain.DeviceType.TypeInfo;

namespace MyClient
{

    public class LocalDataBase : IResopnseInterceptor
    {
        static public LocalDataBase Instance;

        public UserInfo User;
        public Dictionary<long, (long, Device)> devices = new();
        public Dictionary<long, (long, UserInfo)> Users = new();
        //public List<DeviceWithUserDeviceInfo> DeviceWithUserDeviceInfos = new();
        public Dictionary<long, (long, User_Device_Group)> User_Device_Groups = new();
        /// <summary>
        /// userid,dvid
        /// </summary>
        public Dictionary<long, Dictionary<long, (long, User_Device)>> User_Devices = new();
        public Dictionary<long, (long, TypeInfo)> TypeInfos = new();

        DeviceTypeService.DeviceTypeServiceClient _deviceTypeServiceClient;
        AccountService.AccountServiceClient _accountServiceClient;
        DeviceService.DeviceServiceClient _deviceServiceClient;
        UserDeviceService.UserDeviceServiceClient _userDeviceServiceClient;
        ITimeUtility _timeUtility;
        IClientCallContextInterceptor _callContextInterceptor;
        public LocalDataBase(DeviceService.DeviceServiceClient deviceServiceClient, UserDeviceService.UserDeviceServiceClient userDeviceServiceClient, AccountService.AccountServiceClient accountServiceClient, DeviceTypeService.DeviceTypeServiceClient deviceTypeServiceClient, ITimeUtility timeUtility, IClientCallContextInterceptor callContextInterceptor)
        {
            if (Instance != null)
            {
                throw new Exception(nameof(LocalDataBase) + "只能实例化一次");
            }
            _deviceServiceClient = deviceServiceClient;
            _userDeviceServiceClient = userDeviceServiceClient;
            _accountServiceClient = accountServiceClient;
            _deviceTypeServiceClient = deviceTypeServiceClient;
            Instance = this;
            _timeUtility = timeUtility;
            _callContextInterceptor = callContextInterceptor;
            _callContextInterceptor.RegistResopnseInterceptor(this);
        }

        public Device GetDevice(long id, bool cache = true, int retry = 5)
        {
            if (retry < 0)
            {
                return null;
            }
            if (cache && devices.ContainsKey(id) && (_timeUtility.GetTicket() - devices[id].Item1) < 10)
                return devices[id].Item2;
            try
            {
                var dv = _userDeviceServiceClient.GetDevices_2(new Request_GetDevices_2()
                {
                    DeviceId = id
                });
                return GetDevice(id, cache, retry - 1);
            }
            catch (Exception)
            {
                return GetDevice(id, cache, retry - 1);
            }

        }

        public UserInfo GetUserInfo(long id, bool cache = true, int retry = 5)
        {
            if (retry < 0)
            {
                return null;
            }
            if (cache && Users.ContainsKey(id)
                && (_timeUtility.GetTicket() - Users[id].Item1) < 10)
                return Users[id].Item2;
            try
            {
                var dv = _accountServiceClient.GetUserInfo(new Request_GetUserInfo()
                {
                    UserId = id,
                    SubUser = false,
                });
                return GetUserInfo(id, cache, retry - 1);
            }
            catch (Exception)
            {
                return GetUserInfo(id, cache, retry - 1);
            }

        }

        public TypeInfo GetTypeInfo(long id, bool cache = true, int retry = 5)
        {
            if (retry < 0)
            {
                return null;
            }
            if (cache && TypeInfos.ContainsKey(id)
                && (_timeUtility.GetTicket() - TypeInfos[id].Item1) < 10)
                return TypeInfos[id].Item2;
            try
            {
                var req = new GrpcMain.DeviceType.DTODefine.Types.Request_GetTypeInfos { };
                req.Ids.Add(id);
                var rsp = _deviceTypeServiceClient.GetTypeInfos(req);
                return GetTypeInfo(id, cache, retry - 1);
            }
            catch (Exception)
            {
                return GetTypeInfo(id, cache, retry - 1);
            }

        }

        public User_Device GetUser_Device(long uid, long dvid, bool cache = true, int retry = 5)
        {
            if (retry < 0)
            {
                return null;
            }
            if (cache && User_Devices.ContainsKey(uid) && User_Devices[uid].ContainsKey(dvid)
                && (_timeUtility.GetTicket() - User_Devices[uid][dvid].Item1) < 10)
                return User_Devices[uid][dvid].Item2;
            try
            {
                _userDeviceServiceClient.GetUserDevices(new Request_GetUserDevices()
                {
                    UserId = uid,
                    DeviceIds = { dvid },
                });
                return GetUser_Device(uid, dvid, cache, retry - 1);
            }
            catch (Exception)
            {
                return GetUser_Device(uid, dvid, cache, retry - 1);
            }

        }
        public User_Device GetUser_Device(long dvid, bool cache = true, int retry = 5)
        {
            return GetUser_Device(User.ID, dvid, cache, retry);
        }
        public bool TestDeviceAuthorityWithMessageBox(long dvid, UserDeviceAuthority authority, string tip)
        {
            var uid = User.ID;
            UserDeviceAuthority? realat = null;
            if (User_Devices.ContainsKey(uid) && User_Devices[uid].ContainsKey(dvid)
             && (_timeUtility.GetTicket() - User_Devices[uid][dvid].Item1) < 10)
                realat = (UserDeviceAuthority)User_Devices[uid][dvid].Item2.Authority;
            else
            {
                _userDeviceServiceClient.GetUserDevices(new Request_GetUserDevices()
                {
                    UserId = uid,
                    DeviceIds = { dvid },
                });
            }
            if (User_Devices.ContainsKey(uid) && User_Devices[uid].ContainsKey(dvid))
                realat = (UserDeviceAuthority)User_Devices[uid][dvid].Item2.Authority;

            if (realat.HasValue)
            {
                if (!((UserDeviceAuthority)realat).HasFlag(authority))
                {
                    MessageBox.Show($"没有设备的{tip}权限", "提示");
                    return false;
                }
                return true;
            }
            else
            {
                MessageBox.Show("获取权限失败", "错误");
                return false;
            }
        }
        public bool TestUserAuthorityWithMessageBox(long uid, string authority, string tip)
        {
            string realat = null;
            if (Users.ContainsKey(uid)
             && (_timeUtility.GetTicket() - Users[uid].Item1) < 100)
                realat = Users[uid].Item2.Authoritys;
            else
            {
                var dv = _accountServiceClient.GetUserInfo(new Request_GetUserInfo()
                {
                    UserId = uid,
                    SubUser = false,
                });
            }
            if (User_Devices.ContainsKey(uid))
                realat = Users[uid].Item2.Authoritys;

            if (realat != null)
            {
                List<string> ats = new List<string>();
                if (realat.TryDeserializeObject(out ats))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show($"用户没有权限{authority}", "提示");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("获取权限失败", "错误");
                return false;
            }
        }

        public void OnResonse<TResponse>(TResponse rsp)
        {
            if (rsp == null)
            {
                return;
            }
            if (rsp.GetType().IsGenericType)
            {
                var a = rsp as IList;
                if (a != null)
                {
                    foreach (var b in a)
                    {
                        OnResonse(b);
                    }
                }
                return;
            }

            var tp = rsp.GetType();
            if (tp == typeof(Device))
            {
                var v = rsp as Device;
                devices[v.Id] = (_timeUtility.GetTicket(), v);
            }
            else if (tp == typeof(UserInfo))
            {
                var v = rsp as UserInfo;
                Users[v.ID] = (_timeUtility.GetTicket(), v);
            }
            else if (tp == typeof(User_Device))
            {
                var v = rsp as User_Device;
                if (!User_Devices.ContainsKey(v.UserId))
                {
                    User_Devices[v.UserId] = new();
                }
                User_Devices[v.UserId][v.Dvid] = (_timeUtility.GetTicket(), v);
            }
            else if (tp == typeof(User_Device_Group))
            {
                var v = rsp as User_Device_Group;
                User_Device_Groups[v.Id] = (_timeUtility.GetTicket(), v);
            }
            else if (tp == typeof(TypeInfo))
            {
                var v = rsp as TypeInfo;
                TypeInfos[v.Id] = (_timeUtility.GetTicket(), v);
            }
            else if (tp == typeof(string))
            {

            }
            else
            {
                foreach (var item in tp.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    OnResonse(item.GetValue(rsp));
                }
            }

        }
    }
}
