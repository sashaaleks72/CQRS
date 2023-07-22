using Application.Common.DTOs;
using Application.Common.Exceptions;
using Application.Interfaces.Repositories;
using Application.Products.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Products.Queries
{
    public record GetProductsQuery(FilterParamsDto FilterParams) : IRequest<PaginatedDataDto<GetProductResponseDto>>;

    public class GetProductsHandler : IRequestHandler<GetProductsQuery, PaginatedDataDto<GetProductResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductsRepository _repository;

        public GetProductsHandler(IMapper mapper, IProductsRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<PaginatedDataDto<GetProductResponseDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var filter = request.FilterParams;
            var receivedPaginatedData = await _repository.GetProductsAsync(filter.Page, filter.Limit, filter.IsAscending, filter.SortField);

            if (receivedPaginatedData.Data.Count == 0)
            {
                throw new NotFoundException("There are no any rows with given params in db");
            }

            var paginatedDataDto = _mapper.Map<PaginatedDataDto<GetProductResponseDto>>(receivedPaginatedData);

            return paginatedDataDto;
        }
    }
}
