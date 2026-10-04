using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APIdemo.Data;
using APIdemo.Models;

namespace APIdemo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _db.Users.FindAsync(id);
        }
    }
}
