using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Models
{
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}