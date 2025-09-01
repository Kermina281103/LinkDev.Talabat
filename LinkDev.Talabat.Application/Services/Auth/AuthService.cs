using LinkDev.Talabat.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Application.Exceptions;
using LinkDev.Talabat.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LinkDev.Talabat.Application.Services.Auth
{
    public class AuthService(IOptions<JwtSettings> jwtSettings
        ,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager) : IAuthServices
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        public async Task<UserDto> LoginAsync(LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user is null) throw new UnAuthorizedExceptoin("Invalid Login. ");
            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

            if (result.IsNotAllowed) throw new UnAuthorizedExceptoin("Account not confirmed yet.");

            if(result.IsLockedOut) throw new UnAuthorizedExceptoin("Account Is Locked");

            // if(result.RequiresTwoFactor) throw new UnAuthorizedExceptoin("Require 2 Factor Authontication ");
            if (!result.Succeeded) throw new UnAuthorizedExceptoin("Login Invalid.");

            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await GenerateTokenAsync(user)
            };
            return response;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto model)
        {
            var user = new ApplicationUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber
            };
            var result = await userManager.CreateAsync(user,model.Password);

            if (!result.Succeeded) throw new ValidationExceptions() { Errors = result.Errors.Select(E => E.Description) };
            var response = new UserDto()
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await GenerateTokenAsync(user)
            };
            return response;
        }
   
       public async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var UserClaims = await userManager.GetClaimsAsync(user);
            var roleClaims = new List<Claim>();

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
                roleClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));

            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.PrimarySid,user.Id),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.GivenName,user.DisplayName)
            }
            .Union(UserClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signinCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            var tokenobj = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                claims: Claims,
                signingCredentials: signinCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenobj);
        }

        public async Task<UserDto> GetCurrentUser(ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email!);

            return new UserDto()
            {
                Id = user!.Id,
                Email = user!.Email!,
                DisplayName = user.DisplayName,
                Token = await GenerateTokenAsync(user)
            };
        }
    }
}
