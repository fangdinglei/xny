namespace GrpcMain.Common
{
    public class MyConvertor
    {
        static public global::GrpcMain.Device.Device Get(MyDBContext.Main.Device value)
        {
            return new Device.Device()
            {
                DeviceTypeId = value.DeviceTypeId,
                Id = value.Id,
                LatestData = value.LatestData,
                LocationStr = value.LocationStr,
                Name = value.Name,
                Status = value.Status,
            };
        }

        static public UserDevice.User_Device Get(MyDBContext.Main.User_Device value)
        {
            return new UserDevice.User_Device()
            {
                Dvid = value.DeviceId,
                UserId = value.UserId,
                PControl = value.PControl,
                PData = value.PData,
                PStatus = value.PStatus,
                UserDeviceGroup = value.User_Device_GroupId,
            };
        }


    }
}
