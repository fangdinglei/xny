using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XNYAPI.Model.Account
{
    public class UserBriefInfo
    {
        public uint ID;
        public string Name;
        public List<UserBriefInfo> SubUsers;

        public UserBriefInfo()
        {

        }
        public UserBriefInfo(uint iD, string name )
        {
            ID = iD;
            Name = name;
            SubUsers = null;
        }
        public UserBriefInfo(uint iD, string name, List<UserBriefInfo> subUsers)
        {
            ID = iD;
            Name = name;
            SubUsers = subUsers;
        }
    }
}
