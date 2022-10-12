using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
using static GrpcMain.UserDevice.UserDeviceTypes.Types;

namespace GrpcMain.UserDevice
{
    public class UserDeviceServiceImp : UserDeviceService.UserDeviceServiceBase
    {
        ITimeUtility _timeutility;
        public UserDeviceServiceImp(ITimeUtility time)
        {
            _timeutility = time;
        }
        [GrpcRequireAuthority]
        public override async Task<CommonResponse> UpdateUserDevice(Request_UpdateUserDevice request, ServerCallContext context)
        {//需要审计
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                if (request.UserId == id ||
                      await ct.User_SFs.Where(it => it.FatherId == id && it.SonId == request.UserId).CountAsync() == 0)
                {
                    return new CommonResponse()
                    {
                        Success = false,
                        Message = "指定了无效的接收用户",
                    };
                }
                //TODO 优化
                var count = await ct.Devices.Join(ct.User_Devices, it => it.Id, it => it.DeviceId, (dv, udv) => new { dv, udv })
                    .Where(it => it.udv.UserId == id && request.Dvids.Contains(it.dv.Id)).CountAsync();
                if (count != request.Dvids.Count)
                {
                    return new CommonResponse()
                    {
                        Success = false,
                        Message = "参数错误或者使用非法的设备ID或不是子用户ID",
                    };
                }
                foreach (var item in request.Dvids)
                {
                    ct.Add(new User_Device()
                    {
                        DeviceId = item,
                        UserId = request.UserId,
                        User_Device_GroupId = 0,
                        Status = request.Status,
                        Data = request.Data,
                        Control = request.Control,
                    });
                }
                await ct.SaveChangesAsync();
            }
            return new CommonResponse() { Success = true };
        }

        [GrpcRequireAuthority]
        public override async Task<Response_GetGroupInfos> GetGroupInfos(Empty request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            Response_GetGroupInfos res = new Response_GetGroupInfos();
            using (MainContext ct = new MainContext())
            {
                var ls = await ct.User_Device_Groups.Where(it => it.CreatorId == id).AsNoTracking().ToListAsync();
                foreach (var item in ls)
                {
                    res.GroupId.Add(item.Id);
                    res.GroupName.Add(item.Name);
                }
            }
            return res;
        }

        [GrpcRequireAuthority]
        public override async Task<CommonResponse> NewGroup(Request_NewGroup request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                ct.Add(new User_Device_Group() { 
                    Name = request.Name,
                    CreatorId=id
                });
               await ct.SaveChangesAsync();
            }
            return new CommonResponse() { Success = true };
        }
        [GrpcRequireAuthority]
        public override async Task<Response_GetUserAllDeviceID?> GetUserAllDeviceID(Request_GetUserAllDeviceID request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            long qid = id;
            if (request.HasUserId)
                id = request.UserId;
            using (MainContext ct = new MainContext())
            {
                IQueryable< User_Device  > bd;
                if (qid!=id)
                {
                     var count = await ct.User_SFs.Where(it => it.FatherId == id && it.SonId == qid).CountAsync();
                    if (count==0)
                    {
                        context.Status = new Status(StatusCode.PermissionDenied, "只能查询自己和子用户");
                        return null;
                    }
                }
                bd = ct.User_Devices.Where(it => it.UserId == id);
                var r = await bd.Select(it => it.DeviceId).ToListAsync();
                var res=new  Response_GetUserAllDeviceID() {
                  UserId=id,
                };
                return res;
            } 
        }

        [GrpcRequireAuthority]
        public override async Task<CommonResponse?> SetDeviceGroup(Request_SetDeviceGroup request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"]; 
            using (MainContext ct = new MainContext())
            {
                if (0==await ct.User_Device_Groups.Where(it => it.CreatorId == id).CountAsync())
                {
                    context.Status = new Status(StatusCode.PermissionDenied, "用户无该分组");
                    return null;
                }
                var bd =ct.User_Devices.Where(it => request.Dvids.ToList().Contains(it.DeviceId));
                var count = await bd.CountAsync();
                if (count != request.Dvids.Count) {
                    context.Status = new Status(StatusCode.PermissionDenied, "尝试为用户没有的设备添加分组或请求包含重复的设备编号");
                    return null;
                }
                foreach (var item in await bd.ToListAsync())
                {
                    item.User_Device_GroupId = request.GroupId;
                }
                await ct.SaveChangesAsync();
                return new CommonResponse() { Success = true };
            }  
        }
    }
}
