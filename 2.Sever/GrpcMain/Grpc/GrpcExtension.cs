using Grpc.Core;
using GrpcMain;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MyJwtHelper;

namespace PgGrpcMain
{

    static public class MyGrpcExtension
    {
        static public void UseMyGrpc(this IServiceCollection services, string jwtkey) {
            services.Configure<MyGrpcHandleCongig>(op=>op.JwtKey= jwtkey);
            services.TryAddSingleton<IJwtHelper, JwtHelper>();

            //添加处理器
            services.TryAddSingleton<IGrpcHandle, MyGrpcHandle>();
            //开启GRPC
            services.AddGrpc(op => {
                //全局错误处理
                op.Interceptors.Add<GrpcInterceptor>();
                op.EnableDetailedErrors = true;
            });
        }

        internal class MyGrpcHandle : IGrpcHandle
        {
            MyGrpcHandleCongig _config;
            public MyGrpcHandle(IOptions<MyGrpcHandleCongig> op)
            {
                _config=op.Value;
                if (string.IsNullOrWhiteSpace(_config.JwtKey))
                {
                    _config.JwtKey = "2432114474";
                }
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
                //TODO check token check Authorize
                var canvisit = false;
                error = "";
                return canvisit;
            }

            public void OnError(Exception e)
            {
                return;
            }
        }

        internal class MyGrpcHandleCongig{
            public string JwtKey { get; set; } = "12331";
        }

    }
}
