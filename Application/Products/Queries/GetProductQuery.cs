using Application.Common.Exceptions;
using Application.Interfaces.Repositories;
using Application.Products.DTOs;
using AutoMapper;
using MediatR;
using System.Net;

namespace Application.Products.Queries
{
    public record GetProductQuery(Guid Id) : IRequest<GetProductResponseDto>;

    public class GetProductHandler : IRequestHandler<GetProductQuery, GetProductResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IProductsRepository _repository;

        public GetProductHandler(IProductsRepository repository, IMapper mapper) 
        { 
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetProductResponseDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var receivedProduct = await _repository.GetProductByIdAsync(request.Id);

            if (receivedProduct == null)
            {
                throw new HttpException("Product with given id hasn't been found!", HttpStatusCode.NotFound);
            }

            var response = _mapper.Map<GetProductResponseDto>(receivedProduct);

            return response;
        }
    }
}
