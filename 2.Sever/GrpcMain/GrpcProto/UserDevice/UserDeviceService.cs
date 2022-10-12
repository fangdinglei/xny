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
            long id = (long)context.UserState["UserId"];
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
            long id = (long)context.UserState["UserId"];
            Response_GetGroupInfos res = new Response_GetGroupInfos();
            using (MainContext ct = new MainContext())
            {
                var ls = await ct.User_Device_Groups.Where(it => it.UserId == id).AsNoTracking().ToListAsync();
                foreach (var item in ls)
                {
                    res.GroupId.Add(item.Id);
                    res.GroupName.Add(item.Name);
                }
            }
            return res;
        }
    
        
    }
}
