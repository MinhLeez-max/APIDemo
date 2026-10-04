using System;
using System.Threading.Tasks;
using BCrypt.Net;
using APIdemo.DTOs;
using APIdemo.Models;
using APIdemo.Repositories;

namespace APIdemo.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;

        public AuthService(IUserRepository users)
        {
            _users = users;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var existing = await _users.GetByEmailAsync(dto.Email);
            if (existing != null) throw new InvalidOperationException("Email already in use");

            var user = new User
            {
                Email = dto.Email,
                FullName = dto.FullName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "Student"
            };

            await _users.AddAsync(user);
        }

        public async Task<string> AuthenticateAsync(LoginDto dto)
        {
            var user = await _users.GetByEmailAsync(dto.Email);
            if (user == null) return null;

            var valid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!valid) return null;

            // Return a simple stub token (replace with JWT in real impl)
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
