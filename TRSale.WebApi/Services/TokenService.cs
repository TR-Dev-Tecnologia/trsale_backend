using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace TRSale.WebApi.Services
{
    public static class TokenService
    {
        public static string GenerateToken(string id, string name, string email)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
                new Claim("id", id),
            });

            return Token(claimsIdentity, DateTime.UtcNow.AddHours(2));
        }

        private static string Token(ClaimsIdentity claimsIdentity, DateTime expires)
        {
            var key = Environment.GetEnvironmentVariable("Secret");
            if (key == null)
                throw new ArgumentNullException();

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {                
                Subject = claimsIdentity,
                Audience = Environment.GetEnvironmentVariable("Audience"),
                Issuer = Environment.GetEnvironmentVariable("Issuer"),
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
        
    }
}