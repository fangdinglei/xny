using Grpc.Core;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext;
using MyDBContext.Main;
using MyUtility;
using static GrpcMain.Account.DTODefine.Types;
namespace GrpcMain.Account
{
    public class AccountServiceImp : AccountService.AccountServiceBase
    {
        IGrpcAuthorityHandle _handle;
        ITimeUtility _timeutility;
        public AccountServiceImp(IGrpcAuthorityHandle handle, ITimeUtility time)
        {
            _handle = handle;
            _timeutility = time;
        }

        [GrpcRequireAuthority(NeedLogin = false)]
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
                    //await ct.AddAsync(new MyDBContext.Main. History()
                    //{
                    //    _Type = HistoryType.Login,
                    //    Success=false,
                    //    Time = _timeutility.GetTicket(),
                    //    Data = Newtonsoft.Json.JsonConvert.SerializeObject(
                    //        new
                    //        {
                    //            Success = false,
                    //            Ip = context.Peer,
                    //        }
                    //   )
                    //});
                    //await ct.SaveChangesAsync();
                    return new Response_LoginByUserName()
                    {
                    };
                }
                else
                {//登陆成功
                    ct.Add(new MyDBContext.Main.History()
                    {
                        _Type = HistoryType.Login,
                        Success = true,
                        Time = _timeutility.GetTicket(),
                        CreatorId = user.Id,
                        Data = Newtonsoft.Json.JsonConvert.SerializeObject(
                           new
                           {
                               Success = true,
                               Ip = context.Peer,
                           }
                      )
                    });
                    await ct.SaveChangesAsync();
                }
            }
            var token = _handle.GetToken(
                new MyJwtHelper.TokenClass
                {
                    Id = user.Id
                });
            return new Response_LoginByUserName()
            {
                Token = token
            };
        }

        public override async Task<CommonResponse?> DeletUser(Request_DeletUser request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                var sf = await ct.User_SFs.Where(it => it.User1Id == id && it.User2Id == request.UserId && it.IsFather).AsNoTracking().SingleOrDefaultAsync();
                if (sf == null)
                {
                    context.Status = new Status(StatusCode.PermissionDenied, "没有该子用户");
                    return null;
                }
                throw new Exception("维护中");
                //ct.Remove(sf.Son);
                await ct.SaveChangesAsync();
            }
            return new CommonResponse()
            {
                Success = true,
            };
        }


        public override async Task<CommonResponse?> ChangePassWord(Request_ChangePassWord request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                if (request.HasUid)
                {//改子用户
                    var sf = await ct.User_SFs.Where(it => it.User1Id == id && it.User2Id == request.Uid && it.IsFather)
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


        public override async Task<Response_CreatUser> CreatUser(Request_CreatUser request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                var trans = await ct.Database.BeginTransactionAsync();
                var user = new User()
                {
                    Name = request.Uname,
                    Pass = request.Pass,
                    Phone = request.Phone,
                    CreatorId = id,
                };
                ct.Add(user);
                await ct.SaveChangesAsync();

                var users = await ct.User_SFs.Where(it => it.User1Id == id && it.IsFather || it.IsSelf).Select(it => it.User2Id).ToListAsync();
                foreach (var item in users)
                {
                    ct.Add(new User_SF()
                    {
                        User1Id = item,
                        User2Id = user.Id,
                        IsSelf = false,
                        IsFather = true,
                    });
                    ct.Add(new User_SF()
                    {
                        User1Id = user.Id,
                        User2Id = item,
                        IsSelf = false,
                        IsFather = false,
                    });
                }
                ct.Add(new User_SF()
                {
                    User1Id = user.Id,
                    User2Id = user.Id,
                    IsSelf = true,
                    IsFather = false,
                });
                await ct.SaveChangesAsync();
                await trans.CommitAsync();
                return new Response_CreatUser()
                {
                    UserId = user.Id
                };
            }
        }


        public override async Task<Response_GetUserInfo?> GetUserInfo(Request_GetUserInfo request, ServerCallContext context)
        {
            using (MainContext ct = new MainContext())
            {
                long id = (long)context.UserState["CreatorId"];
                long qid = id;
                if (request.HasUserId && request.UserId != id)
                {
                    qid = request.UserId;
                    if ((await id.GetOwnerTypeAsync(ct, qid)) != AuthorityUtility.OwnerType.SonOfCreator)
                    {
                        context.Status = new Status(StatusCode.PermissionDenied, "只能获取自己的子用户的信息");
                        return null;
                    }

                }
                var list = new List<User>();
                if (request.SubUser)
                {
                    var r = await ct.Users.Where(it => it.Id == qid).AsNoTracking().FirstOrDefaultAsync();
                    list.Add(r);
                    list.AddRange(await ct.Users.Where(it => it.CreatorId == qid).AsNoTracking().ToListAsync());
                }
                else
                {
                    var r = await ct.Users.Where(it => it.Id == qid).AsNoTracking().FirstOrDefaultAsync();
                    list.Add(r);
                }
                var rsp = new Response_GetUserInfo();
                rsp.UserInfo.AddRange(list.Select(it =>
                {
                    return new UserInfo()
                    {
                        Father = it.CreatorId,
                        ID = it.Id,
                        Phone = it.Phone,
                        UserName = it.Name,
                        Email = it.EMail,
                        LastLogin = it.LastLogin,
                        Authoritys = it.Authoritys,
                    };
                }));
                return rsp;
            }

        }


        public override async Task<CommonResponse> UpdateUserInfo(Request_UpdateUserInfo request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                User? user; User? self;
                //鉴权 自己则不能改权限 子用户则
                if (request.UserInfo.ID == id)
                {
                    request.UserInfo.ClearAuthoritys();
                }
                else
                {
                    var sf = await ct.User_SFs.Where(it => it.User1Id == id && it.User2Id == request.UserInfo.ID && it.IsFather)
                      .AsNoTracking().FirstOrDefaultAsync();
                    if (sf == null)
                    {
                        return new CommonResponse()
                        {
                            Success = false,
                            Message = "无该子用户的所有权",
                        };
                    }
                }
                user = await ct.Users.Where(it => it.Id == id).FirstOrDefaultAsync();
                if (user == null)
                {
                    throw new Exception("用户应当不空但是为空");
                }
                if (request.UserInfo.HasUserName)
                {
                    user.Name = request.UserInfo.UserName;
                }
                if (request.UserInfo.HasPhone)
                {
                    user.Phone = request.UserInfo.Phone;
                }
                if (request.UserInfo.HasEmail)
                {
                    user.EMail = request.UserInfo.Email;
                }
                if (request.UserInfo.HasAuthoritys)
                {//修改高级权限
                    List<string>? authorityssubuser;
                    if (user.Authoritys.TryDeserializeObject(out authorityssubuser) && authorityssubuser.Count > 0)
                    {
                        self = await ct.Users.Where(it => it.Id == id).AsNoTracking().FirstOrDefaultAsync();
                        if (self == null)
                        {
                            throw new Exception("用户自己应当不空但是为空");
                        }
                        List<string>? authoritysself;
                        if (!self.Authoritys.TryDeserializeObject(out authoritysself) ||
                            authoritysself == null || authoritysself.Count < authorityssubuser.Count
                            || authorityssubuser.Except(authoritysself).Count() != 0)
                        {
                            return new CommonResponse()
                            {
                                Success = false,
                                Message = "父用户不具有这些权限",
                            };
                        }
                        user.Authoritys = request.UserInfo.Authoritys;
                    }
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
