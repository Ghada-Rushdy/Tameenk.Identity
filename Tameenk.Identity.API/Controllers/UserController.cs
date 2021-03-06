﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Tameenk.Identity.Company.Component;
using Tameenk.Identity.DAL;
using Tameenk.Identity.Individual.Component;

namespace Tameenk.Identity.API.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserController(SignInManager<IdentityUser> signInManager
            , UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;           
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginModel model)
        {
            LoginOutput loginOutput = new LoginOutput();

            if (ModelState.IsValid)
            {
                Login userLogin = new Login(_signInManager, _userManager, _configuration);

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
                Register registerUser = new Register(_signInManager, _userManager,_configuration);
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
        
        [HttpPost]        [Route("CompanyRegister")]        [AllowAnonymous]        public async Task<IActionResult> CompanyRegister([FromBody]RegisterCompanyModel model)        {            CompanyRegisterOutput output = new CompanyRegisterOutput();            if (model.IsValid)            {                RegisterCompany registerUser = new RegisterCompany(_signInManager, _userManager, _configuration);                output = await registerUser.UserRegister(model);                return Ok(output);            }            else            {                output.ErrorCode = CompanyRegisterOutput.ErrorCodes.NullRequest;                output.ErrorDescription = "Model is not valid";                output.Token = null;                return BadRequest(output);            }        }

        [HttpPost]
        [Route("LogoutUser")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "User");
        }

        public ActionResult Index()
        {
            return Ok();
        }
    }
}
