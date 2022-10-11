using FDL.Program;
using MySqlConnector;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace XNYAPI.Utility
{
    public static class DBCnn
    {

        const string DataSource = "fdlmaindb.mysql.rds.aliyuncs.com";
        const string DataName = "xnytest2";
        const string DBUser = "fangdinglei";
        const string DBPass = "320123fdl";
        static string DBStr = "";

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NetFailedException"/>
        /// <exception cref="MySqlException"/> 
        static public MySqlConnection GetCnn()
        {
            if (DBStr == "")
            {
                MySqlConnectionStringBuilder cs = new MySqlConnectionStringBuilder();
                cs.Server = DataSource;
                cs.Database = DataName;
                cs.UserID = DBUser;
                cs.Password = DBPass;
                cs.Port = 3306;
                cs.SslMode = MySqlSslMode.None;
                DBStr = cs.ToString();
            }

            MySqlConnection sql = new MySqlConnection(DBStr);
            sql.Open();
            //int tout = 3;
            //bool ok = false;
            //DateTime st = DateTime.Now;
            //MySqlException ex = null;
            //Thread thread = new Thread(() =>
            //{
            //    try
            //    {
            //        sql.Open();
            //        ok = true;
            //    }
            //    catch (MySqlException ex1)
            //    {
            //        ex = ex1;
            //    }
            //    catch (Exception)
            //    {

            //    }

            //});
            //thread.Start();
            //while (!ok && (DateTime.Now - st).TotalSeconds < tout)
            //{
            //    Thread.Sleep(10);
            //}
            //if (ok)
            //{
            //    if (ex != null)
            //    {
            //        if (ex.ErrorCode == MySqlErrorCode.UnableToConnectToHost)
            //        {
            //            throw new NetFailedException();
            //        }
            //    }
            //    else
            //    {
            //        return sql;
            //    }

            //}
            //else
            //{
            //    thread.Interrupt();
            //    throw new NetFailedException();
            //}
            return sql;
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NetFailedException"/>
        /// <exception cref="MySqlException"/> 
        static public async Task<MySqlConnection> GetCnnAsync()
        {
            if (DBStr == "")
            {
                MySqlConnectionStringBuilder cs = new MySqlConnectionStringBuilder();
                cs.Server = DataSource;
                cs.Database = DataName;
                cs.UserID = DBUser;
                cs.Password = DBPass;
                cs.Port = 3306;
                cs.SslMode = MySqlSslMode.None;
                cs.ConnectionTimeout = 3;
                DBStr = cs.ToString();
            }

            MySqlConnection sql = new MySqlConnection(DBStr);
            try
            {
                await sql.OpenAsync();
            }
            catch (MySqlException e)
            {
                if (e.ErrorCode == MySqlErrorCode.UnableToConnectToHost)
                {
                    throw new NetFailedException();
                }
                //Unable to connect to any of the specified MySQL hosts.
                throw;
            }
            return sql;
        }
    }

}
