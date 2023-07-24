using Amazon.S3;
using Amazon.S3.Model;
using Application.Common.DTOs;
using Application.Common.Models;
using Application.Products.Commands;
using Application.Products.DTOs;
using Application.Products.Queries;
using Infrastructure.Configurations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace WebAPI.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly AmazonS3Configuration _s3Config;

        public ProductsController(IMediator mediator, IAmazonS3 amazonS3, IOptions<AmazonS3Configuration> options) : base(mediator)
        {
            _amazonS3 = amazonS3;
            _s3Config = options.Value;
        }

        [HttpPost("Get")]
        [ProducesResponseType(typeof(PaginatedDataDto<GetProductResponseDto>), 200)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromBody] FilterParamsDto filterParams)
        {
            var result = await _mediator.Send(new GetProductsQuery(filterParams));

            return Ok(result);
        }

        [HttpGet("GetById")]
        [ProducesResponseType(typeof(GetProductResponseDto), 200)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetProductQuery(id));

            return Ok(result);
        }

        [HttpPost("Post")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post(AddProductRequestDto data)
        {
            var result = await _mediator.Send(new AddProductCommand(data));

            return Ok(result);
        }

        [HttpPut("Put")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(UpdateProductRequestDto data)
        {
            var result = await _mediator.Send(new UpdateProductCommand(data));

            return Ok(result);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));

            return Ok(result);
        }

        [HttpPut("UploadProductImage")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UploadProductImage(Guid productId, IFormFile image)
        {
            var fileName = $"{Guid.NewGuid().ToString()}.{image.ContentType.Split("/")[1]}";
            var request = new PutObjectRequest
            {
                BucketName = _s3Config.Name,
                Key = $"{_s3Config.Folders["TeapotImages"]}{fileName}",
                InputStream = image.OpenReadStream()
            };
;
            request.Metadata.Add("Content-Type", image.ContentType);

            var response = await _amazonS3.PutObjectAsync(request);
            //var result = await _mediator.Send(new DeleteProductCommand(id));

            return Ok(response.HttpStatusCode);
        }
    }
}