using Grpc.Core.Interceptors;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using GrpcMain.Account;
using GrpcMain.UserDevice;
using GrpcMain.Device;
using GrpcMain.InternalMail;
using GrpcMain.DeviceData;
using GrpcMain.DeviceType;
using GrpcMain.Common;

namespace MyClient.Grpc
{
    public static class GrpcExt
    {
        static public void ThrowIfNotSuccess(this CommonResponse rsp) {
            if (rsp.Success)
            {
                return;
            }
            throw new Exception(String.IsNullOrWhiteSpace(rsp.Message   )?"未知错误": rsp.Message);
        }
        static public void UserGrpc(this IServiceCollection serviceCollection)
        {
            var channel = new Channel("http://localhost:5001", ChannelCredentials.Insecure);
            channel.Intercept(new ClientCallContextInterceptor());
            serviceCollection.TryAddSingleton(channel);
            serviceCollection.TryAddSingleton<AccountService.AccountServiceClient>();
            serviceCollection.TryAddSingleton<UserDeviceService.UserDeviceServiceClient>();
            serviceCollection.TryAddSingleton<DeviceService.DeviceServiceClient>();
            serviceCollection.TryAddSingleton<InternalMailService.InternalMailServiceClient>();
            serviceCollection.TryAddSingleton<DeviceDataService.DeviceDataServiceClient>();
            serviceCollection.TryAddSingleton<DeviceTypeService.DeviceTypeServiceClient>();
        }
    }
    public class ClientCallContextInterceptor : Interceptor
    {
        public string? Token;
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            return continuation(request, context);
        }

    }
}
