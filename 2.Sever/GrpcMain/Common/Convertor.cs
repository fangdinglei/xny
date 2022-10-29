using GrpcMain.Account;
using MyDBContext.Main;

namespace GrpcMain.Common
{

    public class MyConvertor
    {
        static public global::GrpcMain.Device.Device Get(MyDBContext.Main.Device value, UserDeviceAuthority authority)
        {
            return new Device.Device()
            {
                DeviceTypeId = value.DeviceTypeId,
                Id = value.Id,
                LatestData = authority.HasFlag( UserDeviceAuthority.read_status)?
                    value.LatestData:"",
                LocationStr = authority.HasFlag(UserDeviceAuthority.Read_BaseInfo) ? 
                    value.LocationStr:"",
                Name = value.Name,
                Status = authority.HasFlag(UserDeviceAuthority.read_status) ?
                    value.Status :0,
            };
        }

        static public UserDevice.User_Device Get(MyDBContext.Main.User_Device value)
        {
            return new UserDevice.User_Device()
            {
                Dvid = value.DeviceId,
                UserId = value.UserId,
                Authority=value.Authority,
                UserDeviceGroup = value.User_Device_GroupId,
            };
        }

        static public MyDBContext.Main.User_Device Get(UserDevice.User_Device userDevice)
        {
            return new MyDBContext.Main.User_Device
            {
                Authority = userDevice.Authority,
                UserId = userDevice.UserId,
                DeviceId=userDevice.Dvid,
                User_Device_GroupId = userDevice.UserDeviceGroup,
            };
        }

        static public DTODefine.Types.UserInfo Get(User user)
        {
            return new DTODefine.Types.UserInfo() { 
                Authoritys = user.Authoritys,
                Email=user.EMail,
                Father=user.CreatorId,
                ID = user.Id,
                LastLogin=user.LastLogin,
                Phone=user.Phone,
                UserName=user.Name,
            };
        }
    }
}
