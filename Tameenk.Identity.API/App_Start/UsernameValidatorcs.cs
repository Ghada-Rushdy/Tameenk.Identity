﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tameenk.Identity.DAL;

namespace Tameenk.Identity.API
{
    public class UsernameValidator<TUser> : IUserValidator<TUser>
                where TUser : ApplicationUser
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            if (user.UserName.Any(x => x == ':' || x == ';' || x == ' ' || x == ',') || user.Email.Any(x => x == ':' || x == ';' || x == ' ' || x == ','))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "InvalidCharactersUsername",
                    Description = "Username or email can not contain ':', ';', ' ' or ','"
                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
