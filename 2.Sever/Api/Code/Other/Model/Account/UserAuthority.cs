using System;

namespace XNYAPI.Model.Account
{
    [Flags]
    public enum UserAuthority
    {
        CreatSubUser = 1,
        CreatDevice = 2,
    }
}
