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
        public ProductsController(IMediator mediator) : base(mediator)
        {
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
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(UpdateProductRequestDto data)
        {
            var result = await _mediator.Send(new UpdateProductCommand(data));

            return Ok(result);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));

            return Ok(result);
        }

        [HttpPut("UploadProductImage")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UploadProductImage(Guid productId, IFormFile image)
        {
            using var fs = image.OpenReadStream();
            var result = await _mediator.Send(new UploadProductImageCommand(productId, fs, image.ContentType));

            return Ok(result);
        }
    }
}