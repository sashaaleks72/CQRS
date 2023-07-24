using Application.Common.Exceptions;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Products.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest<string>;

    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, string>
    {
        private readonly IProductsRepository _repository;

        public DeleteProductHandler(IProductsRepository repository) 
        {
            _repository = repository;
        }

        public async Task<string> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = await _repository.DeleteProductByIdAsync(request.Id);

            if (!isDeleted)
            {
                throw new BadRequestException("The product with given id hasn't been deleted!");
            }

            return "Product has been deleted!";
        }
    }
}
