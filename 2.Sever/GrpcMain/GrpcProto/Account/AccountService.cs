using Grpc.Core;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
using static GrpcMain.Account.AccountServiceTypes.Types;
namespace GrpcMain.Account
{
    public class AccountServiceImp : AccountService.AccountServiceBase
    {
        IGrpcHandle _handle;
        ITimeUtility _timeutility;
        public AccountServiceImp(IGrpcHandle handle, ITimeUtility time)
        {
            _handle = handle;
            _timeutility = time;
        }

        public override async Task<Response_LoginByUserName?>
            LoginByUserName(
            Request_LoginByUserName request, ServerCallContext context)
        {
            User? user;
            using (MainContext ct = new MainContext())
            {
                user = await ct.Users.Where(it => it.Name == request.UserName
                && it.Pass == request.PassWord).AsNoTracking().FirstOrDefaultAsync();
                if (user == null)
                {//登陆失败
                    await ct.AddAsync(new User_Login_History()
                    {
                        Success = false,
                        Ip = context.Peer,
                        Time = _timeutility.GetTicket()
                    });
                    await ct.SaveChangesAsync();
                    return new Response_LoginByUserName()
                    {
                        Token = null
                    };
                }
                else
                {//登陆成功
                    await ct.AddAsync(new User_Login_History()
                    {
                        Success = true,
                        Ip = context.Peer,
                        Time = _timeutility.GetTicket()
                    });
                }
            }
            var token = _handle.GetToken(new Dictionary<string, object> {
                    { "UserId",user.Id}
                });
            return new Response_LoginByUserName()
            {
                Token = token
            };
        }

        public override async Task<CommonResponse?> DeletUser(Request_DeletUser request, ServerCallContext context)
        {
            long id = (long)context.UserState["UserId"];
            using (MainContext ct = new MainContext())
            {
                var sf = await ct.User_SFs.Where(it => it.FatherId == id && it.SonId == request.UserId).AsNoTracking().SingleOrDefaultAsync();
                if (sf == null)
                {
                    context.Status = new Status(StatusCode.PermissionDenied, "没有该子用户");
                    return null;
                }
                ct.Remove(sf.Son);
                await ct.SaveChangesAsync();
            }
            return new CommonResponse()
            {
                Success = true,
            };
        }
        public override async Task<CommonResponse?> ChangePassWord(Request_ChangePassWord request, ServerCallContext context)
        {
            long id = (long)context.UserState["UserId"];
            using (MainContext ct = new MainContext())
            {
                if (request.HasUid)
                {//改子用户
                    var sf = await ct.User_SFs.Where(it => it.FatherId == id && it.SonId == request.Uid)
                        .AsNoTracking().FirstOrDefaultAsync();
                    if (sf == null)
                    {
                        context.Status = new Status(StatusCode.PermissionDenied, "无该子用户的所有权");
                        return null;
                    }
                    var user = await ct.Users.Where(it => it.Id == request.Uid
                     && it.Pass == request.New).FirstOrDefaultAsync();
                    if (user == null)
                    {
                        throw new Exception("用户应当存在而不存在");
                    }
                    user.Pass = request.New;
                    await ct.SaveChangesAsync();
                    return new CommonResponse()
                    {
                        Success = true
                    };
                }
                else
                {//改自己
                    var user = await ct.Users.Where(it => it.Id == id
                        && it.Pass == request.Old).FirstOrDefaultAsync();
                    if (user == null)
                    {//密码错误
                        return new CommonResponse()
                        {
                            Success = false,
                            Message = "密码错误",
                        };
                    }
                    user.Pass = request.New;
                    await ct.SaveChangesAsync();
                    return new CommonResponse()
                    {
                        Success = true
                    };
                }



            }
        }
        //todo 事务
        public override async Task<Response_CreatUser> CreatUser(Request_CreatUser request, ServerCallContext context)
        {
            long id = (long)context.UserState["UserId"];
            using (MainContext ct = new MainContext())
            {
                var trans=await ct.Database.BeginTransactionAsync();
                var user = new User()
                {
                    Name = request.Uname,
                    Pass = request.Pass,
                    Phone = request.Phone,
                };
                ct.Add(user);
                await ct.SaveChangesAsync();

                ct.Add(new User_SF()
                {
                    FatherId = id,
                    SonId = user.Id,
                });
                await ct.SaveChangesAsync();
                trans.Commit();
                return new Response_CreatUser()
                {
                    UserId = user.Id
                };
            }
        }
        public override Task<Response_GetUserInfo> GetUserInfo(Request_GetUserInfo request, ServerCallContext context)
        {
            throw new NotImplementedException();
            return base.GetUserInfo(request, context);

        }
        public override async Task<CommonResponse> UpdateUserInfo(Request_UpdateUserInfo request, ServerCallContext context)
        {
            long id = (long)context.UserState["UserId"];
            using (MainContext ct = new MainContext())
            {
                User? user;
                //鉴权
                if (request.UserInfo.ID != id)
                {
                    var sf = await ct.User_SFs.Where(it => it.FatherId == id && it.SonId == request.UserInfo.ID)
                       .AsNoTracking().FirstOrDefaultAsync();
                    if (sf == null)
                    {
                        context.Status = new Status(StatusCode.PermissionDenied, "无该子用户的所有权");
                        return null;
                    }
                }
                user = await ct.Users.Where(it => it.Id == id).FirstOrDefaultAsync();
                if (user == null)
                {
                    throw new Exception("用户应当不空但是为空");
                }
                if (string.IsNullOrEmpty(request.UserInfo.UserName))
                {
                    user.Name = request.UserInfo.UserName;
                }
                if (string.IsNullOrEmpty(request.UserInfo.Phone))
                {
                    user.Phone = request.UserInfo.Phone;
                }
                await ct.SaveChangesAsync();
                return new CommonResponse
                {
                    Success = true,
                };
            }
        }

    }
}
