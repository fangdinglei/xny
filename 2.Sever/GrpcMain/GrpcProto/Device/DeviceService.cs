using Grpc.Core;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility; 
namespace GrpcMain.Device
{
    public class DeviceServiceImp : DeviceService.DeviceServiceBase 
    { 
        ITimeUtility _timeutility;
        public DeviceServiceImp(  ITimeUtility time)
        { 
            _timeutility = time;
        }

    
        

    }
}
