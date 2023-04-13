using Grpc.Core;
using Grpc.Core.Interceptors;
using GrpcMain.Attributes;
using MyDBContext.Main;
using Org.BouncyCastle.Asn1.Ocsp;
using Ubiety.Dns.Core;

namespace GrpcMain.Interceptors
{
    public class Node<TRequest, TResponse> 
        where TRequest : class
       where TResponse : class
    {
        Func<TRequest, ServerCallContext, Node<TRequest, TResponse>, Task<TResponse>> call;
        public Node<TRequest, TResponse> Next;
        public Node(Func<TRequest, ServerCallContext,Node<TRequest, TResponse>, Task<TResponse>> call, Node<TRequest, TResponse> next)
        {
            this.call = call;
            this.Next = next;
        }

        public Task<TResponse> Run(TRequest request,
            ServerCallContext context) {
            return call(request,context,Next);
        }
    }

    public class GrpcInterceptor : Interceptor
    {
        const string ATTRIBUTE = "att";

        Dictionary<string, MyGrpcMethodAttribute> AuthorityAttributes;
        IGrpcAuthorityHandle _Handle;
        public GrpcInterceptor(IGrpcAuthorityHandle handle, Dictionary<string, MyGrpcMethodAttribute> authorityAttributes)
        {
            _Handle = handle;
            AuthorityAttributes = authorityAttributes;
        }

        async Task<TResponse> DBWrap<TRequest, TResponse>(
            TRequest req, ServerCallContext context, Node<TRequest, TResponse> next) where TRequest : class where TResponse : class
        {
            MyGrpcMethodAttribute att = context.UserState[ATTRIBUTE] as MyGrpcMethodAttribute;
            context.UserState[nameof(MainContext)] = null;
            bool fail = false; bool commitfail = false;
            try
            {
                if (att.NeedDB)
                {
                    MainContext ct = new MainContext();
                    context.UserState[nameof(MainContext)] = ct;
                    if (att.NeedTransaction)
                    {
                        await ct.Database.BeginTransactionAsync();
                    }
                }
                return await next.Run(req, context);
            }
            catch (Exception ex)
            {
                fail = true;
                throw;
            }
            finally
            {
                var ct = context.UserState[nameof(MainContext)] as MainContext;
                if (ct != null)
                {
                    if (att.NeedTransaction && !fail)
                    {
                        try
                        {
                            await ct.Database.CommitTransactionAsync();
                        }
                        catch (Exception ex)
                        {
                            throw new RpcException(new Status(StatusCode.Internal, "事务提交失败"));
                        }
                    }
                    ct.Dispose();
                }
            }
        }
        async Task<TResponse> AuthorityWrap<TRequest, TResponse>(
            TRequest req, ServerCallContext context, Node<TRequest, TResponse> next)
            where TRequest : class where TResponse : class
        {
            MyGrpcMethodAttribute? at;
            if (!AuthorityAttributes.TryGetValue(context.Method, out at) || at == null)
            {  //默认鉴权
                at = MyGrpcMethodAttribute.Default;
            }
            context.UserState[ATTRIBUTE] = at;

            if (!at.NeedLogin)
            {
                //不要鉴权
                var r = await next.Run(req, context);
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
                    r = await next.Run(req, context);
                }
                catch (RpcException ex)
                {
                    throw;
                }
                return r;
            }

        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            Node<TRequest, TResponse> authoritynode = new Node<TRequest, TResponse>(AuthorityWrap, null);
            Node<TRequest, TResponse> dbnode = new Node<TRequest, TResponse>(DBWrap, null);
            Node<TRequest, TResponse> end = new Node<TRequest, TResponse>((r,c,n) => { 
               return continuation(r,c);
            },null);
            authoritynode.Next = dbnode ;
            dbnode.Next = end;

            try
            {
                return await authoritynode.Run(request, context);
            }
            catch (RpcException ex) {
                throw;
            }
            catch (Exception ex)
            {
                _Handle.OnError(ex);
                throw new RpcException(new Status(StatusCode.Internal, "内部异常"));
            }
        
        }

    }

}
