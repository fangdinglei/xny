using Grpc.Core.Interceptors;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace GrpcMain
{
    /// <summary>
    /// GRPC服务的鉴权\日志等处理器
    /// </summary>
    public interface IGrpcHandle {
        public bool Authorize(ServerCallContext context, GrpcRequireAuthorityAttribute att,out string error);
        public void OnError(Exception e);
    }
    /// <summary>
    /// GRPC方法权限
    /// </summary>
    [AttributeUsage( AttributeTargets.Method,AllowMultiple =false)] 
    public class GrpcRequireAuthorityAttribute : Attribute {
        public string[] Authoritys;
        public bool NeedLogin;
    }
    public class GrpcInterceptor : Interceptor
    {
        IGrpcHandle _Handle;
        public GrpcInterceptor(IGrpcHandle handle)
        {
             _Handle = handle;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
                var at = continuation.Method.GetCustomAttribute<GrpcRequireAuthorityAttribute>(  false);
 
            //LogCall<TRequest, TResponse>(MethodType.Unary, context); 
            try
            {
                if (at == null) {
                    return await continuation(request, context);
                } 
                else {
                    string errormsg;
                    var canvisit = _Handle.Authorize(context,at,out errormsg);
                    if (!canvisit)
                    {
                        context.Status = new Status( StatusCode.PermissionDenied,errormsg);
                        return null;
                    }
                    return await continuation(request, context);
                }
            }
            catch (Exception ex)
            {
                _Handle.OnError(ex);
                context.Status = new Status(StatusCode.Internal, "内部异常");
                return null;
            }
        }

    } 
}
