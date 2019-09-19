using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tameenk.Identity.DAL;
using Tameenk.Identity.Log.DAL;

namespace Tameenk.Identity.Individual.Component
{
    public class Login
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private IAuthenticationLogRepository _authenticationLogRepository;

        public Login(SignInManager<ApplicationUser> signInManager
            , UserManager<ApplicationUser> userManager, IConfiguration configuration, IAuthenticationLogRepository authenticationLogRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _authenticationLogRepository = authenticationLogRepository;

        }

        public async Task<LoginOutput> UserLogin(LoginModel model)
        {
            LoginOutput output = new LoginOutput();

            if (model == null)
            {
                Log(ErrorCodes.NullRequest, "Model is Null" , null, null, null , null);

                output.ErrorCode = LoginOutput.ErrorCodes.NullRequest;
                output.ErrorDescription = "Model is not valid";
                output.Token = null;
                return output;
            }

            try
            {   
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                    if (result.Succeeded)
                    {
                        Log(ErrorCodes.Success, "Success authenticated User", model.UserName , model.Password , model.Email , model.Channel);


                        GenerateToken generateToken = new GenerateToken(_configuration);
                        JwtSecurityToken token = generateToken.GenerateTokenJWT(user.Id,user.Email);

                        output.ErrorCode = LoginOutput.ErrorCodes.Success;
                        output.ErrorDescription = "Success authenticated User";
                        output.Token = new JwtSecurityTokenHandler().WriteToken(token);

                        return output;                        
                    }
                    else
                    {
                        Log(ErrorCodes.NullResponse, "Incorrect password", model.UserName, model.Password, model.Email, model.Channel);

                        output.ErrorCode = LoginOutput.ErrorCodes.NullResponse;
                        output.ErrorDescription = "Incorrect password";
                        output.Token = null;

                        return output;
                    }
                }
                else
                {
                    Log(ErrorCodes.NullResponse, "User not exist", model.UserName, model.Password, model.Email, model.Channel);

                    output.ErrorCode = LoginOutput.ErrorCodes.NullResponse;
                    output.ErrorDescription = "User not exist";
                    output.Token = null;

                    return output;
                }
            }
            catch(Exception exp)
            {
                Log(ErrorCodes.MethodException, exp.ToString(), model.UserName, model.Password, model.Email, model.Channel);

                output.ErrorCode = LoginOutput.ErrorCodes.MethodException;
                output.ErrorDescription = "UserLogin through exception";
                output.Token = null;

                return output;
            }
          
        }

        public void Log(ErrorCodes ErrorCode, string ErrorDescription, string UserName , string Password, string Email , int? Channel)
        {
            AuthenticationLog log = new AuthenticationLog();            log.Method = "Login";
            log.ServerIP = Utilities.GetServerIP();
            log.CreatedDate = DateTime.Now;
            log.Channel = Channel;
            log.ErrorCode = ErrorCode;
            log.ErrorDescription = ErrorDescription;
            log.Password = Password;
            log.Email = Email;
            log.UserName =  UserName;
           
            _authenticationLogRepository.Insert(log);

        }


    }
}
