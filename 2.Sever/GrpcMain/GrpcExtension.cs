using Grpc.Core;
using GrpcMain;
using GrpcMain.Account;
using GrpcMain.Common;
using GrpcMain.Device;
using GrpcMain.DeviceData;
using GrpcMain.DeviceType;
using GrpcMain.InternalMail;
using GrpcMain.UserDevice;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MyDBContext.Main;
using MyJwtHelper;
using MyUtility;
using System.Reflection;

namespace PgGrpcMain
{
    static public class MyGrpcExtension
    {
        static public void UseMyGrpc(this IServiceCollection services, string jwtkey)
        {
            services.Configure<MyGrpcHandleCongig>(op => op.JwtKey = jwtkey);
            services.TryAddSingleton<IJwtHelper, JwtHelper>();
            services.TryAddSingleton<IGrpcCursorUtility, GrpcCursorUtilityImp>();

            //添加处理器
            services.TryAddSingleton<IGrpcHandle, MyGrpcHandle>();
            //开启GRPC
            services.AddGrpc(op =>
            {
                //全局错误处理 
                 op.Interceptors.Add<GrpcInterceptor>(); 
                    op.EnableDetailedErrors = true; 
            });
            //Test 测试
            using (MainContext ct = new MainContext())
            {
                ct.Database.EnsureCreated();
            }
         
        }
        /// <summary>
        /// 注册grpc服务到路由
        /// </summary>
        /// <param name="app"></param>
        static public void RegistMyGrpc(this WebApplication app)
        {
            app.MapGrpcService<AccountServiceImp>();
            app.MapGrpcService<DeviceServiceImp>();
            app.MapGrpcService<UserDeviceServiceImp>();
            app.MapGrpcService<DeviceDataServiceImp>();
            app.MapGrpcService<DeviceTypeServiceImp>();
            app.MapGrpcService<InternalMailServiceImp>();

            foreach (var item in typeof(IGrpcHandle).Assembly.GetTypes())
            {
                var att= item.GetCustomAttribute< BindServiceMethodAttribute>();
                if (att == null)
                    continue;
                foreach (var func in item.GetMethods( ))
                {
                    if (!func.IsVirtual)
                        continue;
                    var atx = func.GetCustomAttribute<GrpcRequireAuthorityAttribute>();
                    if (atx == null)
                        continue;
                    var str="/" + att.BindType.FullName+"/"+func.Name;
                    GrpcInterceptor.AuthorityAttributes.Add(str, atx);
                }
               
            }
        }

       static bool TryDeserializeObject<T>(this  string json,out T? obj)where T:class
        {
           
            if (string.IsNullOrWhiteSpace(json))
            {
                obj = null;
                return false;
            }
            try
            {
                 obj=Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json); 
                return obj != null;
            }
            catch (Exception)
            {
                obj = null;
                return false;
            }
        }


        internal class MyGrpcHandle : IGrpcHandle
        {
            MyGrpcHandleCongig _config;
            IJwtHelper _jwtHelper;
            ITimeUtility _timeUtility;
            public MyGrpcHandle(IOptions<MyGrpcHandleCongig> op, IJwtHelper helper, ITimeUtility timeUtility)
            {
                _config = op.Value;
                if (string.IsNullOrWhiteSpace(_config.JwtKey))
                {
                    _config.JwtKey = "2432114474";
                }
                _jwtHelper = helper;
                _timeUtility = timeUtility;
            }

            public async Task<(bool,string?  )> Authorize(ServerCallContext context, GrpcRequireAuthorityAttribute att )
            {
                object token;
                var et = context.RequestHeaders.Get("Token");
                if (et == null|| (token = et.Value) == null)
                {
                    return (false, "请添加Token再访问"); 
                }
                var tokenstr = (string)token;
                var jwt = _jwtHelper.Get<TokenClass>(tokenstr);
                if (jwt == null)
                { 
                    return (false, "token无效");
                }
                if (att.Authoritys!=null&&att.Authoritys.Count()>0)
                {//校验高级权限
                    using (MainContext ct = new MainContext()) {
                       var authoritys=await ct.Users.Where(it => it.Id == jwt.Id).Select(it => it.Authoritys).FirstOrDefaultAsync();
                        List<string>? authoritysx;
                        if (authoritys == null || !authoritys.TryDeserializeObject(out authoritysx)||authoritysx==null) {
                            return (false, "需要高级权限 " + att.Authoritys[0]);  
                        }
                        var nothave = att.Authoritys.Except(authoritysx);
                        if (nothave.Count()!=0)
                        {
                            return (false, "需要高级权限 " + nothave.First());
                        } 
                    }    
                }
                context.UserState["CreatorId"] = jwt.Id;
                return (true, null);
            }

            public string GetToken(TokenClass tokenClass)
            {
                return _jwtHelper.Get(tokenClass, _config.JwtKey);
            }

            public void OnError(Exception e)
            {
                return;
            }

            public async Task RecordAudit<TRequest, TResponse>(ServerCallContext context, object request, UnaryServerMethod<TRequest, TResponse> continuation, GrpcRequireAuthorityAttribute att)
                where TRequest : class
                where TResponse : class
            {
                long Sponsorid = (long)context.UserState["CreatorId"];
                long AuditorId = (long)context.UserState["AuditorId"];
                using (MainContext ct = new MainContext())
                {
                    ct.User_Op_Audits.Add(
                        new User_Op_Audit()
                        {
                            SponsorId = Sponsorid,
                            AuditorId = AuditorId,
                            Time = _timeUtility.GetTicket(),
                            Op = att.NeedAudit_OpName,
                            Data = Newtonsoft.Json.JsonConvert.SerializeObject(request)
                        }
                        ); ;
                    await ct.SaveChangesAsync();
                }
            }
        }

        internal class MyGrpcHandleCongig
        {
            public string JwtKey { get; set; } = "12331";
        }

    }
}
