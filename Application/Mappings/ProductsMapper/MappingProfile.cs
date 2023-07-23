using Application.Common.DTOs;
using Application.Common.Models;
using Application.Products.DTOs;
using Application.Products.Queries;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings.ProductsMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<TeapotEntity, GetProductResponseDto>();
            CreateMap<PaginatedData<TeapotEntity>, PaginatedDataDto<GetProductResponseDto>>();
            CreateMap<AddProductRequestDto, TeapotEntity>();
            CreateMap<UpdateProductRequestDto, TeapotEntity>();
        }
    }
}
