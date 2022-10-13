using Grpc.Core.Interceptors;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClient.Grpc
{
    public class GlobalClients{
        static public void GetInstance<T>() where T: ClientBase<T>
        { 
            
        }
    }
    public class ClientCallContextInterceptor : Interceptor
    {
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {  
            return continuation(request, context);  
        }

    }
}
