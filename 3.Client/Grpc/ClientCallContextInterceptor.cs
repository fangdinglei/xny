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
            var channel = new Channel("localhost", 5008, ChannelCredentials.SecureSsl);
            var interceptor = new ClientCallContextInterceptor();
           var interceptorchannel= channel.Intercept(interceptor);
            //serviceCollection.TryAddSingleton<ChannelBase>(channel);
            serviceCollection.TryAddSingleton(interceptor);
            serviceCollection.TryAddSingleton(interceptorchannel);
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
            CallOptions options = context.Options;
            if (Token!=null)
            {
                if (context.Options.Headers == null) {
                    options = new CallOptions(new Metadata(),DateTime.UtcNow.AddSeconds(8),options.CancellationToken
                        ,options.WriteOptions,options.PropagationToken,options.Credentials); 
                }
#pragma warning disable CS8602 // 解引用可能出现空引用。
                options.Headers.Add("Token", Token);
#pragma warning restore CS8602 // 解引用可能出现空引用。
            } 
            var contextx = new ClientInterceptorContext<TRequest, TResponse>
                (context.Method, context.Host, options);
            return continuation(request, contextx);
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
            return continuation(request, contextx); 
        }
    }
}
