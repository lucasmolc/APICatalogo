using ApiCatalogo.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiCatalogo.Services
{
    public class TokenService : ITokenService
    {
        public string GerarToken(string key, string issuer, string audience, UserModel user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var credentials = new SigningCredentials(securityKey,
                                                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(claims: claims,
                                        expires: DateTime.Now.AddMinutes(60),
                                        signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
