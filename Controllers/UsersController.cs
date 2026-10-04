using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIdemo.Services;

namespace APIdemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _users;

        public UsersController(IUserService users)
        {
            _users = users;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _users.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
