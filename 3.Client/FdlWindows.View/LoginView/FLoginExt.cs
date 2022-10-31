using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FdlWindows.View.LoginView
{
    static public class FLoginExt
    {
        static public void UseFLogin(this IServiceCollection serviceCollection, FLoginOption option)
        {
            serviceCollection.TryAddTransient<FLogin>();
            serviceCollection.TryAddSingleton(option);
        }
    }
}
