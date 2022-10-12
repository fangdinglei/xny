using Grpc.Core;
using GrpcMain;
using GrpcMain.Account;
using GrpcMain.Common;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MyDBContext.Main;
using MyJwtHelper;
using MyUtility;

namespace PgGrpcMain
{
    static public class MyGrpcExtension
    {
        static public void UseMyGrpc(this IServiceCollection services, string jwtkey)
        {
            services.Configure<MyGrpcHandleCongig>(op => op.JwtKey = jwtkey);
            services.TryAddSingleton<IJwtHelper, JwtHelper>();
            services.TryAddSingleton<IGrpcCursorUtility, GrpcCursorUtilityImp>();

            //添加处理器
            services.TryAddSingleton<IGrpcHandle, MyGrpcHandle>();
            //开启GRPC
            services.AddGrpc(op =>
            {
                //全局错误处理
                op.Interceptors.Add<GrpcInterceptor>();
                op.EnableDetailedErrors = true;
            });
            services.AddGrpcClient<AccountServiceImp>();
        }
        /// <summary>
        /// 注册grpc服务到路由
        /// </summary>
        /// <param name="app"></param>
        static public void RegistMyGrpc(this WebApplication app)
        {
            app.MapGrpcService<AccountServiceImp>();
        }


        internal class MyGrpcHandle : IGrpcHandle
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

            public bool Authorize(ServerCallContext context, GrpcRequireAuthorityAttribute att, out string error)
            {
                object token;
                if ((token = context.RequestHeaders.Get("Token")) == null)
                {
                    error = "请添加Token再访问";
                    return false;
                }
                var tokenstr = (string)token;
                var jwt = _jwtHelper.Get<TokenClass>(tokenstr);
                if (jwt == null)
                {
                    error = "token无效";
                    return false;
                }

                context.UserState["CreatorId"] = jwt.Id;
                error = "";
                return true;
            }

            public string GetToken(TokenClass tokenClass)
            {
                return _jwtHelper.Get(tokenClass, _config.JwtKey);
            }

            public void OnError(Exception e)
            {
                return;
            }

            public async Task RecordAudit<TRequest, TResponse>(ServerCallContext context, object request, UnaryServerMethod<TRequest, TResponse> continuation, GrpcRequireAuthorityAttribute att)
                where TRequest : class
                where TResponse : class
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
                            Data = Newtonsoft.Json.JsonConvert.SerializeObject(request)
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
