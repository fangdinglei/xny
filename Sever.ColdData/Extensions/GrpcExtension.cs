

using Microsoft.Extensions.DependencyInjection;
using Sever.ColdData;
using Sever.ColdData.Imp;

namespace GrpcMain.Extensions
{
    static public class ColdDataExtension
    {
        static public void UseColdData(this IServiceCollection services)
        {
            services.AddSingleton<IDeviceColdDataService, DeviceColdDataServiceImp>();
            //services.AddSingleton<IDeviceColdDataManager, DeviceColdDataManagerImp>();
        }

    }
}
