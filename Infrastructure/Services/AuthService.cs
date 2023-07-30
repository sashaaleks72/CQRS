using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Application.Auth.DTOs;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Configurations;
using Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using System.Net;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAmazonCognitoIdentityProvider _identityProvider;
        private readonly CognitoUserPool _userPool;
        private readonly ApplicationDbContext _dbContext;
        private readonly AmazonCognitoConfiguration _cognitoConfig;
        private readonly IMapper _mapper;

        public AuthService(IAmazonCognitoIdentityProvider identityProvider, CognitoUserPool userPool, ApplicationDbContext dbContext
            , IOptions<AmazonCognitoConfiguration> options, IMapper mapper) 
        {
            _identityProvider = identityProvider;
            _userPool = userPool;
            _dbContext = dbContext;
            _cognitoConfig = options.Value;
            _mapper = mapper;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto data)
        {
            var user = new CognitoUser(data.Email, _cognitoConfig.ClientId, _userPool, _identityProvider);
            var authRequest = new InitiateSrpAuthRequest
            {
                Password = data.Password
            };

            var authResponse = await user.StartWithSrpAuthAsync(authRequest);

            var expiresAt = DateTime.Now + TimeSpan.FromSeconds(authResponse.AuthenticationResult.ExpiresIn);

            var response = new LoginResponseDto
            {
                IdToken = authResponse.AuthenticationResult.IdToken,
                RefreshToken = authResponse.AuthenticationResult.RefreshToken,
                ExpiresAt = expiresAt
            };

            return response;
        }

        public async Task<string> RegisterAsync(RegisterRequestDto data)
        {
            bool isAdded = false;

            var request = new SignUpRequest
            {
                ClientId = _cognitoConfig.ClientId,
                Password = data.Password,
                Username = data.Username,
                UserAttributes = new List<AttributeType>
                {
                    new()
                    {
                        Name = "email",
                        Value = data.Email
                    },
                    new()
                    {
                        Name = "custom:role",
                        Value = "user"
                    },
                    new()
                    {
                        Name = "name",
                        Value = data.Username
                    }
                },
            };

            var signUpResponse = await _identityProvider.SignUpAsync(request);

            if (signUpResponse.HttpStatusCode == HttpStatusCode.OK)
            {
                var user = _mapper.Map<UserEntity>(data);
                user.RoleId = 1;

                try
                {
                    await _dbContext.Users.AddAsync(user);
                    isAdded = await _dbContext.SaveChangesAsync() > 0;
                }
                finally
                {
                    if (!isAdded)
                    {
                        var deleteRequest = new AdminDeleteUserRequest
                        {
                            Username = data.Username,
                            UserPoolId = _cognitoConfig.UserPoolId
                        };

                        await _identityProvider.AdminDeleteUserAsync(deleteRequest);
                    }
                    
                }
            }

            return isAdded ? $"User {data.Username} has been successfully registered. You need to verify you email now." : "User hasn't been registered.";
        }
    }
}
