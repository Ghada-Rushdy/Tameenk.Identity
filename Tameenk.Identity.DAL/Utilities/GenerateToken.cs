using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Tameenk.Identity.DAL
{
    public class GenerateToken
    {
        private readonly IConfiguration _configuration;

        public GenerateToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JwtSecurityToken GenerateTokenJWT(string ID, string Email, string userName)
        {
            var claims = new[]
                       {
                          new Claim(JwtRegisteredClaimNames.Sub, ID),
                          new Claim(JwtRegisteredClaimNames.Email, Email),
                          new Claim(JwtRegisteredClaimNames.UniqueName, userName),
                          new Claim(JwtRegisteredClaimNames.AuthTime, DateTime.Now.ToString())
                        };

            var secrectkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var creds = new SigningCredentials(secrectkey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
              _configuration["Tokens:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: creds);

            return token;
        }
    }
}
