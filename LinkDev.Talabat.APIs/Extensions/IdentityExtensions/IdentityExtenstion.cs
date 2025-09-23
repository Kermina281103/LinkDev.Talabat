
using LinkDev.Talabat.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Application.Services.Auth;
using LinkDev.Talabat.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using LinkDev.Talabat.Shared.Models.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LinkDev.Talabat.APIs.Extensions.IdentityExtensions
{
    public static class IdentityExtenstion
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services,IConfiguration configuration )
        {
            Services.Configure<JwtSettings>((configuration.GetSection("jwtSettings")));
           Services.AddIdentity<ApplicationUser, IdentityRole>(identityOptions =>
            {
                identityOptions.User.RequireUniqueEmail = true;
                // identityOptions.User.AllowedUserNameCharacters = "abcdefghijklmnopqur#$%G";

                identityOptions.SignIn.RequireConfirmedAccount = true;//alway will be true 
                identityOptions.SignIn.RequireConfirmedEmail = true;
                identityOptions.SignIn.RequireConfirmedPhoneNumber = true;

                identityOptions.Password.RequireNonAlphanumeric = true;// #$@
                identityOptions.Password.RequiredUniqueChars = 1;// 1 is default 
                identityOptions.Password.RequiredLength = 6;
                identityOptions.Password.RequireDigit = true;
                identityOptions.Password.RequireLowercase = false;
                identityOptions.Password.RequireUppercase = false;

                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 10;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);




            })
               .AddEntityFrameworkStores<StoreIdentityDbContext>();

            Services.AddScoped(typeof(IAuthServices), typeof(AuthService));

            Services.AddScoped(typeof(Func<IAuthServices>), (serviceProvider) =>
            {
                return () => serviceProvider.GetService<IAuthServices>();
            });

            Services.AddAuthentication((authonticationOptions) =>
            {
                authonticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer((configurationOptions) =>
                {
                    configurationOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,

                        ClockSkew=TimeSpan.FromMinutes(0),
                        ValidIssuer = configuration["jwtSettings:Issuer"],
                        ValidAudience = configuration["jwtSettings:Audience"],
                        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtSettings:Key"]!))
                    };
                });
                
                


            return Services;
        }
    }
}
