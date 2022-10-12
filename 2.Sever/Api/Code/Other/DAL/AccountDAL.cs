
using MySqlConnector;
using System;
using System.Collections.Generic;
using XNYAPI.Model.Account;
using XNYAPI.Utility;

namespace XNYAPI.DAL
{

    public class AccountDAL
    {
        /// <summary>
        /// 获取用户是否存在 按用户名
        /// </summary> 
        /// <returns> </returns>
        /// <exception cref="Exception"/> 
        static public bool HasUser(string uname, MySqlCommand cmd)
        {
            cmd.CommandText = $"SELECT 1 FROM user  " +
                 $" WHERE  Name='{uname}'";
            var res = cmd.ExecuteScalar();
            if (res == null)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 获取用户是否存在 按账号密码
        /// </summary> 
        /// <returns> </returns>
        /// <exception cref="Exception"/> 
        static public bool HasUser(uint uid, string pass, MySqlCommand cmd)
        {
            cmd.CommandText = $"SELECT 1 FROM user  " +
                 $" WHERE  ID={uid} AND Pass='{pass}'";
            var res = cmd.ExecuteScalar();
            if (res == null)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 获取用户是否存在 按用户名 密码
        /// </summary> 
        /// <returns>0 or ID </returns>
        /// <exception cref="Exception"/> 
        static public uint GetUserID(string name, string pass, MySqlCommand cmd)
        {
            cmd.CommandText = $"SELECT ID FROM user  " +
                 $" WHERE  Name='{name}' AND Pass='{pass}'";
            var res = cmd.ExecuteScalar();
            if (res == null)
                return 0;
            else
                return Convert.ToUInt32(res);
        }

        ///// <summary>
        ///// 获取用户的父用户
        ///// </summary>
        ///// <returns>null if no this user </returns>>
        ///// <exception cref="Exception"/>
        //static public uint GetUserFather(uint uid)
        //{
        //    using (var cnn = DBCnn.GetCnn())
        //    {
        //        var cmd = cnn.CreateCommand();
        //        cmd.CommandText = $"SELECT  Father FROM user " +
        //         $" WHERE  ID={uid}  ";
        //        var res = cmd.ExecuteScalar();
        //        if (res == null)
        //            return 0;
        //        else
        //            return Convert.ToUInt32(res);
        //    }
        //}

      

        /// <summary>
        /// 获取系统用户的名称
        /// </summary> 
        /// <returns> </returns>
        /// <exception cref="Exception"/>
        static public string GetSystemUserName()
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                cmd.CommandText = $"SELECT Name FROM user WHERE Creator=1 LIMIT 1";
                var rd = cmd.ExecuteScalar();
                if (rd == null)
                    return "";
                return Convert.ToString(rd);
            }
        }
        /// <summary>
        /// 获取系统用户的ID
        /// </summary> 
        /// <returns> </returns>
        /// <exception cref="Exception"/>
        static public uint GetSystemUserID()
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                cmd.CommandText = $"SELECT ID FROM user WHERE Creator=1 LIMIT 1";
                var rd = cmd.ExecuteScalar();
                if (rd == null)
                    return 0;
                return Convert.ToUInt32(rd);
            }
        }
        /// <summary>
        /// 判断用户是否是系统用户
        /// </summary>
        /// <param name="uid">用户id </param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        static public bool IsSystemUserName(uint uid)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                cmd.CommandText = $"SELECT 1 FROM user WHERE Creator=1 AND ID={uid} LIMIT 1";
                var rd = cmd.ExecuteScalar();
                if (rd == null)
                    return false;
                return true;
            }
        }

        /// <summary>
        /// 判断一个用户是否是另一个的上级用户
        /// </summary>
        /// <param name="father"></param>
        /// <param name="son"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        static public bool IsFatherOrFatherFather(uint father, uint son )
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                return IsFatherOrFatherFather(father, son, cmd);
            }
        }
        static public bool  IsFatherOrFatherFather(uint father, uint son, MySqlCommand cmd)
        {
            cmd.CommandText = $"SELECT 1 FROM user inner join user_sf on user.ID= user_sf.Son " +
             $"WHERE user.ID={son} AND  user_sf.Creator={father}";
            var sre = cmd.ExecuteScalar();
            if (sre == null)
                return false;
            return true;
        }
        /// <summary>
        /// 判断一个用户是否是另一个的直接父用户
        /// </summary>
        /// <param name="father"></param>
        /// <param name="son"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        static public bool IsFather (uint father, uint son)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                return IsFatherOrFatherFather(father, son, cmd);
            }
        }
        static public bool IsFather (uint father, uint son, MySqlCommand cmd)
        {
            cmd.CommandText = $"SELECT 1 FROM user   WHERE ID={son} AND  Creator={father}";
            var sre = cmd.ExecuteScalar();
            if (sre == null)
                return false;
            return true;
        }


        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="uname">用户名(must be sql safe) </param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        static public UserAuthority GetUserAuthority(uint uid)
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                return  GetUserAuthority(uid, cmd);
            }
        }
        static public UserAuthority GetUserAuthority(uint uid, MySqlCommand cmd)
        {
            cmd.CommandText = $"SELECT Authority FROM user_authority WHERE ID='{uid}'";
            var res = cmd.ExecuteScalar();
            uint it;
            if (res == null)
                it = 0;
            else
                it = Convert.ToUInt32(res);
            return (UserAuthority)it;
        }

        /// <summary>
        /// 修改用户密码 
        /// </summary>
        /// <param name="uid"></param> 
        /// <param name="newpass"></param> 
        /// <returns> false if oldpass mathch</returns>
        /// <exception cref="Exception"/> 
        static public bool  ChangePassWord(uint uid, string newpass, MySqlCommand cmd)
        {
            cmd.CommandText = $"UPDATE user SET Pass='{newpass}' WHERE  ID={uid}  ";
            return cmd.ExecuteNonQuery()>0; 
        }


        /// <summary>
        /// 获取用户的子用户的简洁信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="cascade">是否获取子用户的子用户</param>
        /// <returns></returns>
        /// <exception cref="Exception"/>
        static public List<UserBriefInfo> GetSubUserBriefInfo(uint uid, bool cascade )
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                return  GetSubUserBriefInfo(uid, cascade, cmd);
            }
        }
        static public List<UserBriefInfo>  GetSubUserBriefInfo(uint uid, bool cascade, MySqlCommand cmd)
        {
            List<UserBriefInfo> re = new List<UserBriefInfo>();
            cmd.CommandText = $"SELECT Name,ID  FROM user WHERE Creator={uid}";
            using (var rd = cmd.ExecuteReader())
                while (rd.Read())
                    re.Add(new UserBriefInfo(rd.GetUInt32(1), rd.GetString(0)));
            if (cascade)
                foreach (var item in re)
                    item.SubUsers =  GetSubUserBriefInfo(item.ID, true, cmd);
            return re;

        }

        /// <summary>
        /// 获取用户的子用户的详细信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        static public List<UserInfo> GetAllSubUserInfo(uint uid ) {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                return GetAllSubUserInfo(uid,  cmd);
            }
        }
        static public List<UserInfo> GetAllSubUserInfo(uint uid,  MySqlCommand cmd)
        {
            List<UserInfo> res = new List<UserInfo>();
            cmd.CommandText = $"SELECT ID,Name,Phone FROM user WHERE Creator={uid}";
            using (var rd=cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    res.Add(new UserInfo(rd.GetUInt32(0),rd.GetString(1),rd.GetString(2),uid));
                }
            }
            return res;
        }

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="uid"></param> 
        /// <returns>null if no this user</returns>
        /// <exception cref="Exception"/>
        static public  UserInfo  GetUserInfo(uint uid )
        {
            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
                return GetUserInfo(uid,  cmd);
            }
        }
        static public  UserInfo  GetUserInfo(uint uid,  MySqlCommand cmd)
        { 
            cmd.CommandText = $"SELECT Name,Phone,Creator  FROM user WHERE ID={uid}";
            using (var rd = cmd.ExecuteReader())
               if(rd.Read())
                    return new UserInfo(uid,rd.GetString(0), rd.GetString(1),rd.GetUInt32(2)) ;
            return null;
        }

        /// <summary>
        /// 删除用户表中用户信息及其权限及其用户关系映射 
        /// </summary>
        /// <param name="deluid"></param>
        /// <param name="cmd"></param>
        /// <returns> subuserid </returns>
        /// <exception cref="Exception"/>
        static public List<uint> DeletUser(uint deluid, MySqlCommand cmd)
        {
            List<uint> subusers = new List<uint>();
            cmd.CommandText = $"SELECT Son FROM user_sf WHERE Creator={deluid}";
            using (var rd=cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    subusers.Add(rd.GetUInt32(0));
                }
            }
            cmd.CommandText = $"DELETE FROM user WHERE ID={deluid};"
                + $"DELETE FROM user_sf WHERE Son={deluid} ;"
            + $"DELETE FROM user_authority WHERE ID={deluid};";
            cmd.ExecuteNonQuery();
            return subusers;
        }
       

      

    }
}
