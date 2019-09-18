using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tameenk.Identity.DAL;

namespace Tameenk.Identity.Individual.Component
{
    public class Logout
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public Logout(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;           
        }

        public async Task<LogOutOutput> UserLogOut()
        {
            LogOutOutput logOutOutput = new LogOutOutput();

            try
            {  
                await _signInManager.SignOutAsync();

                logOutOutput.ErrorCode = LogOutOutput.ErrorCodes.Success;
                logOutOutput.ErrorDescription = "Success Logout";

                return logOutOutput;

            }
            catch (Exception exp)
            {               
                logOutOutput.ErrorCode = LogOutOutput.ErrorCodes.Success;
                logOutOutput.ErrorDescription = "UserLogOut Through exception";

                return logOutOutput;

            }

        }

     }
 }
