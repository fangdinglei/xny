namespace MyClient.Grpc
{
    /// <summary>
    /// 客户端响应拦截器
    /// </summary>
    public interface IResopnseInterceptor
    {
        void OnResonse<TResponse>(TResponse rsp);
    }
}
