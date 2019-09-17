using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tameenk.Identity.DAL;

namespace Tameenk.Identity.Individual.Component
{
    public class Login
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration; 

        public Login(SignInManager<IdentityUser> signInManager
            , UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        
        public async Task<LoginOutput> UserLogin(LoginModel model)
        {
            LoginOutput output = new LoginOutput();
            try
            {   
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                          new Claim(JwtRegisteredClaimNames.Sub, user.Id),                          
                          new Claim(JwtRegisteredClaimNames.Email, user.Email)                  
                        };

                        var secrectkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var creds = new SigningCredentials(secrectkey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken token = new JwtSecurityToken(_configuration["Tokens:Issuer"],
                          _configuration["Tokens:Issuer"],
                          claims,                         
                          signingCredentials: creds);

                        output.ErrorCode = LoginOutput.ErrorCodes.Success;
                        output.ErrorDescription = "Success authenticated User";
                        output.Token = new JwtSecurityTokenHandler().WriteToken(token);

                        return output;                        
                    }
                    else
                    { 
                        output.ErrorCode = LoginOutput.ErrorCodes.NullResponse;
                        output.ErrorDescription = "Incorrect password";
                        output.Token = null;

                        return output;
                    }
                }
                else
                {
                    output.ErrorCode = LoginOutput.ErrorCodes.NullResponse;
                    output.ErrorDescription = "User not exist";
                    output.Token = null;

                    return output;
                }
            }
            catch(Exception exp)
            {
                output.ErrorCode = LoginOutput.ErrorCodes.MethodException;
                output.ErrorDescription = "UserLogin through exception";
                output.Token = null;

                return output;
            }
          
        }
    }
}
