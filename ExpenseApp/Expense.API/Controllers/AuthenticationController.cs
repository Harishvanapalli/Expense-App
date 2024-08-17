using Expense.API.helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Data_Access_Layer.Models;
using Data_Access_Layer.Models.DTO_s;

namespace Expense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtTokenHandlerClass _jwtTokenHandler;
        public AuthenticationController(JwtTokenHandlerClass _jwtTokenHandler)
        {
            this._jwtTokenHandler = _jwtTokenHandler;  
        }

        //[HttpGet]
        //public string GetString()
        //{
        //    return "Hii";
        //}

        [HttpPost("authenticate")]

        public ActionResult<AuthenticationResponse> UserLogin([FromBody] UserDto user)
        {
            var response = _jwtTokenHandler.CreateToken(user);

            if (response == null) return Unauthorized();

            return response;
        }
    }
}
