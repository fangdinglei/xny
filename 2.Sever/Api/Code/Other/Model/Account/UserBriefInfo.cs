using System.Collections.Generic;

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
        public UserBriefInfo(uint iD, string name)
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
