using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseDefines
{
    public enum UserAuthorityEnum
    {
        //管理员权限  xxxx00
        TopUserAdd = 1 << 4,
        //普通用户权限 xxxx01
        DeviceAdd = 1 << 4 + 1,
        DeviceTypeW = 2 << 4 + 1,
        DeviceTypeAdd = 3 << 4 + 1,
        ColdDataR = 4 << 4 + 1,
        ColdDataW = 5 << 4 + 1,

    }
}
