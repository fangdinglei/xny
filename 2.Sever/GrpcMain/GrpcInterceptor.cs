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
using Grpc.AspNetCore.Server.Model;

namespace GrpcMain
{
    public class GrpcInterceptor : Interceptor
    {
        static public Dictionary<string, GrpcRequireAuthorityAttribute> AuthorityAttributes = new Dictionary<string, GrpcRequireAuthorityAttribute>();
        IGrpcHandle _Handle; 
        public GrpcInterceptor(IGrpcHandle handle )
        {
            _Handle = handle; 
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        { 
            try
            { 
                GrpcRequireAuthorityAttribute? at;
                if (!AuthorityAttributes.TryGetValue(context.Method, out at) || at == null)
                {  //默认鉴权 不审计
                    string? errormsg;
                    (var canvisit, errormsg) =await _Handle.Authorize(context, at);
                    if (canvisit == false)
                    {
                        context.Status = new Status(StatusCode.PermissionDenied, errormsg);
                        return null;
                    }
                    var r = await continuation(request, context);
                    return r;
                }
                else if (!at.NeedLogin)
                {
                    //不要鉴权
                    return await continuation(request, context);
                }
                else
                {
                    string? errormsg;
                    (var canvisit, errormsg) = await _Handle.Authorize(context, at );
                    if (canvisit == false)
                    {
                        context.Status = new Status(StatusCode.PermissionDenied, errormsg);
                        return null;
                    }
                    var r = await continuation(request, context);
                    //审计
                    if (at.NeedAudit)
                    {
                        if (at.NeedAudit && context.Status.StatusCode == StatusCode.Cancelled)
                        {
                            await _Handle.RecordAudit(context, request, continuation, at);
                            context.Status = Status.DefaultSuccess;
                        }
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
