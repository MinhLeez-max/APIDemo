using System;
using System.Threading.Tasks;
using APIdemo.Models;
using APIdemo.Repositories;

namespace APIdemo.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _users;

        public UserService(IUserRepository users)
        {
            _users = users;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _users.GetByIdAsync(id);
        }
    }
}
