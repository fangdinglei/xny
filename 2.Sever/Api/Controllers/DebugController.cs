using Microsoft.AspNetCore.Mvc;
using XNYAPI.DAL;
using XNYAPI.Utility;

namespace XNYAPI.Controllers
{
#if !DEBUG
 [TokenCheckFilter(new string[] { })]
#endif

    public class DebugController : Controller
    {
        string DeleteAll()
        {
            return
                "DELETE FROM userdevice_group;" +
                        "DELETE FROM userdevice;" +
                        "DELETE FROM user_sf;" +
                        "DELETE FROM user_authority;" +
                        "DELETE FROM user;" +
                        "DELETE FROM time_schedule;" +
                        "DELETE FROM t_kvs;" +
                        "DELETE FROM powerservice_info;" +
                        " " +
                        //"DELETE FROM group_service;" +
                        "DELETE FROM id_allocator;" +
                        "DELETE FROM devicetype;" +
                        "DELETE FROM deviceinfo;" +
                        "DELETE FROM device_datapoints;" +
                        "DELETE FROM cmd_send;";
        }
        string InitBase()
        {
            var script = "IyMjCjMwCmRhdGEgCiMjIwoxMjAKb25saW5lCmRhdGFyZWFkIFRlbXBlcmF0dXJlLDEwLDEwLGRvdWJsZQpkYXRhcmVhZCBMdW1pbmF0aW9uLDEwLDEwLGRvdWJsZQpkYXRhcmVhZCBIdW1pZGl0eSwxMCwxMCxkb3VibGUKZGF0YXJlYWQgZUNPMiwxMCwxMCxkb3VibGUKcG93ZXIgY2FsLDEwLDExMDU5MjAKbGVk";

            return
                "INSERT INTO user(ID,Name,Pass,Phone,Creator)VALUES" +
                "(1,'admin','','',0),(2,'system','p1','',1);" +

                "INSERT INTO id_allocator(ID,Name,Min,Max,Current,Comments)VALUES"
                + "(1,'UserID',2,4294967290,2,'用户ID分配'),"
                + "(2,'DeviceID',100,1073741820,100,'设备ID分配'),"
                + "(3,'GroupID',1073741830,4294967290,1073741830,'设备服务的分组的ID分配'),"
                + "(4,'UserDeviceGroupID',2,4294967290,2,'用户设备分组ID分配'),"
                + "(5,'DeviceTypeID',1,4294967290,1,'设备类型ID分配');"
                + "INSERT INTO devicetype(ID,Name,Script,DataNames)"
                 + $"VALUES(2, '标准设备', '{ script }','Temperature,Lumination,Humidity,eCO2,Power_in,Power_out'),"
                + $"(1, '测试',   '{ script }','Temperature,Lumination,Humidity,eCO2,Power_in,Power_out'),(0, '默认设备',  '{ script }','Temperature,Lumination,Humidity,eCO2,Power_in,Power_out');"
                + "INSERT INTO user_authority VALUES(1,255,0,0); " +
                "INSERT INTO user_authority VALUES(2,255,0,0);" +
                "INSERT INTO user_authority VALUES(3,255,0,0);" +
                "INSERT INTO user_authority VALUES(4,255,0,0);" +
                "INSERT INTO user_authority VALUES(5,255,0,0);";


        }
        string InsertTest()
        {
            return
               "INSERT INTO user(ID,Name,Pass,Phone,Creator)VALUES"
               +
               "(3,'fdl','pass','',2)," +
               "(4,'fdl2','pass','',2)," +
               "(5,'fdl3','pass','',3);" +
               "UPDATE id_allocator SET Current=6 WHERE ID=1;" +
               "INSERT INTO user_sf (Son,Creator)VALUES" +
               "(3,2)," +
               "(4,2)," +
               "(5,3),(5,2);";
        }
        public string InitDataBaseFromTest()
        {

            UserPayLoad p = ViewBag.payload;

            using (var cnn = DBCnn.GetCnn())
            {
                var cmd = cnn.CreateCommand();
#if !DEBUG
                    if (!AccountDAL.IsSystemUserName(p.UserID))
                {
                    return "fail";
                }
#endif


                cmd.Transaction = cnn.BeginTransaction();
                try
                {
                    cmd.CommandText = DeleteAll();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = InitBase();
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = InsertTest();
                    cmd.ExecuteNonQuery();

                    cmd.Transaction.Commit();
                }
                catch (System.Exception e)
                {
                    cmd.Transaction.Rollback();
                    return "fail";
                }
                DataServiceUtility.RefreshDevices();
            }
            return "suc";
        }
        public string RefreshDevices()
        {
            return DataServiceUtility.RefreshDevices() ? "suc" : "fail";
        }
    }
}
