using CefSharp.DevTools.Database;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using GrpcMain.Account;
using GrpcMain.AccountHistory;
using GrpcMain.Common;
using GrpcMain.Device;
using GrpcMain.DeviceData;
using GrpcMain.DeviceType;
using GrpcMain.History;
using GrpcMain.InternalMail;
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
            //serviceCollection.TryAddSingleton<ChannelBase>(channel);
            serviceCollection.TryAddSingleton(interceptor);
            serviceCollection.TryAddSingleton(interceptorchannel);
            serviceCollection.TryAddSingleton<AccountService.AccountServiceClient>();
            serviceCollection.TryAddSingleton<UserDeviceService.UserDeviceServiceClient>();
            serviceCollection.TryAddSingleton<DeviceService.DeviceServiceClient>();
            serviceCollection.TryAddSingleton<InternalMailService.InternalMailServiceClient>();
            serviceCollection.TryAddSingleton<DeviceDataService.DeviceDataServiceClient>();
            serviceCollection.TryAddSingleton<DeviceTypeService.DeviceTypeServiceClient>();
            serviceCollection.TryAddSingleton<AccountHistoryService.AccountHistoryServiceClient>();
            serviceCollection.TryAddSingleton<RepairService.RepairServiceClient>();
        }
    }
    public class ClientCallContextInterceptor : Interceptor
    {
        public string? Token;                    
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            CallOptions options = context.Options;
            if (Token != null)
            {
                if (context.Options.Headers == null)
                {
                    options = new CallOptions(new Metadata(), DateTime.UtcNow.AddSeconds(8), options.CancellationToken
                        , options.WriteOptions, options.PropagationToken, options.Credentials);
                }
#pragma warning disable CS8602 // 解引用可能出现空引用。
                options.Headers.Add("Token", Token);
#pragma warning restore CS8602 // 解引用可能出现空引用。
            }
            var contextx = new ClientInterceptorContext<TRequest, TResponse>
                (context.Method, context.Host, options);
            var rsp=continuation(request, contextx);
            rsp.GetAwaiter().OnCompleted(() => {
                LocalDataBase db = LocalDataBase.Instance;
                if (db != null)
                {
                    db.OnResonse(rsp.GetAwaiter().GetResult());
                }
            });
            
            return rsp;
        }

        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, BlockingUnaryCallContinuation<TRequest, TResponse> continuation) where TRequest : class where TResponse : class
        {
            CallOptions options = context.Options;
            if (Token != null)
            {
                if (context.Options.Headers == null)
                {
                    options = new CallOptions(new Metadata(), DateTime.UtcNow.AddSeconds(8), options.CancellationToken
                        , options.WriteOptions, options.PropagationToken, options.Credentials);
                }
#pragma warning disable CS8602 // 解引用可能出现空引用。
                options.Headers.Add("Token", Token);
#pragma warning restore CS8602 // 解引用可能出现空引用。
            }
            var contextx = new ClientInterceptorContext<TRequest, TResponse>
                (context.Method, context.Host, options);
            var rsp = continuation(request, contextx);
            LocalDataBase db = LocalDataBase.Instance;
            if (db != null)
            {
                db.OnResonse(rsp);
            }
            return rsp;
        }
    }
}
