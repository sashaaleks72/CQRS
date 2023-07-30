using Application.Auth.Commands;
using Application.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequestDto data)
        {
            var response = await _mediator.Send(new RegisterCommand(data));

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto data) 
        {
            var response = await _mediator.Send(new LoginCommand(data));

            return Ok(response);
        }
    }
}
