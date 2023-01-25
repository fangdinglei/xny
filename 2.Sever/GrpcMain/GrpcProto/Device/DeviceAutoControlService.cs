using Grpc.Core;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;

namespace GrpcMain.Device.AutoControl
{
    static public class Ext
    {
        /// <summary>
        /// 将请求对象转换为新的Grpc对象
        /// </summary>
        /// <returns></returns>
        static public DeviceAutoControlSetting AsGrpc(this Device_AutoControl_Settings_Item item)
        {
            return new DeviceAutoControlSetting
            {
                DeviceId = item.DeviceId,
                Cmd = item.Cmd,
                Name = item.Name,
                Open = item.Open,
                Order = item.Order,
                OwnerID = item.OwnerID,
                TimeEnd = item.TimeEnd,
                TimeStart = item.TimeStart,
                TriggerType = item.TriggerType,
                Week = item.Week,
            };
        }

        /// <summary>
        /// 将请求对象转换为新的DB对象
        /// </summary>
        /// <returns></returns>
        static public Device_AutoControl_Settings_Item AsDb(this DeviceAutoControlSetting item, long deviceid)
        {
            return new Device_AutoControl_Settings_Item
            {
                DeviceId = deviceid,
                Cmd = item.Cmd,
                Name = item.Name,
                Open = item.Open,
                Order = (byte)item.Order,
                OwnerID = item.OwnerID,
                TimeEnd = item.TimeEnd,
                TimeStart = item.TimeStart,
                TriggerType = (byte)item.TriggerType,
                Week = (byte)item.Week,
            };
        }
    }

    public class DeviceAutoControlServiceImp : DeviceAutoControlService.DeviceAutoControlServiceBase
    {
        ITimeUtility _timeutility;
        public DeviceAutoControlServiceImp(ITimeUtility time)
        {
            _timeutility = time;
        }


        public override async Task<Response_GetDeviceSetting> GetDeviceSetting(Request_GetDeviceSetting request, ServerCallContext context)
        {
            /*
            权限校验 用户拥有设备权限并拥有设备自动控制权限
            返回值按名称和优先级排序
             */
            User? user = context.UserState["user"] as User;
            if (user == null)
                throw new RpcException(new Status(StatusCode.PermissionDenied, "拒绝访问"));
            using var ct = new MainContext();
            var dv = await ct.Devices.Where(it => it.Id == request.Dvids).FirstOrDefaultAsync();
            var ud = await ct.User_Devices.Where(it => it.DeviceId == request.Dvids && it.UserId == user.Id).FirstOrDefaultAsync();
            if (dv == null || ud == null)
                throw new RpcException(new Status(StatusCode.PermissionDenied, "拒绝访问"));
            if (!((UserDeviceAuthority)ud.Authority).HasFlag(UserDeviceAuthority.Control_TimeSetting))
                throw new RpcException(new Status(StatusCode.PermissionDenied, "需要设备自动控制访问权限"));
            var data = await ct.Device_AutoControl_Settings_Items
                .Where(it => it.DeviceId == request.Dvids)
                .OrderBy(it => new { it.Name, it.Order })
                .AsNoTracking().ToListAsync();
            var res = new Response_GetDeviceSetting();
            res.Dvids = request.Dvids;
            res.Setting.AddRange(data.Select(it => it.AsGrpc()));
            return res;
        }

        public override async Task<CommonResponse> SetDeviceSetting(Request_SetDeviceSetting request, ServerCallContext context)
        {
            /*
            权限校验 用户拥有设备权限并拥有设备自动控制权限

            先删除对应设备的配置然后重新添加
             */
            User? user = context.UserState["user"] as User;
            if (user == null)
                throw new RpcException(new Status(StatusCode.PermissionDenied, "拒绝访问"));
            using var ct = new MainContext();
            using var trans = await ct.Database.BeginTransactionAsync();
            bool partfail = false;
            var settinglist = request.Setting.ToList().OrderBy(it => new { it.Name, it.Order });
            foreach (var item in request.Dvids)
            {
                var dv = await ct.Devices.Where(it => it.Id == item).FirstOrDefaultAsync();
                var ud = await ct.User_Devices.Where(it => it.DeviceId == item && it.UserId == user.Id).FirstOrDefaultAsync();
                if (dv == null || ud == null)
                {
                    partfail = true;
                    continue;
                }
                if (!((UserDeviceAuthority)ud.Authority).HasFlag(UserDeviceAuthority.Control_TimeSetting))
                    throw new RpcException(new Status(StatusCode.PermissionDenied, "需要设备自动控制访问权限"));

                await ct.Device_AutoControl_Settings_Items
                    .DeleteRangeAsync(ct, it => it.DeviceId == item);
                ct.AddRange(settinglist.Select(it => it.AsDb(item)));
                await ct.SaveChangesAsync();
            }
            await trans.CommitAsync();


            return new CommonResponse()
            {
                Success = !partfail,
                Message = partfail ? "部分设备无权限" : "",
            };
        }
    }
}
