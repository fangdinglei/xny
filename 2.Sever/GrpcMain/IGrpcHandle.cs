using Grpc.Core;

namespace GrpcMain
{
    /// <summary>
    /// GRPC服务的鉴权\日志等处理器
    /// </summary>
    public interface IGrpcHandle {
        public string GetToken(Dictionary<string,object>kvs);
        public bool Authorize(ServerCallContext context, GrpcRequireAuthorityAttribute att,out string error);
        public void OnError(Exception e);
    }
}
