using Grpc.Core;
using Grpc.Core.Interceptors;
using Newtonsoft.Json.Linq;

namespace MyClient.Grpc
{
    public class ClientCallContextInterceptor : Interceptor, IClientCallContextInterceptor
    {
        DateTime _time;
        Func<string, string> _retoken;
        public string? _Token;
        IResopnseInterceptor _Interceptor;
        public ClientCallContextInterceptor(Func<string,string> retoken)
        {
            _retoken=retoken;
        }

        private void CheckToken() {
            if ((DateTime.Now - _time).TotalMinutes>10)
            {
                _Token = _retoken.Invoke(_Token);
                _time = DateTime.Now;
            }
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            CheckToken();
            CallOptions options = context.Options;
            if (_Token != null)
            {
                if (context.Options.Headers == null)
                {
                    options = new CallOptions(new Metadata(), DateTime.UtcNow.AddSeconds(8), options.CancellationToken
                        , options.WriteOptions, options.PropagationToken, options.Credentials);
                }
#pragma warning disable CS8602 // 解引用可能出现空引用。
                options.Headers.Add("Token", _Token);
#pragma warning restore CS8602 // 解引用可能出现空引用。
            }
            var contextx = new ClientInterceptorContext<TRequest, TResponse>
                (context.Method, context.Host, options);
            var rsp = continuation(request, contextx);
            if (_Interceptor != null)
            {
                rsp.GetAwaiter().OnCompleted(() =>
                {
                    try
                    {
                        _Interceptor?.OnResonse(rsp);
                    }
                    catch (Exception)
                    {
                    }
                });
            }
            return rsp;
        }

        public override TResponse BlockingUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context, BlockingUnaryCallContinuation<TRequest, TResponse> continuation) where TRequest : class where TResponse : class
        {
            CheckToken();
            CallOptions options = context.Options;
            if (_Token != null)
            {
                if (context.Options.Headers == null)
                {
                    options = new CallOptions(new Metadata(), DateTime.UtcNow.AddSeconds(8), options.CancellationToken
                        , options.WriteOptions, options.PropagationToken, options.Credentials);
                }
#pragma warning disable CS8602 // 解引用可能出现空引用。
                options.Headers.Add("Token", _Token);
#pragma warning restore CS8602 // 解引用可能出现空引用。
            }
            var contextx = new ClientInterceptorContext<TRequest, TResponse>
                (context.Method, context.Host, options);
            var rsp = continuation(request, contextx);
            try
            {
                _Interceptor?.OnResonse(rsp);
            }
            catch (Exception) { }
            return rsp;
        }

        public void RegistResopnseInterceptor(IResopnseInterceptor interceptor)
        {
            _Interceptor = interceptor;
        }

        public void SetToken(string token)
        {
            _time = DateTime.Now;
            _Token = token;
        }
    }
}
