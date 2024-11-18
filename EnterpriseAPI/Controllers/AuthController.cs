using EnterpriseAPI.Contract;
using EnterpriseAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[EnableCors("AllowAllHeaders")]
	public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(LoginUser loginUser)
        {
            var result = await _authService.Register(loginUser);

			if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUser loginUser)
        {
            if (await _authService.Login(loginUser))
            {
                var token = _authService.GenerateToken(loginUser); 
                return Ok(new { Token = token, User = loginUser });
            }
            else
                return BadRequest("UserName/Password incorrect");
        }
    }
}
