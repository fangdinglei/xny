namespace GrpcMain
{
    /// <summary>
    /// GRPC方法权限
    /// <br/>当NeedAudit并且状态为Cancel并且包含AuditorId 自动记录审计
    /// </summary>
    [AttributeUsage( AttributeTargets.Method,AllowMultiple =false)] 
    public class GrpcRequireAuthorityAttribute : Attribute {
        public string[]? Authoritys;
        public bool NeedLogin ;
        /// <summary>
        ///  当NeedAudit并且状态为Cancel并且包含AuditorId 自动记录审计
        /// </summary>
        public bool NeedAudit { get; private set; }
        public string? NeedAudit_OpName { get; private set; }
        public GrpcRequireAuthorityAttribute()
        {
            NeedLogin = true;
        }
        public GrpcRequireAuthorityAttribute(params string[] authoritys)
        {
            NeedLogin = true;
            Authoritys = authoritys;
        }

        public GrpcRequireAuthorityAttribute(bool needAudit, string needAudit_OpName)
        {
            if (!needAudit||string.IsNullOrWhiteSpace(needAudit_OpName))
            {
                throw new Exception("错误的构造参数");
            }
            NeedAudit = needAudit;
            NeedAudit_OpName = needAudit_OpName;
        }
    }
}
