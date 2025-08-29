using LinkDev.Talabat.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Application.Exceptions;
using LinkDev.Talabat.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Services.Auth
{
    public class AuthService(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager) : IAuthServices
    {
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
                Token = "this will be token"
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
                Token = "this will be token"
            };
            return response;
        }
    }
}
