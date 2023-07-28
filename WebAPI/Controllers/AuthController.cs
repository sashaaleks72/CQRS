using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime.Internal;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Application.Auth.DTOs;
using Infrastructure.Configurations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        private readonly IAmazonCognitoIdentityProvider _identityProvider;
        private readonly CognitoUserPool _userPool;
        private readonly AmazonCognitoConfiguration _cognitoConfig;
        private readonly AmazonSimpleEmailServiceClient _sesClient;

        public AuthController(IMediator mediator, IAmazonCognitoIdentityProvider identityProvider, 
            CognitoUserPool userPool, IOptions<AmazonCognitoConfiguration> options, AmazonSimpleEmailServiceClient sesClient) : base(mediator)
        { 
            _identityProvider = identityProvider;
            _userPool = userPool;
            _cognitoConfig = options.Value;
            _sesClient = sesClient;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto data) 
        {
            var user = new CognitoUser(data.Email, _cognitoConfig.ClientId, _userPool, _identityProvider);
            var authRequest = new InitiateSrpAuthRequest
            {
                Password = data.Password
            };

            var authResponse = await user.StartWithSrpAuthAsync(authRequest);
            var expiresAt = DateTime.Now + TimeSpan.FromSeconds(authResponse.AuthenticationResult.ExpiresIn);

            return Ok(new { IdToken = authResponse.AuthenticationResult.IdToken, RefreshToken = authResponse.AuthenticationResult.RefreshToken, ExpiresAt = expiresAt });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequestDto data)
        {
            var request = new SignUpRequest
            {
                ClientId = _cognitoConfig.ClientId,
                Password = data.Password,
                Username = data.Username,
                UserAttributes = new List<AttributeType>
                {
                    new()
                    {
                        Name = "name",
                        Value = data.Username
                    },
                    new()
                    {
                        Name = "email",
                        Value = data.Email
                    },
                    new()
                    {
                        Name = "custom:role",
                        Value = "user"
                    }
                }
            };

            var sesRequest = new VerifyEmailAddressRequest
            {
                EmailAddress = data.Email
            };

            //var sesResponse = await _sesClient.VerifyEmailAddressAsync(sesRequest);

            var signUpResponse = await _identityProvider.SignUpAsync(request);
            
            return Ok(signUpResponse.HttpStatusCode);
        }
    }
}
