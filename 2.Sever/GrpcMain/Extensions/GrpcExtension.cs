using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcMain.Account;
using GrpcMain.AccountHistory;
using GrpcMain.Attributes;
using GrpcMain.Device;
using GrpcMain.DeviceData;
using GrpcMain.DeviceType;
using GrpcMain.Interceptors;
using GrpcMain.InternalMail;
using GrpcMain.System;
using GrpcMain.UserDevice;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MyDBContext.Main;
using MyJwtHelper;
using MyUtility;
using System.Reflection;
using Type = System.Type;

namespace GrpcMain.Extensions
{
    static public class MyGrpcExtension
    {
        static public void UseMyGrpc(this IServiceCollection services, string jwtkey)
        {
            services.Configure<MyGrpcHandleCongig>(op => op.JwtKey = jwtkey);
            services.TryAddSingleton<IJwtHelper, JwtHelper>();
            services.TryAddSingleton<IGrpcCursorUtility, GrpcCursorUtilityImp>();
            services.UseColdData();
            //添加处理器
            services.TryAddSingleton<IGrpcAuthorityHandle, MyGrpcHandle>();
            //开启GRPC
            services.AddGrpc(op =>
            {
                //全局错误处理 
                op.Interceptors.Add<GrpcInterceptor>();
                op.EnableDetailedErrors = true;
            });
            //获取方法属性和路由信息
            Dictionary<string, MyGrpcMethodAttribute> dic = new Dictionary<string, MyGrpcMethodAttribute>();
            foreach (var item in typeof(IGrpcAuthorityHandle).Assembly.GetTypes())
            {
                if (item.BaseType == null)
                    continue;
                var att = item.BaseType.GetCustomAttribute<BindServiceMethodAttribute>();
                if (att == null)
                    continue;
                foreach (var func in item.GetMethods())
                {
                    if (!func.IsVirtual)
                        continue;
                    var atx = func.GetCustomAttribute<MyGrpcMethodAttribute>();
                    if (atx == null)
                        continue;
                    var str = "/" + att.BindType.GetField("__ServiceName", BindingFlags.Static | BindingFlags.NonPublic).GetValue(att.BindType) + "/" + func.Name;
                    dic.Add(str, atx);
                }
            }
            services.TryAddSingleton(dic);

            //Test 测试
            using (MainContext ct = new MainContext())
            {
                ct.Database.EnsureCreated();
            }
            Console.Clear();
        }
        /// <summary>
        /// 注册grpc服务到路由
        /// </summary>
        /// <param name="app"></param>
        static public void RegistMyGrpc(this WebApplication app)
        {
            var addmethod = typeof(GrpcEndpointRouteBuilderExtensions).GetMethod("MapGrpcService");
            foreach (var item in typeof(IGrpcAuthorityHandle).Assembly.GetTypes())
            {
                if (item.BaseType == null)
                    continue;
                var att = item.BaseType.GetCustomAttribute<BindServiceMethodAttribute>();
                if (att == null)
                    continue;
                _ = addmethod.MakeGenericMethod(item).Invoke(null, new object[] { app as IEndpointRouteBuilder });
            }
            //app.MapGrpcService<AccountServiceImp>();
            //app.MapGrpcService<DeviceServiceImp>();
            //app.MapGrpcService<UserDeviceServiceImp>();
            //app.MapGrpcService<DeviceDataServiceImp>();
            //app.MapGrpcService<DeviceTypeServiceImp>();
            //app.MapGrpcService<InternalMailServiceImp>();
            //app.MapGrpcService<AccountHistoryServiceImp>();
            //app.MapGrpcService<RepairServiceImp>();
            //app.MapGrpcService<SystemServiceImp>();
        }

        static public bool TryDeserializeObject<T>(this string json, out T? obj) where T : class
        {

            if (string.IsNullOrWhiteSpace(json))
            {
                obj = null;
                return false;
            }
            try
            {
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
                return obj != null;
            }
            catch (Exception)
            {
                obj = null;
                return false;
            }
        }


        internal class MyGrpcHandle : IGrpcAuthorityHandle
        {
            MyGrpcHandleCongig _config;
            IJwtHelper _jwtHelper;
            ITimeUtility _timeUtility;
            public MyGrpcHandle(IOptions<MyGrpcHandleCongig> op, IJwtHelper helper, ITimeUtility timeUtility)
            {
                _config = op.Value;
                if (string.IsNullOrWhiteSpace(_config.JwtKey))
                {
                    _config.JwtKey = "2432114474";
                }
                _jwtHelper = helper;
                _timeUtility = timeUtility;
            }

            public async Task<(bool, string?)> Authorize(ServerCallContext context, MyGrpcMethodAttribute att)
            {
                object token;
                var et = context.RequestHeaders.Get("Token");
                if (et == null || (token = et.Value) == null)
                {
                    return (false, "请添加Token再访问");
                }
                var tokenstr = (string)token;
                var jwt = _jwtHelper.Get<TokenClass>(tokenstr);
                if (jwt == null)
                {
                    return (false, "token无效");
                }
                using MainContext ct = new MainContext();
                var select = await ct.Users.Where(it => it.Id == jwt.Id).Select(it => new { it.Id, it.Status, it.UserTreeId, it.Authoritys }).FirstOrDefaultAsync();
                if (select == null)
                {
                    throw new RpcException(new Status(StatusCode.Unauthenticated, "登陆过时"));
                }
                User user = new User()
                {
                    Id = select.Id,
                    Status = select.Status,
                    Authoritys = select.Authoritys,
                    UserTreeId = select.UserTreeId,
                };


                if (att != null && att.Authoritys != null && att.Authoritys.Count() > 0)
                {//校验高级权限
                    var authoritys = user.Authoritys;
                    List<string>? authoritysx;
                    if (authoritys == null || !authoritys.TryDeserializeObject(out authoritysx) || authoritysx == null)
                    {
                        return (false, "需要高级权限 " + att.Authoritys[0]);
                    }
                    var nothave = att.Authoritys.Except(authoritysx);
                    if (nothave.Count() != 0)
                    {
                        return (false, "需要高级权限 " + nothave.First());
                    }
                }
                context.UserState["CreatorId"] = jwt.Id;
                context.UserState["user"] = user;
                return (true, null);
            }

            public string GetToken(TokenClass tokenClass)
            {
                return _jwtHelper.Get(tokenClass, _config.JwtKey);
            }

            public void OnError(Exception e)
            {
                return;
            }

            public async Task RecordAudit<TRequest, TResponse>(ServerCallContext context, object request, UnaryServerMethod<TRequest, TResponse> continuation, MyGrpcMethodAttribute att, User user)
                where TResponse : class
                where TRequest : class
            {
                long Sponsorid = (long)context.UserState["CreatorId"];
                long AuditorId = (long)context.UserState["AuditorId"];
                using (MainContext ct = new MainContext())
                {
                    ct.User_Op_Audits.Add(
                        new User_Op_Audit()
                        {
                            SponsorId = Sponsorid,
                            AuditorId = AuditorId,
                            Time = _timeUtility.GetTicket(),
                            Op = att.NeedAudit_OpName,
                            Data = Newtonsoft.Json.JsonConvert.SerializeObject(request),
                            UserTreeId = user.UserTreeId,
                        }
                        ); ;
                    await ct.SaveChangesAsync();
                }
            }
        }

        internal class MyGrpcHandleCongig
        {
            public string JwtKey { get; set; } = "12331";
        }

    }
}
