
using MySqlConnector;
using System;

namespace XNYAPI.DAL
{
    public class ServiceDAL {
        /// <summary>
        /// 判断用户是否是此分组的所有者或者是所有者的上级
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="groupid"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        static public bool  HasGroup(uint userid,uint groupid, MySqlCommand cmd) {
            //has group
            cmd.CommandText = $"SELECT OwnerUser FROM group_service WHERE  ID={groupid}";
            var sc = cmd.ExecuteScalar();
            if (sc == null)
                return false;
            var gowner = Convert.ToUInt32(sc);
            if (gowner == userid)
                return true;
            cmd.CommandText = $"SELECT 1 FROM user_sf WHERE Son={gowner} AND Creator={userid}";
            sc = cmd.ExecuteScalar();
            return sc != null;
        }
    

  
    
    }

   
}
