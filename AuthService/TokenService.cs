using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SoftWizBusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static SoftWizHelpers.Enums;

namespace SoftWizECommerce.AuthService
{
    public class TokenService : ITokenService
    {
        private IConfiguration _configuration = null;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BuildToken(string key, string issuer, string audience, UserDTO user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim("Mobile", user.Mobile == null ? string.Empty : user.Mobile));
            claims.Add(new Claim(ClaimTypes.Role, ((UserType)user.Type).ToString()));

            SecurityTokenDescriptor desc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(desc);
            return handler.WriteToken(token);
        }
    }
}
