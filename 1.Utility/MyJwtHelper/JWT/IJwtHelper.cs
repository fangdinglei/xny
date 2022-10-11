
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
        public string Get(Dictionary<string, string> pars, string key)
        {
            return encoder.Encode(pars, key);
        }

        public T Get<T>(string str)
        {
            return decoder.DecodeToObject<T>(str);
        }
    }
    public interface IJwtHelper
    {
        public string Get(Dictionary<string, string> pars, string key);
        public T Get<T>(string str);
    }
}
