using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APIdemo.Data;
using APIdemo.Models;

namespace APIdemo.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _db;

        public CourseRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Course course)
        {
            await _db.Courses.AddAsync(course);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _db.Courses.ToListAsync();
        }

        public async Task<Course> GetByIdAsync(Guid id)
        {
            return await _db.Courses.FindAsync(id);
        }
    }
}
