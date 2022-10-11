
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNYAPI.DAL;
using XNYAPI.Model.Account;
using XNYAPI.Response;
using XNYAPI.Response.Account;
using XNYAPI.Utility;

namespace XNYAPI.Controllers
{
    [TokenCheckFilter(new string[] { "Login" })]
    public class AccountController : Controller
    {
        /// <summary>
        /// 获取当前用户的基础信息
        /// </summary>
        /// <param name="subuser">是否返回子用户, false  返回自己的信息 true 返回所有子用户的信息</param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        public string GetUserInfo(bool subuser = false)
        {
            /*
                获取用户的信息或者子用户信息 
             */
            UserPayLoad payload = this.ViewBag.payload;
            GetUserInfosResponse rsp = new GetUserInfosResponse();
            try
            {
                if (subuser)
                {
                    rsp.Data = AccountDAL.GetAllSubUserInfo(payload.UserID);
                    rsp.Data.Insert(0,AccountDAL.GetUserInfo(payload.UserID));
                }
                else
                {
                    rsp.Data = new List<UserInfo>() {
                        AccountDAL.GetUserInfo(payload.UserID)
                    };

                }

                return JsonConvert.SerializeObject(rsp);
            }
            catch (Exception)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        ///  <exception cref="Exception"/>
        ///   <exception cref="NetFailedException"/>
        public string CreatUser(string uname, string pass, string phone = "")
        {
            /*
            创建用户
            如果用户名存在则拒绝
            插入用户信息
            插入用户权限信息
            插入用户关系映射

            TODO 校验用户的子用户的数量并修改用户子用户数量
            */
            UserPayLoad payload = this.ViewBag.payload;
            if (!uname.IsSqlSafeString() || !pass.IsSqlSafeString())
                return this.Error(XNYResponseBase.EErrorCode.ParameterNotSafe);
            if (phone == null)
                phone = "";
            if (phone != "" && !phone.IsIntString())
                return this.Error(XNYResponseBase.EErrorCode.ParameterWrong);
           
            try
            {
                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    if (AccountDAL.HasUser(uname, cmd))
                    {
                        return JsonConvert.SerializeObject(new TextResponse("用户名存在"));
                    }

                    cmd.Transaction = cnn.BeginTransaction();
                    
                    try
                    {
                        cmd.CommandText= $"SELECT COUNT(*) FROM user  " +
                            $"WHERE ID = {payload.UserID}";
                         int count =Convert.ToInt32(cmd.ExecuteScalar())  ;
                        cmd.CommandText = $"SELECT MaxSubUser FROM user_authority  " +
                           $"WHERE ID = {payload.UserID}";
                        int max = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count>=max)
                            return this.Error(XNYResponseBase.EErrorCode.PermissionDenied, "用户数量上限");
                        uint newid = DALUtility.GetID(1,cmd);
                        string sq1 = $"INSERT INTO user(ID,Name,Pass,Phone,Father)  " +
                          $"VALUES({ newid},'{uname}','{pass}','{phone}','{payload.UserID}');";
                        string sq2 = $"INSERT INTO user_authority(ID, MaxSubUser,SubUserCount,Authority) " +
                                $"VALUES({ newid}, 255,0,0);";
                        string sq3 = $"INSERT IGNORE INTO user_sf(Son,Father) SELECT {newid},Father FROM user_sf WHERE Son={payload.UserID};" +
                            $"INSERT IGNORE INTO user_sf(Son,Father) VALUES({newid},{payload.UserID});";
                        cmd.CommandText = sq1 + sq2 + sq3;
                        cmd.ExecuteNonQuery();
                        cmd.Transaction.Commit();
                        return JsonConvert.SerializeObject(new DataResponse<uint>(newid));
                    }
                    catch (Exception e1)
                    {
                        cmd.Transaction.Rollback();
                        return this.Error(XNYResponseBase.EErrorCode.InternalError);
                    }
                
                }
            }
            catch (Exception e2)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldpass"></param>
        /// <param name="newpass"></param>
        /// <returns></returns>
        public string ChangePassWord(string oldpass, string newpass)
        {
            /*
            修改密码
            如果新旧密码相同,不能修改
            如果旧密码错误,不能修改
            修改密码为新密码
            */
            UserPayLoad payload = this.ViewBag.payload;
            if (!oldpass.IsSqlSafeString() || !newpass.IsSqlSafeString())
            {
                return this.Error(XNYResponseBase.EErrorCode.ParameterNotSafe);
            }
            try
            {
                if (oldpass == newpass)
                    return JsonConvert.SerializeObject(new TextResponse("请使用新密码"));
                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    if (!AccountDAL.HasUser(payload.UserID, oldpass, cmd))
                    {
                        return JsonConvert.SerializeObject(new TextResponse("密码错误"));
                    }
                    AccountDAL.ChangePassWord(payload.UserID, newpass, cmd);
                    return JsonConvert.SerializeObject(new TextResponse(true, null));
                }
            }
            catch (Exception ex)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }

        }

        /// <summary>
        /// 删除用户 及其所有设备 和其权限记录
        /// </summary>
        ///  <exception cref="NetFailedException"/>
        ///  <exception cref="Exception"/>
        public string DeletUser(uint uid)
        {
            /*
            删除用户
            如果不是上级用户，不能删除 
            将此用户加入队列
            获取一个用户id 删除其信息 返回其子用户id
            子用户id加入队列并重新此操作
            */
            UserPayLoad payload = this.ViewBag.payload;
            try
            {
                using (var cnn = DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    //不是上级无法删除用户
                    if (!AccountDAL.IsFatherOrFatherFather(payload.UserID, uid, cmd))
                        return this.Error(XNYResponseBase.EErrorCode.PermissionDenied);

                    cmd.Transaction = cnn.BeginTransaction();
                    try
                    {
                        Queue<uint> userneeddel = new Queue<uint>();
                        userneeddel.Enqueue(uid);
                        while (userneeddel.Count > 0)
                        {
                            var nls = AccountDAL.DeletUser(userneeddel.Dequeue(), cmd);
                            UserDeviceDAL.DeletUserAllDevice(uid, cmd);
                            foreach (var item in nls)
                            {
                                userneeddel.Enqueue(item);
                            }
                        }
                        cmd.Transaction.Commit();
                        return this.Error(XNYResponseBase.EErrorCode.Non);
                    }
                    catch (Exception e)
                    {
                        cmd.Transaction.Rollback();
                        return this.Error(XNYResponseBase.EErrorCode.InternalError);
                    }
                }
            }
            catch (Exception ex)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }

        }

        public string Login(string uname, string pass)
        {
            /*
             用户登陆
            用户名和密码匹配则登陆成功
            下发token
             */
            if (!uname.IsSqlSafeString() || !pass.IsSqlSafeString())
            {
                return this.Error(XNYResponseBase.EErrorCode.ParameterNotSafe);
            } 
            using (var cnn = DBCnn.GetCnn())
            {

                var id = AccountDAL.GetUserID(uname, pass, cnn.CreateCommand());
                if (id == 0)
                    return JsonConvert.SerializeObject(new TextResponse(false, null));
#if DEBUG
                var tk = new JwtBuilder()
                   .WithAlgorithm(new HMACSHA256Algorithm()) // 使用算法
                   .WithSecret(TokenCheckFilterAttribute.DefaultSecret) // 使用秘钥 
                   .AddClaim("exp", DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds())
                   .AddClaim("UserName", uname)
                   .AddClaim("UserID", id)
                  .Encode();
#else
  var tk = new JwtBuilder()
                     .WithAlgorithm(new HMACSHA256Algorithm()) // 使用算法
                     .WithSecret(TokenCheckFilterAttribute.DefaultSecret) // 使用秘钥 
                     .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                     .AddClaim("UserName", uname) 
                       .AddClaim("UserID", id) 
                     .Encode( );
#endif

                return JsonConvert.SerializeObject(new TextResponse(true, tk));
            }
        }
    }

}
