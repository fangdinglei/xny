using Newtonsoft.Json;
using System.Collections.Generic;

namespace XNYAPI.Response 
{
     
    public class XNYResponseBase
    {
        public  enum EErrorCode
        {
            Non = 0,
            PermissionDenied = 1,
            ParameterNotSafe = 2,
            InvalidQuery = 3,
            SeverBusy=4,
            ParameterWrong=5,
            InternalError =6,
            Other = 7, 
            Count =8,
        }
 
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Error;

        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrCode;



        ///// <summary>
        ///// 子错误码
        ///// </summary>
        //public string SubErrCode { get; set; }

        ///// <summary>
        ///// 子错误信息
        ///// </summary>
        //public string SubErrMsg { get; set; }

        ///// <summary>
        ///// 响应原始内容
        ///// </summary>
        //public string Body { get; set; }

        ///// <summary>
        ///// 二进制数据响应内容
        ///// </summary>
        //public byte[] BinDataBody { get; set; }

        ///// <summary>
        ///// HTTP GET请求的URL
        ///// </summary>
        //public string ReqUrl { get; set; }

        /// <summary>
        /// 响应结果错误
        /// </summary>
        [JsonIgnore]
        public bool IsError
        {
            get
            {
                return ErrCode != "0";
            }
        }
    
        public XNYResponseBase()
        {
            Error = "";
            ErrCode = "0";
        }
        public XNYResponseBase(string error, EErrorCode errCode)
        {
            Error = error;
            ErrCode = errCode.ToString();
        }
        public XNYResponseBase(EErrorCode errCode)
        {

            ErrCode = ((int)errCode).ToString();
            switch (errCode)
            {
                case EErrorCode.Non:
                    break;
                case EErrorCode.PermissionDenied:
                    Error = "权限不足";
                    break;
                case EErrorCode.ParameterNotSafe:
                    Error = "参数不安全";
                    break;
                case EErrorCode.InvalidQuery:
                    Error = "无效查询";
                    break;
                case EErrorCode.SeverBusy:
                    Error = "服务器忙";
                    break;
                case EErrorCode.ParameterWrong:
                    Error = "参数错误";
                    break;
                case EErrorCode.InternalError:
                    Error = "内部错误";
                    break;
                default:
                    break;
            }
        }

    }

    public class TextResponse : XNYResponseBase
    {
        public bool Suc;
        public string Data;

        public TextResponse(string data)
        {
            Data = data;
            Suc = false;
        }

        public TextResponse(bool suc, string data)
        {
            Suc = suc;
            Data = data;
        }
    }

    public class DataResponse<T> : XNYResponseBase
    {
        public T Data;

        public DataResponse(T data)
        {
            Data = data;
        }

        public DataResponse()
        {

        }
    }
    public class DataListResponse<T> : XNYResponseBase
    {
        public List<T> Data;

 
        public DataListResponse(List<T> data)
        {
            Data = data;
        }
    }
}
