using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtBearer.Models;
using Microsoft.IdentityModel.Tokens;

namespace JwtBearer.Services
{
    public class TokenService
    {
        public string Generate(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.PrivateKey);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(2),
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);


        }
        private static ClaimsIdentity GenerateClaims(User user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.Name,user.Email));
            //user.Email)); eu posso substituir o Email por id, nome, etc. 

            foreach (var role in user.Roles)
                ci.AddClaim(new Claim(ClaimTypes.Role, role));
            return ci;
        }

    }
}


