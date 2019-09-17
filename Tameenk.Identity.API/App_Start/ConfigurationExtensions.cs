using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tameenk.Identity.DAL;

namespace Tameenk.Identity.API
{
    public static class ConfigurationExtensions
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName) where TModel : new()
        {
            var options = new TModel();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }

        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(
                options =>
                {
                    options.Password = new PasswordOptions
                    {
                        RequiredLength = 6,
                        RequireUppercase = false,
                        RequireNonAlphanumeric = false,
                        RequireDigit = false,
                        RequireLowercase = false
                    };
                    options.User.AllowedUserNameCharacters = string.Empty;
                }).AddUserValidator<UsernameValidator<AspNetUsers>>()
                .AddEntityFrameworkStores<TameenkIdentityDbContext>()
                .AddDefaultTokenProviders();

            IdentitySettings identitySettings;
            string IdentitySectionName = "identity";
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                identitySettings = configuration.GetOptions<IdentitySettings>(IdentitySectionName);
                services.Configure<IdentitySettings>(configuration.GetSection(IdentitySectionName));
            }

            // JWT
            var key = Encoding.UTF8.GetBytes(identitySettings.JWT_Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = identitySettings.RequireHttpsMetadata;
                x.SaveToken = identitySettings.SaveToken;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = identitySettings.ValidateIssuer,
                    ValidateAudience = identitySettings.ValidateAudience,
                    ClockSkew = TimeSpan.Zero
                };
            });

        }
    }
}
