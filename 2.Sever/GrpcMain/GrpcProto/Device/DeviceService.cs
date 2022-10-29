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
        public DeviceServiceImp(ITimeUtility time)
        {
            _timeutility = time;
        }


        public override Task<CommonResponse> SendCMD(Request_SendCMD request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO 加锁
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="RpcException"></exception>
        [GrpcRequireAuthority(true, "DeletDevice")]
        public override async Task<CommonResponse> DeletDevice(Request_DeletDevice request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
               
                var dv=await ct.Devices.Where(it => it.Id== request.Dvid).FirstOrDefaultAsync();
                if (dv == null)
                    throw new Exception("不一致:设备"+request.Dvid+" 应当存在却不存在");
                var ownertype=await dv.GetOwnerTypeAsync(ct,id);
                if (ownertype == AuthorityUtility.OwnerType.Non) {
                    //没权限
                    throw new RpcException(new Status(StatusCode.PermissionDenied, "没有该设备删除权限"));
                } else if (ownertype == AuthorityUtility.OwnerType.SonOfCreator) {
                    //子用户
                    var ud = await ct.User_Devices
                    .Where(it => it.DeviceId == request.Dvid && it.UserId == id)
                     .AsNoTracking().FirstOrDefaultAsync();
                    if (ud == null || !ud._Authority.HasFlag(UserDeviceAuthority.Write_DeletDevice))
                    {
                        //没有权限
                        throw new RpcException(new Status(StatusCode.PermissionDenied, "没有该设备删除权限"));
                    }
                    context.Status = Status.DefaultCancelled;
                    return null;
                }
                else {
                    //父用户和创建者
                    await ct.Database.BeginTransactionAsync();
                    await ct.Devices.DeleteRangeAsync(ct,it=>it.Id==request.Dvid);
                    await ct.User_Devices.DeleteRangeAsync(ct,it=>it.DeviceId==id);
                    await ct.Device_DataPoints.DeleteRangeAsync(ct, it => it.DeviceId == id);
                    await ct.Device_DataPoint_Colds.DeleteRangeAsync(ct, it => it.DeviceId == id);
                    await ct.Database.CommitTransactionAsync();
                    return new CommonResponse() { Success=true}; 
                }
            }
        }

        public override Task<Response_GetAllDeviceStatus> GetAllDeviceStatus(Request_GetAllDeviceStatus request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }


        public override Task<CommonResponse> UpdateDeviceInfo(Request_UpdateDeviceInfo request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

    }
}
