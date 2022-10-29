using Grpc.Core;
using GrpcMain.Common;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
using static GrpcMain.Account.DTODefine.Types;

namespace GrpcMain.Account
{
    static public class Ext
    {
        static public DTODefine.Types.UserInfo AsGrpcObj(this User user,bool haspass=false)
        {
            return new DTODefine.Types.UserInfo()
            {
                Authoritys = user.Authoritys,
                Email = user.EMail,
                Father = user.CreatorId,
                ID = user.Id,
                LastLogin = user.LastLogin,
                Phone = user.Phone,
                UserName = user.Name,
                Status = user.Status,
                TreeId = user.TreeId,
                PassWord = haspass? user.Pass:"",
            };
        }

        static public User AsDBObj(this DTODefine.Types.UserInfo user)
        {
            return new User
            {
                Id = user.ID,
                Authoritys = user.Authoritys,
                CreatorId = user.Father,
                Name = user.UserName,
                EMail = user.Email,
                LastLogin = user.LastLogin,
                Pass = user.PassWord,
                Phone = user.Phone,
                TreeId = user.TreeId,
                Status = (byte)user.Status,
            };
        }
   
    }
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
                //登陆失败
                if (user == null)
                {
                    return new Response_LoginByUserName()
                    {
                    };
                }
                //登陆成功
                ct.Add(new MyDBContext.Main.AccountHistory()
                {
                    _Type = AccountHistoryType.Login,
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
            var token = _handle.GetToken(
                new MyJwtHelper.TokenClass
                {
                    Id = user.Id
                });
            return new Response_LoginByUserName()
            {
                Token = token,
                User = user.AsGrpcObj()
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
                    throw new RpcException(new Status(StatusCode.PermissionDenied, "没有该子用户"));
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

        #region ChangePassWord
        async Task<CommonResponse> ChangePassWord_SubUserAsync(Request_ChangePassWord request,  MainContext ct, long fatherid) {
            var sf = await ct.User_SFs.Where(it => it.User1Id == fatherid && it.User2Id == request.Uid && it.IsFather)
                        .AsNoTracking().FirstOrDefaultAsync();
            if (sf == null)
            {
                throw new RpcException(new Status(StatusCode.PermissionDenied, "无该子用户的所有权"));
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
        async Task<CommonResponse> ChangePassWord_SelfAsync(Request_ChangePassWord request, MainContext ct, long id)
        {
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
        public override async Task<CommonResponse?> ChangePassWord(Request_ChangePassWord request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                if (request.HasUid)
                {//改子用户
                    return await ChangePassWord_SubUserAsync(request,ct,id);
                }
                else
                {//改自己
                    return await ChangePassWord_SelfAsync(request, ct, id); 
                }
            }
        }
        #endregion

        #region CreatUser
        async Task CreatUser_AddUserSFAsync(MainContext ct,long fatheruserid,long uid) {
            var users = await ct.User_SFs.Where(it => it.User1Id == fatheruserid && (it.IsFather || it.IsSelf)).Select(it => it.User2Id).ToListAsync();
            foreach (var item in users)
            {
                ct.Add(new User_SF()
                {
                    User1Id = item,
                    User2Id = uid,
                    IsSelf = false,
                    IsFather = true,
                });
                ct.Add(new User_SF()
                {
                    User1Id = uid,
                    User2Id = item,
                    IsSelf = false,
                    IsFather = false,
                });
            }
            ct.Add(new User_SF()
            {
                User1Id = uid,
                User2Id = uid,
                IsSelf = true,
                IsFather = false,
            });
        }
        public override async Task<Response_CreatUser> CreatUser(Request_CreatUser request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            using (MainContext ct = new MainContext())
            {
                var trans = await ct.Database.BeginTransactionAsync();
                var me=ct.Users.Where(it=>it.Id==id).FirstOrDefaultAsync();
                if (me != null)
                    throw new Exception("数据库不一致"+me.Id+"应当存在却不存在");
                var user = request.User.AsDBObj();
                user.CreatorId = id;
                ct.Add(user);
                await ct.SaveChangesAsync();
                await CreatUser_AddUserSFAsync(ct,user.Id,id);
                await ct.SaveChangesAsync();
                await trans.CommitAsync();
                return new Response_CreatUser()
                {
                    UserId = user.Id
                };
            }
        }
        #endregion


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
                        throw new RpcException(new Status(StatusCode.PermissionDenied, "只能获取自己的子用户的信息"));
                    }

                }

                var list = new List<User>();
                var r = await ct.Users.Where(it => it.Id == qid).AsNoTracking().FirstOrDefaultAsync();
                list.Add(r);
                if (request.SubUser)
                    list.AddRange(await ct.Users.Where(it => it.CreatorId == qid).AsNoTracking().ToListAsync());
                var rsp = new Response_GetUserInfo();
                rsp.UserInfo.AddRange(list.Select(it =>it.AsGrpcObj()));
                return rsp;
            }

        }


        async Task UpdateUserInfo_ChangeAuthoritysAsync(MainContext ct,User son,long fatherid,string newauthority) {
            List<string>? authorityssubuser;
            if (newauthority.TryDeserializeObject(out authorityssubuser) && authorityssubuser.Count > 0)
            {
                var father = await ct.Users.Where(it => it.Id == fatherid).AsNoTracking().FirstOrDefaultAsync();
                if (father == null)
                {
                    throw new Exception("用户自己应当不空但是为空");
                }
                List<string>? authoritysself;
                if (!father.Authoritys.TryDeserializeObject(out authoritysself) ||
                    authoritysself == null || authoritysself.Count < authorityssubuser.Count
                    || authorityssubuser.Except(authoritysself).Count() != 0)
                {
                    throw new RpcException(new Status(StatusCode.PermissionDenied, "父用户不具有这些权限"));
                }
                son.Authoritys = newauthority;
            }
        }
        public override async Task<Response_UpdateUserInfo> UpdateUserInfo(Request_UpdateUserInfo request, ServerCallContext context)
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
                        context.Status = new Status( StatusCode.PermissionDenied, "无该子用户的所有权");
                        return null;
                    }
                }
                user = await ct.Users.Where(it => it.Id == id).FirstOrDefaultAsync();
                if (user == null)
                {
                    throw new Exception("用户应当不空但是为空");
                }
                //if (request.UserInfo.HasUserName)
                //{
                //    user.Name = request.UserInfo.UserName;
                //}
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
                    await UpdateUserInfo_ChangeAuthoritysAsync(ct, user, id,request.UserInfo.Authoritys);
                }
                await ct.SaveChangesAsync();
                return new Response_UpdateUserInfo() { 
                    UserInfo=user.AsGrpcObj()
                };
            }
        }

    }
}
