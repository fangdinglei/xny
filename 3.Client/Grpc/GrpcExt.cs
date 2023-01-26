using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using GrpcMain.Account;
using GrpcMain.AccountHistory;
using GrpcMain.Common;
using GrpcMain.Device;
using GrpcMain.Device.AutoControl;
using GrpcMain.DeviceData;
using GrpcMain.DeviceData.Cold;
using GrpcMain.DeviceType;
using GrpcMain.InternalMail;
using GrpcMain.System;
using GrpcMain.UserDevice;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MyClient.Grpc
{
    public static class GrpcExt
    {
        static public void ThrowIfNotSuccess(this CommonResponse rsp)
        {
            if (rsp.Success)
            {
                return;
            }
            throw new Exception(String.IsNullOrWhiteSpace(rsp.Message) ? "未知错误" : rsp.Message);
        }
        static public void UseGrpc(this IServiceCollection serviceCollection)
        {
            GrpcChannel grpcChannel = GrpcChannel.ForAddress("https://localhost:8089");
            var interceptor = new ClientCallContextInterceptor();
            var interceptorchannel = grpcChannel.Intercept(interceptor);
            serviceCollection.TryAddSingleton<IClientCallContextInterceptor>(interceptor);
            serviceCollection.TryAddSingleton(interceptorchannel);
            serviceCollection.TryAddSingleton<AccountService.AccountServiceClient>();
            serviceCollection.TryAddSingleton<UserDeviceService.UserDeviceServiceClient>();
            serviceCollection.TryAddSingleton<DeviceService.DeviceServiceClient>();
            serviceCollection.TryAddSingleton<InternalMailService.InternalMailServiceClient>();
            serviceCollection.TryAddSingleton<DeviceDataService.DeviceDataServiceClient>();
            serviceCollection.TryAddSingleton<DeviceTypeService.DeviceTypeServiceClient>();
            serviceCollection.TryAddSingleton<AccountHistoryService.AccountHistoryServiceClient>();
            serviceCollection.TryAddSingleton<RepairService.RepairServiceClient>();
            serviceCollection.TryAddSingleton<SystemService.SystemServiceClient>();
            serviceCollection.TryAddSingleton<ColdDataService.ColdDataServiceClient>();
            serviceCollection.TryAddSingleton<DeviceAutoControlService.DeviceAutoControlServiceClient>();
        }
    }
}
