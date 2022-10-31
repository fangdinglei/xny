using Grpc.Core;
using Grpc.Core.Interceptors;

namespace GrpcMain
{
    public class GrpcInterceptor : Interceptor
    {
        static public Dictionary<string, GrpcRequireAuthorityAttribute> AuthorityAttributes = new Dictionary<string, GrpcRequireAuthorityAttribute>();
        IGrpcAuthorityHandle _Handle;
        public GrpcInterceptor(IGrpcAuthorityHandle handle)
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
                    at = GrpcRequireAuthorityAttribute.Default;
                   
                }

               

                if (!at.NeedLogin)
                {
                    //不要鉴权
                    var r = await continuation(request, context);
                    if (r == null)
                    {
                        return Activator.CreateInstance<TResponse>();
                    }
                    return r;
                }
                else
                {
                    string? errormsg;
                    (var canvisit, errormsg) = await _Handle.Authorize(context, at);
                    if (canvisit == false)
                    {
                        throw new RpcException(new Status(StatusCode.NotFound, errormsg));
                    }
                    TResponse r=null;
                    try
                    {
                        r = await continuation(request, context);
                    }
                    catch (RpcException ex)
                    {
                        //审计
                        if (at.NeedAudit && ex.Status.StatusCode == StatusCode.Cancelled)
                        {
                            await _Handle.RecordAudit(context, request, continuation, at);
                        }
                        throw new RpcException(new Status(StatusCode.Cancelled, "需要审计"));
                    }
                    return r;
                }
            }
            catch (RpcException ex) {
                throw;
            }
            catch (Exception ex)
            {
                _Handle.OnError(ex);
                context.Status = new Status(StatusCode.Internal, "内部异常");
                return Activator.CreateInstance<TResponse>();
            }
        }

    }

}
