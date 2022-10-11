using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using XNYAPI.Response;

namespace XNYAPI.Controllers
{
    [TokenCheckFilter(new string[] { })]
    public class PowerController : Controller
    {
        public string   GetSocRate( ){ 
            UserPayLoad payload = this.ViewBag.payload;
            try
            {
                var res=DAL.PowerServiceDAL.GetPowerRates(payload.UserID); 
                return JsonConvert.SerializeObject(res); ;
            }
            catch (Exception e)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }
            
        }

        ////todo 开启或者关闭全局电量计算服务
        //public string SetSever(bool open)
        //{
        //    return this.Error(XNYAPI.Response.XNYResponseBase.EErrorCode.Non);
        //}

        //public string Update()
        //{
        //    UserPayLoad payload = this.ViewBag.payload;
        //    if (!AccountDAL.IsSystemUserName(payload.UserName))
        //        this.Error(XNYAPI.Response.XNYResponseBase.EErrorCode.PermissionDenied);
        //    return "todo";
        //}


    }
}
