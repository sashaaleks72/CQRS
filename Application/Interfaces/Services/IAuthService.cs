
using Application.Auth.DTOs;

namespace Application.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<string> RegisterAsync(RegisterRequestDto data);

        public Task<LoginResponseDto> LoginAsync(LoginRequestDto data);
    }
}
