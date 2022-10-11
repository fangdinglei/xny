namespace XNYAPI.Model.Account
{
    public class UserInfo
    {
        public uint ID;
        public string UserName;
        public string Phone;
        public uint Father;

        public UserInfo(uint iD, string userName, string phone)
        {
            ID = iD;
            UserName = userName;
            Phone = phone;
        }

        public UserInfo(uint iD, string userName, string phone, uint father) : this(iD, userName, phone)
        {
            Father = father;
        }
    }
}
