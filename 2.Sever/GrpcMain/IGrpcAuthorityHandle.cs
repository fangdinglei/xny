using Grpc.Core;
using GrpcMain.Attributes;
using MyJwtHelper;

namespace GrpcMain
{





    /// <summary>
    /// GRPC服务的鉴权\日志等处理器
    /// </summary>
    public interface IGrpcAuthorityHandle
    {
        public string GetToken(TokenClass tokenClass);
        public Task<(bool, string?)> Authorize(ServerCallContext context, MyGrpcMethodAttribute att);
        public void OnError(Exception e);
        //public Task RecordAudit<TRequest, TResponse>(ServerCallContext context, object request, UnaryServerMethod<TRequest, TResponse> continuation, MyGrpcMethodAttribute att, User user) where TRequest : class where TResponse : class;
    }
}
