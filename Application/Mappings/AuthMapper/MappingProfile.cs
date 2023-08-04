using Application.Auth.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings.AuthMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<RegisterRequestDto, UserEntity>();
            CreateMap<UserEntity, UserInfoResponseDto>()
                .ForMember(dest => dest.Role, opts => opts.MapFrom(src => src.Role.Name));
        }
    }
}
