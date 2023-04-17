namespace FdlWindows.View.LoginView
{
    public class FLoginOption
    {
        public Func<IServiceProvider, long, string, Task<object>> LoginCall { get; private set; }
        public Action<IServiceProvider, object> SuccessCall { get; private set; }
        public FLoginOption(Func<IServiceProvider, long, string, Task<object>> loginCall, Action<IServiceProvider, object> successCall)
        {
            LoginCall = loginCall;
            SuccessCall = successCall;
        }
    }
}
