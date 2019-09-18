using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tameenk.Identity.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Tameenk.Identity.Log.DAL;

namespace Tameenk.Identity.Individual.Component
{
    public class Register
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private IAuthenticationLogRepository _authenticationLogRepository;

        public Register(SignInManager<ApplicationUser> signInManager
            , UserManager<ApplicationUser> userManager, IConfiguration configuration, IAuthenticationLogRepository authenticationLogRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _authenticationLogRepository = authenticationLogRepository;

        }

        public async Task<IndividualRegisterOutput> UserRegister(IndividualRegisterModel model)
        {
            IndividualRegisterOutput output = new IndividualRegisterOutput();
            

            if (model == null)
            {
                Log(ErrorCodes.NullRequest, "Model is not valid", model.Email, model.UserName, model.Channel, model.Password);

                output.ErrorCode = IndividualRegisterOutput.ErrorCodes.NullRequest;
                output.ErrorDescription = "Model is not valid";
                output.Token = null;
                return output;
            }

            try
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    EmailConfirmed = true,
                    IsCompany=false,
                    PhoneNumber = model.Mobile,
                    PhoneNumberConfirmed = false,
                    UserName = model.UserName,
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,
                    RoleId = Guid.Parse("DB5159FA-D585-4FEE-87B1-D9290D515DFB"),
                    LanguageId = Guid.Parse("5046A00B-D915-48A1-8CCF-5E5DFAB934FB")

                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    Log(ErrorCodes.ServiceError, "User failed to register", model.Email, model.UserName, model.Channel, model.Password);


                    StringBuilder errorList = new StringBuilder();
                    result.Errors.ToList().ForEach(e => errorList.Append(e.Description + Environment.NewLine));

                    output.ErrorCode = IndividualRegisterOutput.ErrorCodes.Success;
                    output.ErrorDescription = "Can not create user, " + errorList.ToString();
                    output.Token = null;

                    return output;
                }
                else
                {
                    var registeredUser = await _userManager.FindByEmailAsync(model.Email);

                    if (registeredUser != null)
                    {
                        Log(ErrorCodes.Success, "User Registered Successfully", model.Email, model.UserName, model.Channel, model.Password);

                        GenerateToken generateToken = new GenerateToken(_configuration);
                        JwtSecurityToken token = generateToken.GenerateTokenJWT(user.Id, user.Email);

                        output.ErrorCode = IndividualRegisterOutput.ErrorCodes.Success;
                        output.ErrorDescription = "Registered Successfully";
                        output.Token = new JwtSecurityTokenHandler().WriteToken(token);

                        return output;
                    }
                    else
                    {
                        Log(ErrorCodes.Success, "User Not Found", model.Email, model.UserName, model.Channel, model.Password);

                        output.ErrorCode = IndividualRegisterOutput.ErrorCodes.NullResponse;
                        output.ErrorDescription = "Failed to create user";
                        output.Token = null;

                        return output;
                    }
                }

            }
            catch (Exception exp)
            {
                Log(ErrorCodes.Success, "UserRegister through exception", model.Email, model.UserName, model.Channel, model.Password);

                output.ErrorCode = IndividualRegisterOutput.ErrorCodes.MethodException;
                output.ErrorDescription = "UserRegister through exception";
                output.Token = null;

                return output;
            }         

        }

        public void Log(ErrorCodes ErrorCode ,string ErrorDescription , string Email , string UserName , int Channel , string Password )
        {
            AuthenticationLog log = new AuthenticationLog();            log.Method = "UserRegister";
            log.ServerIP = Utilities.GetServerIP();
            log.CreatedDate = DateTime.Now;
            log.Channel = Channel;
            log.ErrorCode = ErrorCode;
            log.ErrorDescription = ErrorDescription;
            log.Password = Password;
            log.Email = Email;
            log.UserName = UserName;
            _authenticationLogRepository.Insert(log);
        }

        public async Task<bool> SendTwoFactorCodeSmsAsync(ApplicationUser userId, string phoneNumber)
        {
            try
            {
                if (userId == null)
                {
                    return false;
                }
                // todo
                var token = await _userManager.GenerateChangePhoneNumberTokenAsync(userId, phoneNumber);
                // See IdentityConfig.cs to plug in Email/SMS services to actually send the code
                await _userManager.VerifyTwoFactorTokenAsync(userId, VerificationCodeResource.SmsTwoFactorCodeProviderName, token);
                return true;
            }
            catch
            {
                return false;
            }
        }



    }
}
