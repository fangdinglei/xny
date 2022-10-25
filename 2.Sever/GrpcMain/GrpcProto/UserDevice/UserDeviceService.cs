using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
using static GrpcMain.UserDevice.DTODefine.Types;

namespace GrpcMain.UserDevice
{
    public class UserDeviceServiceImp : UserDeviceService.UserDeviceServiceBase
    {
        ITimeUtility _timeutility;
        IGrpcCursorUtility _cursorUtility;



        public UserDeviceServiceImp(ITimeUtility time, IGrpcCursorUtility cursorUtility)
        {
            _timeutility = time;
            _cursorUtility = cursorUtility;
        }

        public override async Task<CommonResponse?> UpdateUserDevice(Request_UpdateUserDevice request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];

            using (MainContext ct = new MainContext())
            {
                if (!request.UserDevice.HasUserId ||
                    await id.IsDirectFatherAsync(ct, request.UserDevice.UserId) == false)

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

                if (!request.UserDevice.PStatus
                        && !request.UserDevice.PData
                        && !request.UserDevice.PControl)
                {//删除权限
                    await ct.DeleteRangeAsync<MyDBContext.Main.User_Device>(
                          it => request.Dvids.Contains(it.DeviceId)
                      );
                }
                else
                {//修改权限
                    //请求的用户的已有权限  用于拦截对子用户添加其他未拥有的权限
                    Dictionary<long, MyDBContext.Main.User_Device> dic = new();
                    (await ct.User_Devices.Where(it => request.Dvids.Contains(it.DeviceId) && it.UserId == id)
                        .AsNoTracking().ToListAsync()).ForEach(it =>
                        {
                            dic.Add(it.DeviceId, it);
                        });
                    //被修改者已有的权限
                    Dictionary<long, MyDBContext.Main.User_Device> dic2 = new();
                    (await ct.User_Devices.Where(it => request.Dvids.Contains(it.DeviceId) && it.UserId == request.UserId)
                        .ToListAsync()).ForEach(it =>
                        {
                            dic2.Add(it.DeviceId, it);
                        });

                    foreach (var item in request.Dvids)
                    {
                        //请求者的此记录
                        MyDBContext.Main.User_Device ud;

                        if (!dic.TryGetValue(item, out ud))
                        {
                            return new CommonResponse()
                            {
                                Success = false,
                                Message = "没有设备:" + item + " 的权限",
                            };
                        }

                        if (request.UserDevice.PStatus && !ud.PStatus
                            || request.UserDevice.PData && !ud.PData
                            || request.UserDevice.PControl && !ud.PControl
                            )
                        {
                            return new CommonResponse()
                            {
                                Success = false,
                                Message = "设备:" + item + " 的权限不足,请添加你拥有的权限",
                            };
                        }

                        //被请求者的此记录
                        MyDBContext.Main.User_Device ud2;
                        if (dic.TryGetValue(item, out ud2))
                        {
                            ud2.PStatus = request.UserDevice.PStatus;
                            ud2.PData = request.UserDevice.PData;
                            ud2.PControl = request.UserDevice.PControl;
                        }
                        else
                        {
                            ct.Add(new MyDBContext.Main.User_Device()
                            {
                                DeviceId = item,
                                UserId = request.UserId,
                                User_Device_GroupId = 0,
                                PStatus = request.UserDevice.PStatus,
                                PData = request.UserDevice.PData,
                                PControl = request.UserDevice.PControl,
                            });
                        }


                    }
                }

                await ct.SaveChangesAsync();

            }
            return new CommonResponse() { Success = true };
        }

        public override async Task<Response_GetDevices> GetDevices(Request_GetDevices request, ServerCallContext context)
        {
            int maxcount = 1000 + 1;
            long id = (long)context.UserState["CreatorId"];
            Response_GetDevices res = new Response_GetDevices();
            using (MainContext ct = new MainContext())
            {
                var bd = ct.User_Devices.Join(ct.Devices, it => it.DeviceId, it => it.Id, (userdeive, device) => new { userdeive, device });
                bd = bd.Where(it => it.userdeive.UserId == id);
                if (request.HasGroupId)
                {
                    bd = bd.Where(it => it.userdeive.User_Device_GroupId == request.GroupId);
                }
                if (request.HasTypeId)
                {
                    bd = bd.Where(it => it.device.DeviceTypeId == request.TypeId);
                }
                if (request.HasCursor)
                {
                    bd = bd.Where(it => it.device.Id >= request.Cursor);
                }
                var ls = await bd.ToListAsync();
                var lsx = _cursorUtility.Run(ls, maxcount, (it) => res.Cursor = it.device.Id);
                res.Info.AddRange(lsx.Select(it =>
                    new DeviceWithUserDeviceInfo
                    {
                        Device = new Device.DTODefine.Types.Device()
                        {
                            Id = it.device.Id,
                            DeviceTypeId = it.device.DeviceTypeId,
                            LatestData = it.device.LatestData,
                            LocationStr = it.device.LocationStr,
                            Name = it.device.Name,
                            Status = it.device.Status,
                        },
                        UserDevice = new DTODefine.Types.User_Device()
                        {
                            PControl = it.userdeive.PControl,
                            PData = it.userdeive.PData,
                            PStatus = it.userdeive.PStatus,
                            UserDeviceGroup = it.userdeive.User_Device_GroupId,
                        }
                    }
                ));
            }
            return res;
        }
        public override async Task<Response_GetUserAllDeviceID?> GetUserAllDeviceID(Request_GetUserAllDeviceID request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            long qid = id;
            if (request.HasUserId)
                id = request.UserId;
            using (MainContext ct = new MainContext())
            {
                IQueryable<MyDBContext.Main.User_Device> bd;
                if (qid != id)
                {
                    var count = await ct.User_SFs.Where(it => it.User1Id == id && it.User2Id == qid && it.IsFather).CountAsync();
                    if (count == 0)
                    {
                        context.Status = new Status(StatusCode.PermissionDenied, "只能查询自己和子用户");
                        return null;
                    }
                }
                bd = ct.User_Devices.Where(it => it.UserId == id);
                var r = await bd.Select(it => it.DeviceId).ToListAsync();
                var res = new Response_GetUserAllDeviceID()
                {
                    UserId = id,
                };
                return res;
            }
        }


        public override async Task<Response_GetGroupInfos> GetGroupInfos(Empty request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            Response_GetGroupInfos res = new Response_GetGroupInfos();
            using (MainContext ct = new MainContext())
            {
                var ls = await ct.User_Device_Groups.Where(it => it.CreatorId == id).AsNoTracking().ToListAsync();
                foreach (var item in ls)
                {
                    res.Groups.Add(new DTODefine.Types.User_Device_Group()
                    {
                        Id = item.Id,
                        Name = item.Name,
                    });
                }
            }
            return res;
        }
        public override async Task<CommonResponse> UpdateGroupInfos(Request_UpdateGroupInfos request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                foreach (var item in request.Groups)
                {
                    if (item.HasDelet && item.Delet)
                    {
                        var gp = await ct.User_Device_Groups.Where(it => it.CreatorId == id && it.Id == item.Id).FirstOrDefaultAsync();
                        if (gp == null)
                        {
                            return new CommonResponse()
                            {
                                Success = false,
                                Message = "用户无该分组"
                            };
                        }
                        if (ct.User_Devices.Where(it => it.UserId == id && it.User_Device_GroupId == item.Id).Take(1).Count() > 0)
                        {
                            return new CommonResponse()
                            {
                                Success = false,
                                Message = "该分组存在设备,请先删除设备或移动设备到其他分组"
                            };
                        }
                        ct.User_Device_Groups.Remove(gp);
                    }
                    else
                    {
                        var gp = await ct.User_Device_Groups.Where(it => it.CreatorId == id && it.Id == item.Id).FirstOrDefaultAsync();
                        if (gp == null)
                        {
                            return new CommonResponse()
                            {
                                Success = false,
                                Message = "用户无该分组"
                            };
                        }
                        gp.Name = item.Name;
                    }
                }
                await ct.SaveChangesAsync();
                return new CommonResponse() { Success = true };
            }
        }
        public override async Task<CommonResponse> NewGroup(Request_NewGroup request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                ct.Add(new MyDBContext.Main.User_Device_Group()
                {
                    Name = request.Name,
                    CreatorId = id
                });
                await ct.SaveChangesAsync();
            }
            return new CommonResponse() { Success = true };
        }
        public override async Task<CommonResponse?> SetDeviceGroup(Request_SetDeviceGroup request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                if (0 == await ct.User_Device_Groups.Where(it => it.CreatorId == id).CountAsync())
                {
                    context.Status = new Status(StatusCode.PermissionDenied, "用户无该分组");
                    return null;
                }
                var bd = ct.User_Devices.Where(it => it.UserId == id && request.Dvids.ToList().Contains(it.DeviceId));
                var count = await bd.CountAsync();
                if (count != request.Dvids.Count)
                {
                    context.Status = new Status(StatusCode.PermissionDenied, "尝试为用户没有的设备添加分组或请求包含重复的设备编号");
                    return null;
                }
                if (request.HasGroupId)
                {
                    foreach (var item in await bd.ToListAsync())
                    {
                        item.User_Device_GroupId = request.GroupId;
                    }
                }
                else
                {
                    foreach (var item in await bd.ToListAsync())
                    {
                        item.User_Device_GroupId = 0;
                    }
                }

                await ct.SaveChangesAsync();
                return new CommonResponse() { Success = true };
            }
        }


        public override async Task<Response_GetUserDevices> GetUserDevices(Request_GetUserDevices request, ServerCallContext context)
        {
            int maxcount = 1000 + 1;
            long id = (long)context.UserState["CreatorId"];
            long qid = id;
            if (request.HasUserId)
                id = request.UserId;
            using (MainContext ct = new MainContext())
            {
                IQueryable<MyDBContext.Main.User_Device> bd;
                if (qid != id)
                {
                    var count = await ct.User_SFs.Where(it => it.User1Id == id && it.User2Id == qid && it.IsFather).CountAsync();
                    if (count == 0)
                    {
                        context.Status = new Status(StatusCode.PermissionDenied, "只能查询自己和子用户");
                        return null;
                    }
                }
                bd = ct.User_Devices.Where(it => it.UserId == request.UserId);
                if (request.HasCursor)
                {
                    bd = bd.Where(it => it.DeviceId >= request.Cursor)
                        .Take(maxcount);
                }
                var r = await bd.AsNoTracking().ToListAsync();
                var res = new Response_GetUserDevices()
                {
                    UserId = request.UserId,
                };
                IEnumerable<MyDBContext.Main.User_Device> lsx;
                if (maxcount == r.Count)
                {
                    res.Cursor = r.Last().DeviceId;
                    lsx = r.Take(maxcount - 1);
                }
                else
                {
                    res.Cursor = 0;
                    lsx = r;
                }
                res.UserDevices.AddRange(lsx.Select(it =>
                {
                    return new DTODefine.Types.User_Device()
                    {
                        PControl = it.PControl,
                        PData = it.PData,
                        Dvid = it.DeviceId,
                        PStatus = it.PStatus,
                        UserId = it.UserId,
                    };
                }));
                return res;
            }
        }

    }
}
