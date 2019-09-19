using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Tameenk.Identity.DAL;
using Tameenk.Identity.Log.DAL;
using ErrorCodes = Tameenk.Identity.Log.DAL.ErrorCodes;

namespace Tameenk.Identity.Individual.Component
{
    public class Logout
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IAuthenticationLogRepository _authenticationLogRepository;
        private IHttpContextAccessor _httpContextAccessor;

        public Logout(SignInManager<ApplicationUser> signInManager , UserManager<ApplicationUser> userManager , IAuthenticationLogRepository authenticationLogRepository
                      , IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authenticationLogRepository = authenticationLogRepository;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<LogOutOutput> UserLogOut()
        {
            LogOutOutput logOutOutput = new LogOutOutput();

            var userId = _httpContextAccessor.HttpContext.User.Identity.Name;
            //ApplicationUser user = _userManager.FindByIdAsync(userId);
            try
            {  
                await _signInManager.SignOutAsync();

                logOutOutput.ErrorCode = LogOutOutput.ErrorCodes.Success;
                logOutOutput.ErrorDescription = "Success Logout";

                return logOutOutput;

            }
            catch (Exception exp)
            {
                //Log(ErrorCodes.MethodException, exp.ToString(), model);

                logOutOutput.ErrorCode = LogOutOutput.ErrorCodes.MethodException;
                logOutOutput.ErrorDescription = "UserLogOut Through exception";

                return logOutOutput;

            }

        }



        public void Log(ErrorCodes ErrorCode, string ErrorDescription, ApplicationUser user, int Channel)
        {
            AuthenticationLog log = new AuthenticationLog();            log.Method = "LogOut";
            log.ServerIP = Utilities.GetServerIP();
            log.CreatedDate = DateTime.Now;
            log.Channel = Channel;
            log.ErrorCode = ErrorCode;
            log.ErrorDescription = ErrorDescription;
            //log.Password = user.Password;
            log.Email = user.Email;
            log.UserName = user.UserName;
            _authenticationLogRepository.Insert(log);
        }




    }
 }
