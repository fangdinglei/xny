using FdlWindows.View;
using Microsoft.Extensions.DependencyInjection;
using MyClient.Grpc;
using MyClient.View;
using MyUtility;

namespace MyClient
{
    public static class Global
    {
        static public IServiceProvider serviceProvider;
        static Global()
        {
            var services = new ServiceCollection();
            services.AddSingleton<ITimeUtility, TimeUtility>();
            services.UseFMain();
            services.AddSingleton(new FMainOption()
            {
                Title = "智慧农业",
                CloseCall = () => System.Environment.Exit(0)
            });
            services.UseGrpc();
            services.AddSingleton<LocalDataBase>();
            services.AddSingleton<FLogin>();

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
