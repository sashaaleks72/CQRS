
using Application.Interfaces.Repositories;
using Application.Products.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Products.Commands
{
    public record AddProductCommand(AddProductRequestDto Data) : IRequest<string>;

    public class AddProductHandler : IRequestHandler<AddProductCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IProductsRepository _repository;

        public AddProductHandler(IMapper mapper, IProductsRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<string> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = _mapper.Map<TeapotEntity>(request.Data);

            var isAdded = await _repository.AddProductAsync(productEntity);

            if (!isAdded) 
            {
                throw new Exception("The product hasn't been added!");
            }

            return "The product has been added!";
        }
    }
}
