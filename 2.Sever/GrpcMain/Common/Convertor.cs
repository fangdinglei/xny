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


    }
}
