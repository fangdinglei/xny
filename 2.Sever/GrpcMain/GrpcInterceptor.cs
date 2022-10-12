using Grpc.Core.Interceptors;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using GrpcMain.Common;

namespace GrpcMain
{
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
                    if (canvisit ==  false)
                    {
                        context.Status = new Status( StatusCode.PermissionDenied,errormsg);
                        return null;
                    }
                    var r=await continuation(request, context);
                    //审计
                    if (at.NeedAudit && context.Status.StatusCode == StatusCode.Cancelled) {
                        await _Handle.RecordAudit( context,request,continuation,at);
                        context.Status=Status.DefaultSuccess;
                    }
                    return r;
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
