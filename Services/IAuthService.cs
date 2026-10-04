using System.Threading.Tasks;
using APIdemo.DTOs;

namespace APIdemo.Services
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(LoginDto dto);
        Task RegisterAsync(RegisterDto dto);
    }
}
