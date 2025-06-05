using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using orchid_backend_net.API.Service;
using orchid_backend_net.Application.Common.Interfaces;

namespace orchid_backend_net.API.Configuration
{
    public static class ApplicationSecurityConfiguration
    {
        public static IServiceCollection ConfigureApplicationSecurity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<JwtService>();
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            services.AddHttpContextAccessor();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

            //    .AddJwtBearer(
            //        JwtBearerDefaults.AuthenticationScheme,
            //        options =>
            //        {
            //            options.Authority = "https://orchidlabsystem.azurewebsites.net/";
            //            options.Audience = "api";
            //            options.TokenValidationParameters.RoleClaimType = "role";
            //            options.SaveToken = true;
            //        });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = "https://orchidlabsystem.azurewebsites.net/",
                        ValidAudience = "api",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("orchid lab @PI 123abc456 pass ddoof ans nha troiwf owiiii")),
                    };
                });

            services.AddAuthorization(ConfigureAuthorization);

            return services;
        }


        private static void ConfigureAuthorization(AuthorizationOptions options)
        {

        }
    }
}
