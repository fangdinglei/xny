using System.Collections.Generic;
using Newtonsoft.Json;
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
