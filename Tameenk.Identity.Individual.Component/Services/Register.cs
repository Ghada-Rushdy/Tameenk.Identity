﻿using System;
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

namespace Tameenk.Identity.Individual.Component
{
    public class Register
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public Register(SignInManager<IdentityUser> signInManager
            , UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;


        }

        public async Task<IndividualRegisterOutput> UserRegister(IndividualRegisterModel model)
        {
            IndividualRegisterOutput output = new IndividualRegisterOutput();

            try
            {
                var user = new AspNetUsers
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
                    StringBuilder errorList = new StringBuilder();
                    result.Errors.ToList().ForEach(e => errorList.Append(e.Description + Environment.NewLine));

                    output.ErrorCode = IndividualRegisterOutput.ErrorCodes.Success;
                    output.ErrorDescription = "Can not create user, " + errorList.ToString();
                    output.Token = null;

                    return output;
                }
                else
                {
                    //if (!await SendTwoFactorCodeSmsAsync(user, user.PhoneNumber))
                    //{
                    //    output.ErrorCode = IndividualRegisterOutput.ErrorCodes.CanNotSendSMS;
                    //    output.ErrorDescription = "Can Not Send SMS";
                    //    output.Token = null;

                    //    return output;
                    //}

                    var registeredUser = await _userManager.FindByEmailAsync(model.Email);

                    if (registeredUser != null)
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

                        output.ErrorCode = IndividualRegisterOutput.ErrorCodes.Success;
                        output.ErrorDescription = "Registered Successfully";
                        output.Token = new JwtSecurityTokenHandler().WriteToken(token);

                        return output;
                    }
                    else
                    {
                        output.ErrorCode = IndividualRegisterOutput.ErrorCodes.NullResponse;
                        output.ErrorDescription = "Failed to create user";
                        output.Token = null;

                        return output;
                    }
                }

            }
            catch (Exception exp)
            {
                output.ErrorCode = IndividualRegisterOutput.ErrorCodes.MethodException;
                output.ErrorDescription = "UserRegister through exception";
                output.Token = null;

                return output;
            }         

        }

        public async Task<bool> SendTwoFactorCodeSmsAsync(AspNetUsers userId, string phoneNumber)
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
