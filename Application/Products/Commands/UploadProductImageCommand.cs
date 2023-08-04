using Application.Common.Exceptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using MediatR;
using System.Net;

namespace Application.Products.Commands
{
    public record UploadProductImageCommand(Guid Id, Stream Image, string ContentType) : IRequest<string>;

    public class UploadProductImageHandler : IRequestHandler<UploadProductImageCommand, string>
    {
        private readonly IProductsRepository _repository;
        private readonly IImageService _imageService;

        public UploadProductImageHandler(IProductsRepository repository, IImageService imageService) 
        {
            _repository = repository;
            _imageService = imageService;
        }

        public async Task<string> Handle(UploadProductImageCommand request, CancellationToken cancellationToken)
        {
            var result = await _imageService.UploadImageAsync(request.Image, request.ContentType);

            if (result.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpException("Image hasn't been uploaded", HttpStatusCode.BadRequest);
            }

            var isUpdated = await _repository.UpdateProductImageAsync(request.Id, result.FileName);

            if (!isUpdated)
            {
                throw new HttpException("Product`s img name column hasn't been updated", HttpStatusCode.BadRequest);
            }

            return $"File {result.FileName} has been uploaded";
        }
    }
}
