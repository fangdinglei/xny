using Grpc.Core;
using MyJwtHelper;

namespace GrpcMain
{
 
    /// <summary>
    /// GRPC服务的鉴权\日志等处理器
    /// </summary>
    public interface IGrpcHandle { 
        public string GetToken(TokenClass tokenClass);
        public bool Authorize(ServerCallContext context, GrpcRequireAuthorityAttribute att,out string error);
        public void OnError(Exception e);
        public Task RecordAudit<TRequest, TResponse> (ServerCallContext context, object request, UnaryServerMethod<TRequest, TResponse> continuation, GrpcRequireAuthorityAttribute att) where TRequest : class where TResponse : class;
    }
}
