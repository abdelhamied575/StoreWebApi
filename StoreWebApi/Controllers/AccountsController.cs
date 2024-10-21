using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWeb.Core.Dtos.Auth;
using StoreWeb.Core.Services.Contract;
using StoreWebApi.Errors;

namespace StoreWebApi.Controllers
{
    
    public class AccountsController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task <ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userService.LoginAsync(loginDto);

            if (user is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized, "Invalid Login"));

            return Ok(user);

             
        }

        [HttpPost("register")]
        public async Task <ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userService.RegisterAsync(registerDto);

            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid Registration")); 

            return Ok(user);

             
        }




    }
}  
