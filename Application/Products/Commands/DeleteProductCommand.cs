using Application.Common.Exceptions;
using Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using System.Net;

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
                throw new HttpException("The product with given id hasn't been deleted!", HttpStatusCode.BadRequest);
            }

            return "Product has been deleted!";
        }
    }
}
