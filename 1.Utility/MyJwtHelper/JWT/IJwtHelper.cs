
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using System.Text;

namespace MyJwtHelper 
{
    public class JwtHelper : IJwtHelper
    {
        IJwtEncoder encoder;
        IJwtDecoder decoder;
        public JwtHelper()
        {
            var algorithm = new HMACSHA256Algorithm();
            var serializer = new JsonNetSerializer();
            var urlEncoder = new JwtBase64UrlEncoder();
            encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            decoder = new JwtDecoder(serializer, urlEncoder);
        }
        public string Get(Dictionary<string, object> pars, string key, long exptime = -1)
        {
            if (exptime>0&&!pars.ContainsKey("exp"))
            {
                pars["exp"] = exptime ;
            }
            return encoder.Encode(pars, key);
        }

        public T Get<T>(string str)
        {
            return decoder.DecodeToObject<T>(str);
        } 
    }
    public interface IJwtHelper
    {
        /// <summary>
        /// 进行jwt加密
        /// </summary>
        /// <param name="pars"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Get(Dictionary<string, object> pars, string key,long exptime=-1);
        /// <summary>
        /// 获取jwt对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public T Get<T>(string str);
    }
}
