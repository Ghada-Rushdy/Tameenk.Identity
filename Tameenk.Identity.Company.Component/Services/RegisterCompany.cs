using System;using System.Collections.Generic;using System.Text;using System.Threading.Tasks;using Tameenk.Identity.DAL;using Microsoft.AspNetCore.Authorization;using Microsoft.AspNetCore.Identity;using System.Linq;using Microsoft.Extensions.Configuration;using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Tameenk.Identity.Log.DAL;

namespace Tameenk.Identity.Company.Component{    public class RegisterCompany    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private IAuthenticationLogRepository _authenticationLogRepository;

        public RegisterCompany(SignInManager<ApplicationUser> signInManager
            , UserManager<ApplicationUser> userManager, IConfiguration configuration , IAuthenticationLogRepository authenticationLogRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _authenticationLogRepository = authenticationLogRepository;

        }        public async Task<CompanyRegisterOutput> UserRegister(RegisterCompanyModel model)        {
            CompanyRegisterOutput output = new CompanyRegisterOutput();

            if (model == null)
            {
                Log(ErrorCodes.NullRequest, "Model is Null", model);

                output.ErrorCode = CompanyRegisterOutput.ErrorCodes.NullRequest;
                output.ErrorDescription = "Model is not valid";
                output.Token = null;
                return output;
            }

            try
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    IsCompany = true,
                    EmailConfirmed = true,
                    PhoneNumber = model.Mobile,
                    PhoneNumberConfirmed = false,
                    CreatedDate = DateTime.Now,
                    LastModifiedDate = DateTime.Now,
                    LastLoginDate = DateTime.Now,
                    RoleId = Guid.Parse("DB5159FA-D585-4FEE-87B1-D9290D515DFB"),
                    LanguageId = Guid.Parse("5046A00B-D915-48A1-8CCF-5E5DFAB934FB")
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    Log(ErrorCodes.ServiceException, "User failed to register", model);

         
                    StringBuilder errorList = new StringBuilder();
                    result.Errors.ToList().ForEach(e => errorList.Append(e.Description + Environment.NewLine));

                    output.ErrorCode = CompanyRegisterOutput.ErrorCodes.ServiceException;
                    output.ErrorDescription = errorList.ToString();
                    output.Token = null;

                    return output;
                }
                else
                {
                    var registeredUser = await _userManager.FindByEmailAsync(model.Email);

                    if (registeredUser != null)
                    {
                        Log(ErrorCodes.Success, "User Registered Successfully", model);

                        GenerateToken generateToken = new GenerateToken(_configuration);
                        JwtSecurityToken token = generateToken.GenerateTokenJWT(user.Id, user.Email,user.UserName);

                        output.ErrorCode = CompanyRegisterOutput.ErrorCodes.Success;
                        output.ErrorDescription = "Registered Successfully";
                        output.Token = new JwtSecurityTokenHandler().WriteToken(token);

                        return output;
                    }
                    else
                    {
                        Log(ErrorCodes.NullResponse, "User not found", model);

                        output.ErrorCode = CompanyRegisterOutput.ErrorCodes.NullResponse;
                        output.ErrorDescription = "Failed to create user";
                        output.Token = null;

                        return output;
                    }
                }
            }
            catch (Exception exp)
            {
                Log(ErrorCodes.MethodException, exp.ToString() , model);

                output.ErrorCode = CompanyRegisterOutput.ErrorCodes.MethodException;
                output.ErrorDescription = "UserLogin through exception";
                output.Token = null;

                return output;
            }
        }

        public void Log(ErrorCodes ErrorCode, string ErrorDescription, RegisterCompanyModel model)
        {
            AuthenticationLog log = new AuthenticationLog();            log.Method = "CompanyRegister";
            log.ServerIP = Utilities.GetServerIP();
            log.CreatedDate = DateTime.Now;
            log.Channel = model?.Channel;
            log.ErrorCode = ErrorCode;
            log.ErrorDescription = ErrorDescription;
            log.Password = model?.Password;
            log.Email = model?.Email;
            log.UserName = model?.Email;
            log.CompanyCrNumber = model?.CompanyCrNumber;
            log.CompanyName = model?.CompanyName;
            log.CompanySponserId = model?.CompanySponserId;
            log.CompanyVatNumber = model?.CompanyVatNumber;
            _authenticationLogRepository.Insert(log);

        }
    }}