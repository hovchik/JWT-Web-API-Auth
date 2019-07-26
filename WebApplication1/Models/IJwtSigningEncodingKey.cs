using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Models
{
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }
        SecurityKey GetKey();
    }
}