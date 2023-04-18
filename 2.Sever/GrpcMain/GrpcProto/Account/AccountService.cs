using BaseDefines;
using Grpc.Core;
using GrpcMain.Attributes;
using GrpcMain.Common;
using GrpcMain.Extensions;
using Microsoft.EntityFrameworkCore;
using MyDBContext.Main;
using MyUtility;
using System.Reflection;
using static GrpcMain.Account.DTODefine.Types;

namespace GrpcMain.Account
{
    static public class Ext
    {
        static public DTODefine.Types.UserInfo AsGrpcObj(this User user, bool haspass = false)
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
                TreeId = user.UserTreeId,
                PassWord = haspass ? user.Pass : "",
                MaxSubUser = user.MaxSubUser,
                TreeDeep = user.TreeDeep,
                MaxSubUserDepth = user.MaxSubUserDepth,
            };
        }

        static public User AsDBObj(this DTODefine.Types.UserInfo user)
        {
            return new User
            {
                Id = user.ID,
                Authoritys = user.HasAuthoritys ? user.Authoritys : "[]",
                CreatorId = user.HasFather ? user.Father : 0,
                Name = user.UserName,
                EMail = user.HasEmail ? user.Email : "",
                LastLogin = 0,
                Pass = user.PassWord,
                Phone = user.HasPhone ? user.Phone : "",
                UserTreeId = user.TreeId,
                Status = 0,
                MaxSubUser = user.HasMaxSubUser ? user.MaxSubUser : 100,
                MaxSubUserDepth = user.HasMaxSubUserDepth ? user.MaxSubUserDepth : 10,
                TreeDeep = user.HasTreeDeep ? user.TreeDeep : 1,
            };
        }

    }
    public class AccountServiceImp : AccountService.AccountServiceBase
    {
        /*
         it =>it.User1Id == id && it.IsFather 查询的User2Id为子用户
         it =>it.User1Id == id && !it.IsFather 查询的User2Id为父用户和自己
         it =>it.User2Id == id && it.IsFather 查询的User1Id为父用户
         it =>it.User2Id == id && !it.IsFather 查询的User1Id子用户和自己
         */
        IGrpcAuthorityHandle _handle;
        ITimeUtility _timeutility;
        public AccountServiceImp(IGrpcAuthorityHandle handle, ITimeUtility time)
        {
            _handle = handle;
            _timeutility = time;
        }

        [MyGrpcMethod(NeedLogin = false, NeedDB = true)]
        public override async Task<Response_Login>
            Login(
            Request_Login request, ServerCallContext context)
        {
            var ct = (MainContext)context.UserState[nameof(MainContext)];
            User? user;
            user = await ct.Users.Where(it => it.Id == request.Id
            && it.Pass == request.PassWord).AsNoTracking().FirstOrDefaultAsync();
            //登陆失败
            if (user == null)
            {
                return new Response_Login()
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
                ),
                UserTreeId = user.UserTreeId
            });
            await ct.SaveChangesAsync();
            var token = _handle.GetToken(
                new MyJwtHelper.TokenClass
                {
                    Id = user.Id
                });
            return new Response_Login()
            {
                Token = token,
                User = user.AsGrpcObj()
            };
        }

        public override Task<Response_LoginByToken> LoginByToken(Request_LoginByToken request, ServerCallContext context)
        {
            long id = (long)context.UserState["CreatorId"];
            var token = _handle.GetToken(
                new MyJwtHelper.TokenClass
                {
                    Id = id
                });
            return Task.FromResult(new Response_LoginByToken()
            {
                Token = token,
            });
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
        async Task<CommonResponse> ChangePassWord_SubUserAsync(Request_ChangePassWord request, MainContext ct, long fatherid)
        {
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
                    return await ChangePassWord_SubUserAsync(request, ct, id);
                }
                else
                {//改自己
                    return await ChangePassWord_SelfAsync(request, ct, id);
                }
            }
        }
        #endregion

        #region CreatUser
        async Task CreatUser_AddUserSFAsync(MainContext ct, long fatheruserid, long uid, int usertreeid)
        {
            var users = await ct.User_SFs.Where(it => it.User1Id == fatheruserid && (it.IsFather || it.IsSelf)).Select(it => it.User2Id).ToListAsync();
            foreach (var item in users)
            {
                ct.Add(new User_SF()
                {
                    User1Id = item,
                    User2Id = uid,
                    IsSelf = false,
                    IsFather = true,
                    UserTreeId = usertreeid,
                });
                ct.Add(new User_SF()
                {
                    User1Id = uid,
                    User2Id = item,
                    IsSelf = false,
                    IsFather = false,
                    UserTreeId = usertreeid
                });
            }
            ct.Add(new User_SF()
            {
                User1Id = uid,
                User2Id = uid,
                IsSelf = true,
                IsFather = false,
                UserTreeId = usertreeid
            });
        }


        [MyGrpcMethod(NeedDB = true, NeedTransaction = true)]
        public override async Task<Response_CreatUser> CreatUser(Request_CreatUser request, ServerCallContext context)
        {
            var ct = (MainContext)context.UserState[nameof(MainContext)];
            long id = (long)context.UserState["CreatorId"];
            var me = await ct.Users.Where(it => it.Id == id).FirstOrDefaultAsync();
            if (me == null)
                throw new Exception("数据库不一致" + id + "应当存在却不存在");

            //最大子用户深度校验
            var maxdeep = await ct.Users.Join(ct.User_SFs, it => it.Id, it => it.User1Id, (a, b) => new { maxdep = a.MaxSubUserDepth, b.User1Id, b.IsFather })
                .Where(it => !it.IsFather && it.User1Id == id).MinAsync(it => it.maxdep);
            if (maxdeep < me.TreeDeep + 1)
                throw new RpcException(new Status(StatusCode.PermissionDenied, "最大用户深度为" + me.MaxSubUserDepth + ",该限制可能是父用户对您的限制也可能是父用户受到的限制"));
            //最大子用户校验 查找父用户和自己 为集合  如果集合中所有用户的子用户数量都小于其受到的限制 则允许插入
            var __ct = await ct.Users.Join(ct.User_SFs, it => it.Id, it => it.User1Id, (a, b) => new { maxuser = a.MaxSubUser, b.User1Id, selfAndFather = b.User2Id, b.IsFather })
                .Where(it => !it.IsFather && it.User1Id == id
                && (ct.User_SFs.Where(sub => sub.User1Id == it.selfAndFather && sub.IsFather).Count() >= it.maxuser)
                ).Take(1).CountAsync();
            var canadd = __ct == 0;
            if (!canadd)
            {
                throw new RpcException(new Status(StatusCode.PermissionDenied, "超出最大用户数量限制,该限制可能是父用户对您的限制也可能是父用户受到的限制"));
            }


            var user = request.User.AsDBObj();
            user.CreatorId = id;
            user.UserTreeId = me.UserTreeId;
            user.TreeDeep = me.TreeDeep + 1;
            ct.Add(user);
            await ct.SaveChangesAsync();
            await CreatUser_AddUserSFAsync(ct, id, user.Id, me.UserTreeId);
            await ct.SaveChangesAsync();
            return new Response_CreatUser()
            {
                UserId = user.Id
            };
        }

        private string buildAuthoritys()
        {
            var ls = new List<UserAuthorityEnum>();
            foreach (var v in Enum.GetValues<UserAuthorityEnum>())
            {
                if (((int)v & 4) ==1)
                {
                    ls.Add(v);
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(ls.Select(it => it.ToString()));
        }
        [MyGrpcMethod(Authoritys = new string[] { nameof(UserAuthorityEnum.TopUserAdd) }, NeedDB = true, NeedTransaction = true)]
        public override async Task<Response_CreatUser> CreatTopUser(Request_CreatUser request, ServerCallContext context)
        {
            var ct = (MainContext)context.UserState[nameof(MainContext)];
            long id = (long)context.UserState["CreatorId"];
            var me = await ct.Users.Where(it => it.Id == id).FirstOrDefaultAsync();
            if (me == null)
                throw new Exception("数据库不一致" + id + "应当存在却不存在");

            var user = request.User.AsDBObj();
            user.CreatorId = 0;
            user.UserTreeId = await ct.Users.MaxAsync(it => it.UserTreeId) + 1;
            user.TreeDeep = 1;
            user.Authoritys = buildAuthoritys();
            ct.Add(user);
            await ct.SaveChangesAsync();
            await CreatUser_AddUserSFAsync(ct, 0, user.Id, me.UserTreeId);
            await ct.SaveChangesAsync();
            return new Response_CreatUser()
            {
                UserId = user.Id
            };
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
                rsp.UserInfo.AddRange(list.Select(it => it.AsGrpcObj()));
                return rsp;
            }

        }


        async Task UpdateUserInfo_ChangeAuthoritysAsync(MainContext ct, User son, long fatherid, string newauthority)
        {
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
            bool updateself = request.UserInfo.ID == id;
            using (MainContext ct = new MainContext())
            {
                User? user; User? self;
                //鉴权 自己则不能改权限 子用户则
                if (updateself)
                {
                    request.UserInfo.ClearAuthoritys();
                }
                else
                {
                    var sf = await ct.User_SFs.Where(it => it.User1Id == id && it.User2Id == request.UserInfo.ID && it.IsFather)
                      .AsNoTracking().FirstOrDefaultAsync();
                    if (sf == null)
                    {
                        throw new RpcException(new Status(StatusCode.PermissionDenied, "无该子用户的所有权"));
                    }
                }
                self = await ct.Users.Where(it => it.Id == id).FirstOrDefaultAsync();
                user = await ct.Users.Where(it => it.Id == request.UserInfo.ID).FirstOrDefaultAsync();
                if (self == null || user == null)
                {
                    throw new Exception("不一致:用户应当不空但是为空");
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
                    await UpdateUserInfo_ChangeAuthoritysAsync(ct, user, id, request.UserInfo.Authoritys);
                }

                if (!updateself)
                {
                    if (request.UserInfo.HasMaxSubUser)
                    {
                        user.MaxSubUser = request.UserInfo.MaxSubUser;
                    }
                    if (request.UserInfo.HasMaxSubUserDepth)
                    {
                        user.MaxSubUserDepth = request.UserInfo.MaxSubUserDepth;
                    }
                }

                await ct.SaveChangesAsync();
                return new Response_UpdateUserInfo()
                {
                    UserInfo = user.AsGrpcObj()
                };
            }
        }

    }
}
