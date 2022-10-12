using Grpc.Core;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility; 
namespace GrpcMain.UserDevice
{
    public class UserDeviceServiceImp : UserDeviceService.UserDeviceServiceBase 
    { 
        ITimeUtility _timeutility;
        public UserDeviceServiceImp(  ITimeUtility time)
        { 
            _timeutility = time;
        }

    
        

    }
}
