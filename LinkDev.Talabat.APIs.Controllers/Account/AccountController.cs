using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Application.Abstraction.Models;
using LinkDev.Talabat.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Application.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Account
{
    public class AccountController(IServiceManager serviceManager):BaseApiController
    {
        [HttpPost("login")] // Post: /api/Account/login

        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var result = await serviceManager.AuthService.LoginAsync(model);

            return Ok(result);
        }

        [HttpPost("register")] // Post: /api/Account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var result = await serviceManager.AuthService.RegisterAsync(model);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]//Get : /api/account

        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var result = await serviceManager.AuthService.GetCurrentUser(User);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("address")] //Get : /api/account/address
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var result = await serviceManager.AuthService.GetUserAddress(User);
          
            return Ok(result);
        }
    }
}
