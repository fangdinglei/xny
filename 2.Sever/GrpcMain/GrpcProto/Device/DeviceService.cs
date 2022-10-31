using Grpc.Core;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
using System.Text;

namespace GrpcMain.Device
{
    public class DeviceServiceImp : DeviceService.DeviceServiceBase
    {
        ITimeUtility _timeutility;
        public DeviceServiceImp(ITimeUtility time)
        {
            _timeutility = time;
        }


        public override async Task<CommonResponse> SendCMD(Request_SendCMD request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                StringBuilder sb = new StringBuilder();
                foreach (var dvid in request.Dvids)
                {
                    var ud = await ct.User_Devices
                        .Where(it => it.DeviceId == dvid && it.UserId == id)
                        .AsNoTracking().FirstOrDefaultAsync();
                    if (ud == null || !ud._Authority.HasFlag(UserDeviceAuthority.Write_BaseInfo))
                    {
                        //没有权限
                        sb.Append(dvid + ":" + "没有设备基础信息修改权限");
                        continue;
                    }
                    if (!ud._Authority.HasFlag(UserDeviceAuthority.Control_Cmd))
                    {//没有权限
                        sb.Append(dvid + ":" + "没有设备命令权限");
                        continue;
                    }
                    var dv = await ct.Devices.Where(it => it.Id == dvid).FirstOrDefaultAsync();
                    if (dv == null)
                        throw new Exception("不一致:设备" + dvid + " 应当存在却不存在");
                    //TODO 发送命令
                }
                return new CommonResponse()
                {
                    Success = true,
                    Message = sb.ToString()
                };
            }
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

                var dv = await ct.Devices.Where(it => it.Id == request.Dvid).FirstOrDefaultAsync();
                if (dv == null)
                    throw new Exception("不一致:设备" + request.Dvid + " 应当存在却不存在");
                var ownertype = await dv.GetOwnerTypeAsync(ct, id);
                if (ownertype == AuthorityUtility.OwnerType.Non)
                {
                    //没权限
                    throw new RpcException(new Status(StatusCode.PermissionDenied, "没有该设备删除权限"));
                }
                else if (ownertype == AuthorityUtility.OwnerType.SonOfCreator)
                {
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
                else
                {
                    //父用户和创建者
                    //TODO 冷数据删除
                    await ct.Database.BeginTransactionAsync();
                    await ct.Devices.DeleteRangeAsync(ct, it => it.Id == request.Dvid);
                    await ct.User_Devices.DeleteRangeAsync(ct, it => it.DeviceId == id);
                    await ct.Device_DataPoints.DeleteRangeAsync(ct, it => it.DeviceId == id);
                    await ct.Device_DataPoint_Colds.DeleteRangeAsync(ct, it => it.DeviceId == id);
                    await ct.Database.CommitTransactionAsync();
                    return new CommonResponse() { Success = true };
                }
            }
        }

        public override Task<Response_GetAllDeviceStatus> GetAllDeviceStatus(Request_GetAllDeviceStatus request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }
        public override async Task<Response_GetDeviceStatusAndLatestData> GetDeviceStatusAndLatestData(Request_GetDeviceStatusAndLatestData request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            Response_GetDeviceStatusAndLatestData res = new Response_GetDeviceStatusAndLatestData();
            using (MainContext ct = new MainContext())
            {
                var dt = await ct.User_Devices.Join(ct.Devices, it => it.DeviceId, it => it.Id, (ud, dv) => new { ud, dv })
                   .Where(it => request.Dvids.Contains(it.ud.DeviceId) && it.ud.UserId == id && 0 != (it.ud.Authority & (int)UserDeviceAuthority.Read_Status))
                    .Select(it => new { it.dv.Status, it.dv.LatestData, it.dv.Id }).ToListAsync();
                foreach (var item in dt)
                {
                    res.Status.Add(item.Status);
                    res.Dvids.Add(item.Id);
                    res.LatestData.Add(item.LatestData);
                }
            }
            return res;
        }


        public override async Task<CommonResponse> UpdateDeviceBaseInfo(Request_UpdateDeviceBaseInfo request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                var ud = await ct.User_Devices
                    .Where(it => it.DeviceId == request.Device.Id && it.UserId == id)
                     .AsNoTracking().FirstOrDefaultAsync();
                if (ud == null || !ud._Authority.HasFlag(UserDeviceAuthority.Write_BaseInfo))
                {
                    //没有权限
                    throw new RpcException(new Status(StatusCode.PermissionDenied, "没有设备基础信息修改"));
                }
                var dv = await ct.Devices.Where(it => it.Id == request.Device.Id).FirstOrDefaultAsync();
                if (dv == null)
                    throw new Exception("不一致:设备" + request.Device.Id + " 应当存在却不存在");
                if (request.Device.HasLocationStr)
                {
                    dv.LocationStr = request.Device.LocationStr;
                }
                if (request.Device.HasName)
                {
                    dv.Name = request.Device.Name;
                }
                await ct.SaveChangesAsync();
            }
            return new CommonResponse() { Success = true };
        }
        //[GrpcRequireAuthority(true, "UpdateDeviceType")]
        //public override async Task<CommonResponse> UpdateDeviceType(Request_UpdateDeviceType request, ServerCallContext context)
        //{
        //    long id = (long)context.UserState["CreatorId"];
        //    using (MainContext ct = new MainContext())
        //    {
        //        var ud = await ct.User_Devices
        //           .Where(it => it.DeviceId == request.Dvid && it.UserId == id)
        //            .AsNoTracking().FirstOrDefaultAsync();
        //        if (ud == null || !ud._Authority.HasFlag(UserDeviceAuthority.Write_BaseInfo))
        //        {
        //            //没有权限
        //            throw new RpcException(new Status(StatusCode.PermissionDenied, "没有设备基础信息修改"));
        //        }
        //        if (!ud._Authority.HasFlag(UserDeviceAuthority.Write_Type))
        //        {
        //            throw new RpcException(new Status(StatusCode.PermissionDenied, "没有设备类型修改权限"));
        //        }
        //        var dv = await ct.Devices.Where(it => it.Id == request.Dvid).FirstOrDefaultAsync();
        //        if (dv == null)
        //            throw new Exception("不一致:设备" + request.Dvid + " 应当存在却不存在");

        //        var dvtypeownertype=await request.TypeId.GetOwnerTypeAsync(ct,id);
        //        if (dvtypeownertype== AuthorityUtility.OwnerType.Non)
        //            throw new Exception("没有该设备类型的权限");

        //        var ownertype = await dv.GetOwnerTypeAsync(ct, id);
        //        if (ownertype == AuthorityUtility.OwnerType.Non)
        //        {
        //            //没权限
        //            throw new RpcException(new Status(StatusCode.PermissionDenied, "不一致"));
        //        }
        //        else if (ownertype == AuthorityUtility.OwnerType.SonOfCreator)
        //        {
        //            //子用户
        //            context.Status = Status.DefaultCancelled;
        //            return null;
        //        }
        //        else
        //        {
        //            dv.DeviceTypeId = request.TypeId;
        //            await ct.SaveChangesAsync();
        //        }
        //    }
        //    return new CommonResponse() { Success = true };

        //}

    }
}
