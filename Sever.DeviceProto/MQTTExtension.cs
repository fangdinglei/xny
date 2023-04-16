using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Sever.DeviceProto
{
    static public class MQTTExtension
    {
        static public void UseMQTT(this IServiceCollection services)
        {
            services.TryAddSingleton<IDeviceMessageHandle, DeviceMessageManager>();
            services.TryAddSingleton<IProto, MQTTSeverClient>();
        }

        static public void StartMQTT(this IServiceProvider app)
        {
            IProto proto = app.GetService<IProto>();
        }
    }
}