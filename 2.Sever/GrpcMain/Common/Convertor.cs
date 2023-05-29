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
                LatestData = authority.HasFlag(UserDeviceAuthority.Read_Status) ?
                    value.LatestData : "",
                LocationStr = authority.HasFlag(UserDeviceAuthority.Read_BaseInfo) ?
                    value.LocationStr : "",
                Name = value.Name,
                Status = authority.HasFlag(UserDeviceAuthority.Read_Status) ?
                    value.Status : 0,
                Alerting=value.Alerting,
                AlertEmail = value.AlertEmail,
            };
        }




    }
}
