
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Serializers;
using System.Text;

namespace MyJwtHelper 
{
    public class TokenClass { 
        public long Id { get; set; }
        public long exp { get; set; }
    }
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

        public T? Get<T>(string str)  
        {
            try
            {
                return decoder.DecodeToObject<T>(str);
            }
            catch (Exception)
            {
                return  default(T);
            }
           
        }

        public string Get(TokenClass token, string key )
        { 
            return encoder.Encode(token, key); 
        }
    }
    public interface IJwtHelper
    {
        public string Get(TokenClass token, string key );
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
        public T? Get<T>(string str);
    }
}
