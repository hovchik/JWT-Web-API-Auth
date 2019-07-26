using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private List<AuthenticationRequest> people = new List<AuthenticationRequest>()
        {
            new AuthenticationRequest(){Name = "test",Password = "test2"},
            new AuthenticationRequest(){Name = "tester",Password = "newTester"}
        };
        [AllowAnonymous]
        public ActionResult<string> Post(AuthenticationRequest authRequest, [FromServices] IJwtSigningEncodingKey signingEncodingKey, [FromServices] IJwtEncryptingEncodingKey encryptingEncodingKey)
        {
            // 1. Проверяем данные пользователя из запроса.
            // ...
            var identity = GetIdentity(authRequest.Name, authRequest.Password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                Response.WriteAsync("Invalid username or password.").GetAwaiter().GetResult();
                return null;
            }
            else
            {


                // 2. Создаем утверждения для токена.
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, authRequest.Name)
                };

                // 3. Генерируем JWT.
                var tokenHandler = new JwtSecurityTokenHandler();

                JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(
                    issuer: "DemoApp",
                    audience: "DemoAppClient",
                    subject: new ClaimsIdentity(claims),
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddMinutes(5),
                    issuedAt: DateTime.Now,
                    signingCredentials: new SigningCredentials(
                        signingEncodingKey.GetKey(),
                        signingEncodingKey.SigningAlgorithm),
                    encryptingCredentials: new EncryptingCredentials(
                        encryptingEncodingKey.GetKey(),
                        encryptingEncodingKey.SigningAlgorithm,
                        encryptingEncodingKey.EncryptingAlgorithm));

                var jwtToken = tokenHandler.WriteToken(token);
                return jwtToken;
            }
        }

        private object GetIdentity(string name, string password)
        {
            AuthenticationRequest person = people.FirstOrDefault(x => x.Name == name && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Name)
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
    }
}