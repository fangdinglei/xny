using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using XNYAPI.Response;

namespace XNYAPI.Controllers
{
    public class TokenCheckFilterAttribute : ActionFilterAttribute
    {
        public const string DefaultSecret = "GADEtcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
        static string PermissionDenied = JsonConvert.SerializeObject(new XNYResponseBase(XNYResponseBase.EErrorCode.PermissionDenied));
        string secret;
        HashSet<string> ignores;

        public TokenCheckFilterAttribute(string[] ignores)
        {
            this.ignores = new HashSet<string>(ignores);
            this.secret = DefaultSecret;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var actname = filterContext.RouteData.Values["action"] as string;
            if (ignores.Contains(actname))
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            string token;

            if (!filterContext.HttpContext.Request.Cookies.TryGetValue("token", out token))
            {
                Microsoft.Extensions.Primitives.StringValues stk;
                if (!filterContext.HttpContext.Request.Query.TryGetValue("token", out stk))
                {
                    var res = new ContentResult();
                    res.Content = Newtonsoft.Json.JsonConvert.SerializeObject(new XNYResponseBase() { ErrCode = "1", Error = "Forbiden" });
                    filterContext.Result = res;
                }
                token = stk.ToString();
            }


            string payload;
            try
            {
                payload = new JwtBuilder()
                     .WithAlgorithm(new HMACSHA256Algorithm()) // 使用算法
                     .WithSecret(secret) // 使用秘钥
                     .MustVerifySignature()
                    .Decode(token);
                UserPayLoad p;
                (filterContext.Controller as Controller).ViewBag.payload = p = JsonConvert.DeserializeObject<UserPayLoad>(payload); ;
                (filterContext.Controller as Controller).ViewBag.token = token;
                //if (!p.UserName.IsSqlSafeString())
                //{
                //    var res = new ContentResult();
                //    res.Content = PermissionDenied;
                //    filterContext.Result = res;
                //    return;
                //}
                base.OnActionExecuting(filterContext);
                //Response.Cookies.Append("token", token);
            }
            catch (Exception ex)
            {
                var res = new ContentResult();
                res.Content = PermissionDenied;
                filterContext.Result = res;
                return;
            }

            ////1、获取请求的类名和方法名
            //string strController = filterContext.RouteData.Values["controller"].ToString();
            //string strAction = filterContext.RouteData.Values["action"].ToString();

            ////2、用另一种方式获取请求的类名和方法名
            //string strController2 = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //string strAction2 = filterContext.ActionDescriptor.ActionName;

            //filterContext.HttpContext.Response.Write("控制器：" + strController + "<br/>");
            //filterContext.HttpContext.Response.Write("控制器：" + strController2 + "<br/>");
            //filterContext.HttpContext.Response.Write("Action：" + strAction + "<br/>");
            //filterContext.HttpContext.Response.Write("Action：" + strAction2 + "<br/>");

            //filterContext.HttpContext.Response.Write("Action执行前：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff") + "<br/>");

        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //filterContext.HttpContext.Response.Write("Action执行后：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff") + "<br/>");
            //base.OnActionExecuted(filterContext);
        }
    }
}


