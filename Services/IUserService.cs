using System;
using System.Threading.Tasks;
using APIdemo.Models;

namespace APIdemo.Services
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(Guid id);
    }
}
