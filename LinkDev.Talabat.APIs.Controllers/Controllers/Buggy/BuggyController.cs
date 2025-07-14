using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Buggy
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("notfound")]
        //Get: /api/buggy/notfound
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(new ApiResponse( 404));//404
        }

        [HttpGet("servererror")] //Get: /api/buggy/servererror

        public IActionResult GetServerError()
        {
            throw new Exception();//500
        }

        [HttpGet("badrequest")] //Get : /api/buggy/badrequest

        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badrequest/{id}")] // GEt: /api/buggy/badrequest/five
        public IActionResult GetValidationError(int id) //=>400
        {
            return Ok();
        }

        [HttpGet("unathorized")] //Get: /api/buggy/unathorized
        public IActionResult GetUnauthorizedError()
        {
            return Unauthorized();//401
        }
        [HttpGet("forbidden")] //Get: /api/buggy/forbidden
        public IActionResult GetForbiddenError()
        {
            return Forbid();//401
        }

        [Authorize]
        [HttpGet("authorized")] //GEt : /api/Buggy/authorized
        public IActionResult GetAuthorizedRequst()
        {
            return Ok();
        }
    }
}