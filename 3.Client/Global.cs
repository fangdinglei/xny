using FdlWindows.View;
using FdlWindows.View.LoginView;
using Grpc.Core;
using GrpcMain.Account;
using Microsoft.Extensions.DependencyInjection;
using MyClient.Grpc;
using MyClient.View;
using MyUtility;

namespace MyClient
{
    public static class Global
    {
        static public IServiceProvider serviceProvider { get; private set; }
        static Global()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ITimeUtility, TimeUtility>();
            services.AddSingleton<LocalDataBase>();
            services.UseGrpc();
            services.UseFMain(new FMainOption()
            {
                Title = "智慧农业",
                CloseCall = () => System.Environment.Exit(0)
            });
            services.UseFLogin(new FLoginOption(async (context, uname, pass) =>
            {
                var client = context.GetService<AccountService.AccountServiceClient>();
                try
                {
                    var a = await client.LoginByUserNameAsync(new DTODefine.Types.Request_LoginByUserName()
                    {
                        PassWord = pass,
                        UserName = uname,
                    });
                    if (a.HasToken)
                    {
                        return a;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }, (context, rsp) =>
            {
                GrpcMain.Account.DTODefine.Types.Response_LoginByUserName a = (DTODefine.Types.Response_LoginByUserName)rsp;
                IClientCallContextInterceptor interceptor = context.GetService<IClientCallContextInterceptor>();
                LocalDataBase db = context.GetService<LocalDataBase>();
                //注册token
                interceptor.SetToken(a.Token);
                db.User = a.User;
                //进入主界面
                FMain? m = context.GetService<FMain>();
                if (m == null)
                {
                    throw new Exception("注入失败");
                }
                m.Show();
            }));
            services.UseFLoading(new FLoadingOption((ex) =>
            {
                if (ex is RpcException ex2)
                    return ex2.Status.Detail;
                else
                    return ex.Message;
            }));
            services.AddSingleton(serviceProvider = services.BuildServiceProvider());
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




    }
}
