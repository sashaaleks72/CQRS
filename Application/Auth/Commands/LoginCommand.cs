using Application.Auth.DTOs;
using Application.Interfaces.Services;
using MediatR;

namespace Application.Auth.Commands
{
    public record LoginCommand(LoginRequestDto Data) : IRequest<LoginResponseDto>;

    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IAuthService _authService;

        public LoginHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(request.Data);
            
            return result;
        }
    }
}
