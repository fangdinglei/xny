using FdlWindows.View;
using Microsoft.Extensions.DependencyInjection;
using MyClient.Grpc;
using MyClient.View;

namespace MyClient
{
    public class Global {
        static public IServiceCollection Collection;
        static  Global()
        {
            var services = Collection = new ServiceCollection();
            services.AddSingleton (services);
          
            services.AddSingleton<FLogin >();
            services.AddSingleton<FMain >();
            services.AddSingleton(new FMainOption() { 
                 Title="智慧农业", 
            });

            var provider = services.BuildServiceProvider( ); 
            services.UserGrpc();
        }
    }
}
