using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Models
{
    public interface IJwtEncryptingDecodingKey
    {
        SecurityKey GetKey();
    }
}