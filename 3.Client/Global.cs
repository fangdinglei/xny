using FdlWindows.View;
using Microsoft.Extensions.DependencyInjection;
using MyClient.Grpc;
using MyClient.View;
using MyUtility;

namespace MyClient
{
    public static class Global {
        static public IServiceCollection Collection;
        static  Global()
        {
            var services = Collection = new ServiceCollection();
            services.AddSingleton (services);
          
            services.AddSingleton<FLogin >();
            services.AddSingleton<ITimeUtility,TimeUtility>();
            services.AddSingleton<FMain >();
            services.AddSingleton(new FMainOption() { 
                 Title="智慧农业", 
                 CloseCall=()=> System.Environment.Exit(0)
            });

            var provider = services.BuildServiceProvider( ); 
            services.UserGrpc();
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
