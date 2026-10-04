using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIdemo.DTOs;

namespace APIdemo.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllAsync();
        Task<CourseDto> CreateAsync(CourseCreateDto dto);
    }
}
