using Grpc.Core;
using GrpcMain.Common;
using GrpcMain.DeviceData;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility; 
namespace GrpcMain.DeviceData
{
    public class DeviceDataServiceImp :  DeviceDataService.DeviceDataServiceBase
    { 
        ITimeUtility _timeutility;
        public DeviceDataServiceImp(  ITimeUtility time)
        { 
            _timeutility = time;
        }

    
        

    }
}
