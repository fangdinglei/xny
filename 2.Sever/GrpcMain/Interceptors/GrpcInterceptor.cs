using Grpc.Core;
using Grpc.Core.Interceptors;
using GrpcMain.Attributes;
using MyDBContext.Main;

namespace GrpcMain.Interceptors
{
    public class GrpcInterceptor : Interceptor
    {
        Dictionary<string, MyGrpcMethodAttribute> AuthorityAttributes;
        IGrpcAuthorityHandle _Handle;
        public GrpcInterceptor(IGrpcAuthorityHandle handle, Dictionary<string, MyGrpcMethodAttribute> authorityAttributes)
        {
            _Handle = handle;
            AuthorityAttributes = authorityAttributes;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                MyGrpcMethodAttribute? at;
                if (!AuthorityAttributes.TryGetValue(context.Method, out at) || at == null)
                {  //默认鉴权 不审计
                    at = MyGrpcMethodAttribute.Default;

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
                    TResponse r = null;
                    try
                    {
                        r = await continuation(request, context);
                    }
                    catch (RpcException ex)
                    {
                        //审计
                        if (at.NeedAudit && ex.Status.StatusCode == StatusCode.Cancelled)
                        {
                            await _Handle.RecordAudit(context, request, continuation, at, (User)context.UserState["user"]);
                            throw new RpcException(new Status(StatusCode.Cancelled, "需要审计"));
                        }
                        throw;
                    }
                    return r;
                }
            }
            catch (RpcException ex)
            {
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
