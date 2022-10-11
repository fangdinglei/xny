
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using XNYAPI.DAL;
using XNYAPI.Model;
using XNYAPI.Model.Device;
using XNYAPI.Response;
namespace XNYAPI.Controllers
{
    [TokenCheckFilter(new string[] { })]
    public  class DeviceTypeController : Controller
    {
        public string GetTypeInfo (string typeids)
        {
            try
            {
                List<DeviceTypeInfo> res = new List<DeviceTypeInfo>();
                var ids= Utility.Utility.PraseIDS(typeids);
                foreach (var item in ids)
                {
                    var g= DeviceTypeDAL.GetTypeInfo(item);
                    if (g != null)
                        res.Add(g);
                }
                var rsp= new DataListResponse<DeviceTypeInfo>(res);
                return JsonConvert.SerializeObject(rsp); 
            }
            catch (Exception)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }  
        }

        public string SetTypeInfo(uint ID, string Name, string ScriptString,string DataNames) {
            //当拥有所有该类型的设备时可使用该操作
            // count（该类型设备）=count（用户所拥有的该类型设备）
            if (!Name.IsSqlSafeString())
                return this.Error(XNYResponseBase.EErrorCode.ParameterNotSafe);
            if (!DataNames.IsSqlSafeString())
                return this.Error(XNYResponseBase.EErrorCode.ParameterNotSafe);
            //测试是否是base64
            try
            {
                if (ScriptString != "*"&&!string.IsNullOrWhiteSpace(ScriptString)) {
                    var b64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(ScriptString));
                } 
            }
            catch (Exception)
            {
                return this.Error(XNYResponseBase.EErrorCode.ParameterWrong);
            }


            UserPayLoad payload = this.ViewBag.payload;
            try
            {
                using (var cnn=Utility.DBCnn.GetCnn())
                {
                    var cmd = cnn.CreateCommand();
                    if (!DeviceTypeDAL.CanSetTypeInfo(ID, payload.UserID, cmd))
                        return this.Error(XNYResponseBase.EErrorCode.PermissionDenied);

                   
                    DeviceTypeDAL.SetTypeInfo( ID,  Name, ScriptString,DataNames);
                    return this.Error(XNYResponseBase.EErrorCode.Non);
                } 
            }
            catch (Exception e)
            {
                return this.Error(XNYResponseBase.EErrorCode.InternalError);
            }
        }

    }
}
