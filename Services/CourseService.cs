using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIdemo.DTOs;
using APIdemo.Models;
using APIdemo.Repositories;

namespace APIdemo.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courses;

        public CourseService(ICourseRepository courses)
        {
            _courses = courses;
        }

        public async Task<CourseDto> CreateAsync(CourseCreateDto dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                IsPublished = false
            };

            await _courses.AddAsync(course);

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Price = course.Price,
                IsPublished = course.IsPublished
            };
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            var items = await _courses.GetAllAsync();
            return items.Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Price = c.Price,
                IsPublished = c.IsPublished
            });
        }
    }
}
