using Grpc.Core;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;

namespace GrpcMain.Device
{
    public class RepairServiceImp : RepairService.RepairServiceBase
    {
        ITimeUtility _timeutility;
        IGrpcCursorUtility _grpcCursorUtility;
        public RepairServiceImp(ITimeUtility timeutility, IGrpcCursorUtility grpcCursorUtility)
        {
            _timeutility = timeutility;
            _grpcCursorUtility = grpcCursorUtility;
        }

        public override async Task<Response_AddRepairInfo> AddRepairInfo(Request_AddRepairInfo request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];  
            using (MainContext ct = new MainContext())
            {
                IQueryable<Device_Repair> bd;
                var ud = await ct.User_Devices
                   .Where(it => it.DeviceId == request.Info.DeviceId && it.UserId == id)
                    .AsNoTracking().FirstOrDefaultAsync();
                if (ud == null  )
                {
                    //没有权限
                    context.Status = new Status(StatusCode.PermissionDenied, "没有该设备的维修信息修改权限");
                    return null;
                }
                var obj = new Device_Repair
                {
                    DeviceId = request.Info.DeviceId,
                    CompletionTime = request.Info.CompletionTime,
                    Context = request.Info.Context,
                    CreatorId =id,
                    DiscoveryTime = request.Info.DiscoveryTime,
                };
                ct.Device_Repairs.Add(obj);
                await ct.SaveChangesAsync();

                request.Info.Id = obj.Id;
                return new Response_AddRepairInfo { 
                    Info=request.Info,
                }; 
            } 
        }

        public override async Task<Response_UpdateRepairInfo> UpdateRepairInfo(Request_UpdateRepairInfo request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                IQueryable<Device_Repair> bd;
                var ud = await ct.User_Devices
                   .Where(it => it.DeviceId == request.Info.DeviceId && it.UserId == id)
                    .AsNoTracking().FirstOrDefaultAsync();
                if (ud == null)
                {
                    //没有权限
                    context.Status = new Status(StatusCode.PermissionDenied, "没有该设备的维修信息修改权限");
                    return null;
                }

                var rp =await ct.Device_Repairs.Where(it=>it.Id==request.Info.Id).FirstOrDefaultAsync();
                if (rp==null||rp.DeviceId!=request.Info.DeviceId)
                {
                    //没有权限
                    context.Status = new Status(StatusCode.PermissionDenied, "没有该维修信息修改权限");
                    return null;
                }
                rp.DiscoveryTime = request.Info.DiscoveryTime;
                rp.CompletionTime = request.Info.CompletionTime;
                rp.Context = request.Info.Context;
                await ct.SaveChangesAsync(); 
                return new  Response_UpdateRepairInfo
                {
                    Info = request.Info,
                };
            }
        }
   
        public override async Task<Response_GetRepairInfos> GetRepairInfos(Request_GetRepairInfos request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"]; 
            if (!request.HasMaxCount)
                request.MaxCount = 1000;
            Response_GetRepairInfos res = new Response_GetRepairInfos();
            using (MainContext ct = new MainContext())
            {
                IQueryable<Device_Repair> bd;
                bd = ct.Device_Repairs.Join(ct.User_Devices, it => it.DeviceId, it => it.DeviceId, (a, b) => new { a, b }).Where(it => it.b.UserId == id).Select(it => it.a);
                if (request.HasDeviceId)
                {
                    bd = bd.Where(it => it.DeviceId == request.DeviceId);
                }
                if (request.HasCursor)
                {
                    bd = bd.Where(it => it.Id >= request.Cursor);
                }
                if (request.HasStartTime)
                {
                    bd = bd.Where(it => it.DiscoveryTime >= request.StartTime);
                }
                if (request.HasEndTime)
                {
                    bd = bd.Where(it => it.CompletionTime < request.EndTime);
                }
                bd = bd.Take(request.MaxCount + 1).AsNoTracking();
                var ls = await bd.ToListAsync();
                var lsx = _grpcCursorUtility.Run(ls, request.MaxCount + 1, (it) => { res.Cursor = it.Id; });
                res.Info.AddRange(lsx.Select(it => new RepairInfo()
                {
                    Id = it.Id,
                    CompletionTime = it.CompletionTime,
                    Context = it.Context,
                    CreatorId = it.CreatorId,
                    DeviceId = it.DeviceId,
                    DiscoveryTime = it.DiscoveryTime,
                }));



            }
            return res;
        }
    }
}
