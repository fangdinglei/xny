using Grpc.Core;
using GrpcMain.Common;
using MyUtility;
using static GrpcMain.Device.DTODefine.Types;

namespace GrpcMain.Device
{
    public class DeviceServiceImp : DeviceService.DeviceServiceBase
    {
        ITimeUtility _timeutility;
        public DeviceServiceImp(ITimeUtility time)
        {
            _timeutility = time;
        }

        [GrpcRequireAuthority]
        public override Task<CommonResponse> SendCMD( Request_SendCMD request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }
        [GrpcRequireAuthority]
        public override Task<CommonResponse> DeletDevice( Request_DeletDevice request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }
        [GrpcRequireAuthority]
        public override Task<Response_GetAllDeviceStatus> GetAllDeviceStatus( Request_GetAllDeviceStatus request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        [GrpcRequireAuthority]
        public override Task<CommonResponse> UpdateDeviceInfo( Request_UpdateDeviceInfo request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

    }
}
