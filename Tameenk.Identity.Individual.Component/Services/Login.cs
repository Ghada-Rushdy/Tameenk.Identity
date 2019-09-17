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
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration; 

        public Login(SignInManager<ApplicationUser> signInManager
            , UserManager<ApplicationUser> userManager , IConfiguration configuration)
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
                        GenerateToken generateToken = new GenerateToken(_configuration);
                        JwtSecurityToken token = generateToken.GenerateTokenJWT(user.Id,user.Email);

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
