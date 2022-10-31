namespace MyClient.Grpc
{
    /// <summary>
    /// 客户端请求拦截器
    /// </summary>
    public interface IClientCallContextInterceptor
    {
        void RegistResopnseInterceptor(IResopnseInterceptor interceptor);
        void SetToken(string token);
    }
}
