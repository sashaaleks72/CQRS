using Application.Auth.DTOs;
using Application.Interfaces.Services;
using MediatR;

namespace Application.Auth.Commands
{
    public record RegisterCommand(RegisterRequestDto Data) : IRequest<string>;

    public class RegisterHandler : IRequestHandler<RegisterCommand, string>
    {
        private readonly IAuthService _authService;

        public RegisterHandler(IAuthService authService) 
        {
            _authService = authService;
        }

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(request.Data);

            return result;
        }
    }
}
