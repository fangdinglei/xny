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
        /// <summary>
        ///  当NeedAudit并且状态为Cancel并且包含AuditorId 自动记录审计
        /// </summary>
        public bool NeedAudit { get; private set; }
        public string? NeedAudit_OpName { get; private set; }
        public MyGrpcMethodAttribute()
        {
            NeedLogin = true;
        }
        public MyGrpcMethodAttribute(params string[] authoritys)
        {
            NeedLogin = true;
            Authoritys = authoritys;
        }

        public MyGrpcMethodAttribute(bool needAudit, string needAudit_OpName)
        {
            if (!needAudit || string.IsNullOrWhiteSpace(needAudit_OpName))
            {
                throw new Exception("错误的构造参数");
            }
            NeedAudit = needAudit;
            NeedLogin = true;
            NeedAudit_OpName = needAudit_OpName;
        }



    }
}
