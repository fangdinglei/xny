using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MyClient.View
{
    static public class FLoadingExt
    {
        static public void UseFLoading(this IServiceCollection serviceCollection, FLoadingOption option)
        {
            serviceCollection.TryAddTransient<FLoading>();
            serviceCollection.TryAddSingleton(option);
        }
    }

}
