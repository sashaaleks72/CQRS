using Application.Auth.Commands;
using Application.Auth.DTOs;
using Application.Auth.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var response = await _mediator.Send(new GetUserInfoQuery());

            return Ok(response);
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
