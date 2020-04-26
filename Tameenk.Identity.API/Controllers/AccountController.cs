using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Tameenk.Identity.Company.Component;
using Tameenk.Identity.DAL;
using Tameenk.Identity.Individual.Component;
using Tameenk.Identity.Log.DAL;

namespace Tameenk.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private IAuthenticationLogRepository _authenticationLogRepository ;
        private IHttpContextAccessor _httpContextAccessor;

        public AccountController(UserManager<ApplicationUser> userManager  , SignInManager<ApplicationUser> signInManager , IConfiguration configuration,
                                 IAuthenticationLogRepository authenticationLogRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _authenticationLogRepository = authenticationLogRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody]LoginModel model)
        {
            LoginOutput loginOutput = new LoginOutput();

            if (ModelState.IsValid)
            {
                Login userLogin = new Login(_signInManager, _userManager , _configuration, _authenticationLogRepository);

                loginOutput = await userLogin.UserLogin(model);

                return Ok(loginOutput);
            }
            else
            {
                loginOutput.ErrorCode = LoginOutput.ErrorCodes.NullRequest;
                loginOutput.ErrorDescription = "Model is not valid";
                loginOutput.Token = null;

                return BadRequest(loginOutput);
            }
        }

        [HttpPost]
        [Route("IndividualRegister")]
        [AllowAnonymous]
        public async Task<IActionResult> IndividualRegister([FromBody]IndividualRegisterModel model)
        {
            IndividualRegisterOutput output = new IndividualRegisterOutput();

            if (model.IsValid)
            {
                Register registerUser = new Register(_signInManager, _userManager , _configuration , _authenticationLogRepository);
                output = await registerUser.UserRegister(model);

                return Ok(output);
            }
            else
            {
                output.ErrorCode = IndividualRegisterOutput.ErrorCodes.NullRequest;
                output.ErrorDescription = "Model is not valid";
                output.Token = null;

                return BadRequest(output);
            }

        }

        [HttpPost]
        [Route("CompanyRegister")]
        [AllowAnonymous]
        public async Task<IActionResult> CompanyRegister([FromBody]RegisterCompanyModel model)
        {
            CompanyRegisterOutput output = new CompanyRegisterOutput();

            if (model.IsValid)
            {
                RegisterCompany registerUser = new RegisterCompany(_signInManager, _userManager, _configuration, _authenticationLogRepository);
                output = await registerUser.UserRegister(model);

                return Ok(output);
            }
            else
            {
                output.ErrorCode = CompanyRegisterOutput.ErrorCodes.NullRequest;
                output.ErrorDescription = "Model is not valid";
                output.Token = null;

                return BadRequest(output);
            }

        }


        [HttpPost]
        [Route("LogoutUser")]
        [AllowAnonymous]
        public async Task<IActionResult> LogoutUser()
        {
            LogOutOutput logOutOutput = new LogOutOutput();

            Logout logout = new Logout(_signInManager , _userManager , _authenticationLogRepository , _httpContextAccessor);
            logOutOutput = await logout.UserLogOut();

            return Ok(logOutOutput);
        }


        [HttpGet]
        [Route("GetUser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser(string id)
        {
            return Ok(await _userManager.FindByIdAsync(id));
        }

    }
}