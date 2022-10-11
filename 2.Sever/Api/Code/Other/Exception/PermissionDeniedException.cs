using System;


namespace FDL.Program
{
    public class PermissionDeniedException : Exception
    {
        public PermissionDeniedException() :
            base($"用户没有登陆或者没有足够的权限")
        { }

    }

}
