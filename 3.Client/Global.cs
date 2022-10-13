using FdlWindows.View;
using Microsoft.Extensions.DependencyInjection; 
using MyClient.View;

namespace MyClient
{
    public class Global {
       static public  IServiceProvider Provider;
        static  Global()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IServiceCollection >(services);
            var provider = services.BuildServiceProvider();
            services.AddSingleton<IServiceProvider>(provider);
            Provider = provider;
            services.AddTransient<FLogin, FLogin>();
            services.AddTransient<FMain, FMain>();
            services.AddSingleton(new FMainOption() { 
                 Title="智慧农业", 
            }); 
        }
    }
}
