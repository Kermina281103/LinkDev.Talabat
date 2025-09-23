using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Shared.Models;
using LinkDev.Talabat.Shared.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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
        public async Task<ActionResult<AddressDto>> GetUserAddress() //Need to exchange to address not address dto found in Domain .Identity 
       {
           var result = await serviceManager.AuthService.GetUserAddress(User);
         
           return Ok(result);
       }

        [Authorize]
        [HttpPut("address")] //Put : /api/account/address
        
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var result = await serviceManager.AuthService.UpdateUserAddress(User,address);
           
            return Ok(result);


        }
        [Authorize]
        [HttpGet("emailexists")] //Get: /api/account/emaiexists/carmina.maged.matta@gmail.com

        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            
            var result = await serviceManager.AuthService.CheckEmailExists(email!);
            return Ok(result);
        }
    }
}
