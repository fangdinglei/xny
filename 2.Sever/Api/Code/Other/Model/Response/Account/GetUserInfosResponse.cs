using System.Collections.Generic;
using XNYAPI.Model.Account;

namespace XNYAPI.Response.Account
{

    public class GetUserInfosResponse : XNYResponseBase
    {
        public List<UserInfo> Data;
        public GetUserInfosResponse()
        {

        }
    }
}
