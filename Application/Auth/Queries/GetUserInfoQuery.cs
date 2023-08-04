
using Application.Auth.DTOs;
using Application.Common.Exceptions;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Application.Auth.Queries
{
    public record GetUserInfoQuery() : IRequest<UserInfoResponseDto>;

    public class GetUserInfoHandler : IRequestHandler<GetUserInfoQuery, UserInfoResponseDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsersRepository _repository;
        private readonly IMapper _mapper;

        public GetUserInfoHandler(IHttpContextAccessor httpContextAccessor, IUsersRepository repository, IMapper mapper) 
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserInfoResponseDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var username = _httpContextAccessor.HttpContext!.User.Claims.SingleOrDefault(c => c.Type == "cognito:username")!.Value;
            
            var user = await _repository.GetUserByUserNameAsync(username);

            if (user == null)
            {
                throw new HttpException("User hasn't been found!", HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<UserInfoResponseDto>(user);

            return response;
        }
    }
}
