using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIdemo.Models;

namespace APIdemo.Repositories
{
    public interface ICourseRepository
    {
        Task<Course> GetByIdAsync(Guid id);
        Task<IEnumerable<Course>> GetAllAsync();
        Task AddAsync(Course course);
    }
}
