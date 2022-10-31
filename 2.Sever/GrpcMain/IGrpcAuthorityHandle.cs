using Grpc.Core;
using MyDBContext.Main;
using MyJwtHelper;

namespace GrpcMain
{





    /// <summary>
    /// GRPC服务的鉴权\日志等处理器
    /// </summary>
    public interface IGrpcAuthorityHandle
    {
        public string GetToken(TokenClass tokenClass);
        public Task<(bool, string?)> Authorize(ServerCallContext context, GrpcRequireAuthorityAttribute att);
        public void OnError(Exception e);
        public Task RecordAudit<TRequest, TResponse>(ServerCallContext context, object request, UnaryServerMethod<TRequest, TResponse> continuation, GrpcRequireAuthorityAttribute att, User user) where TRequest : class where TResponse : class;
    }
}
