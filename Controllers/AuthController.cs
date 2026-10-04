using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIdemo.DTOs;
using APIdemo.Services;

namespace APIdemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            await _auth.RegisterAsync(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _auth.AuthenticateAsync(dto);
            if (token == null) return Unauthorized();
            return Ok(new { token });
        }
    }
}
