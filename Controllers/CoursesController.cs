using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIdemo.Services;
using APIdemo.DTOs;

namespace APIdemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courses;

        public CoursesController(ICourseService courses)
        {
            _courses = courses;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _courses.GetAllAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseCreateDto dto)
        {
            var created = await _courses.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
    }
}
