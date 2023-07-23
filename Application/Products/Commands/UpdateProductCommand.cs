using Application.Interfaces.Repositories;
using Application.Products.DTOs;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Products.Commands
{
    public record UpdateProductCommand(UpdateProductRequestDto Data) : IRequest<string>;

    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IProductsRepository _repository;

        public UpdateProductHandler(IMapper mapper, IProductsRepository repository) 
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<string> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var updatedProduct = _mapper.Map<TeapotEntity>(request.Data);

            var isUpdated = await _repository.UpdateProductAsync(updatedProduct);

            if (!isUpdated) 
            {
                throw new Exception("Product hasn't been updated");
            }

            return "Product has been updated";
        }
    }
}
