using BaseDefines;
using Grpc.Core;
using GrpcMain.Attributes;
using GrpcMain.Common;
using GrpcMain.MQTT;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
using System.Text;

namespace GrpcMain.Device
{
    static public class Ext
    {
        /// <summary>
        /// 将请求对象转换为新的DB对象
        /// </summary>
        /// <returns></returns>
        static public MyDBContext.Main.Device AsNewDeviceInDB(this GrpcMain.Device.Device dv, User us)
        {
            return new MyDBContext.Main.Device
            {
                Id = 0,
                CreatorId = us.Id,
                UserTreeId = us.UserTreeId,
                DeviceTypeId = dv.DeviceTypeId,
                LatestData = "{}",
                LocationStr = dv.LocationStr ?? "",
                Name = dv.Name,
                AlertEmail= dv.HasAlertEmail? dv.AlertEmail:"",
                Status = 2,
            };
        }

    }

    public class DeviceServiceImp : DeviceService.DeviceServiceBase
    {
        DeviceUtility _du;
        ITimeUtility _timeutility;
        public DeviceServiceImp(ITimeUtility time, DeviceUtility du)
        {
            _timeutility = time;
            _du = du;
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

                    //var dv = await ct.Devices.Where(it => it.Id == dvid).FirstOrDefaultAsync();
                    //if (dv == null)
                    //    throw new Exception("不一致:设备" + dvid + " 应当存在却不存在");
                    var r = await _du.SendCmd(dvid, request.Cmd, DeviceCmdSenderType.User, id);
                    if (r)
                        return new CommonResponse()
                        {
                            Success = true,
                            Message = ""
                        };
                    else
                        return new CommonResponse()
                        {
                            Success = false,
                            Message = "发送失败或网络异常"
                        };
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
        [MyGrpcMethod("DeletDevice")]
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
                if (request.Device.HasAlertEmail)
                {
                    dv.AlertEmail = request.Device.AlertEmail;
                }
                await ct.SaveChangesAsync();
            }
            return new CommonResponse() { Success = true };
        }

        /// <summary>
        /// todo 加锁防止重名
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [MyGrpcMethod(nameof(UserAuthorityEnum.DeviceAdd), NeedDB = true, NeedTransaction = true)]
        public override async Task<Response_AddDevice> AddDevice(Request_AddDevice request, ServerCallContext context)
        {
            User us = (User)context.UserState["user"];
            var ct = (MainContext)context.UserState[nameof(MainContext)];
            var type = ct.Device_Types.Where(it => it.Id == request.Device.DeviceTypeId &&
            it.UserTreeId == us.UserTreeId).FirstOrDefault();
            if (type == null)
            {
                throw new RpcException(new Status(StatusCode.PermissionDenied, "没有该设备类型的权限"));
            }
            var dv = request.Device.AsNewDeviceInDB(us);
            ct.Devices.Add(dv);
            await ct.SaveChangesAsync();
            var fatherAndSelf = await ct.User_SFs.Where(it => it.User1Id == us.Id && !it.IsFather).Select(it => it.User2Id).ToListAsync();
            foreach (var item in fatherAndSelf)
            {
                ct.User_Devices.Add(new User_Device()
                {
                    UserTreeId = us.UserTreeId,
                    DeviceId = dv.Id,
                    UserId = us.Id,
                    User_Device_GroupId = 0,
                    _Authority = UserDeviceAuthority.Every
                });
            }
            await ct.SaveChangesAsync();
            return new Response_AddDevice()
            {
                Device = request.Device,
            };
        }

    }
}
