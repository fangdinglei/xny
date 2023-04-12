namespace GrpcMain.Attributes
{
    /// <summary>
    /// GRPC方法权限
    /// <br/>当NeedAudit并且状态为Cancel并且包含AuditorId 自动记录审计
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MyGrpcMethodAttribute : Attribute
    {
        static public MyGrpcMethodAttribute Default = new MyGrpcMethodAttribute();

        public string[]? Authoritys;
        public bool NeedLogin;
        public bool NeedDB;
        public bool NeedTransaction;
        public MyGrpcMethodAttribute()
        {
            NeedLogin = true;
        }
        public MyGrpcMethodAttribute(params string[] authoritys)
        {
            NeedLogin = true;
            Authoritys = authoritys;
        }
    }
}
