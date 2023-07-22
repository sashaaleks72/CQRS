using Application.Common.DTOs;
using Application.Products.DTOs;
using Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        public ProductsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("Get")]
        [ProducesResponseType(typeof(PaginatedDataDto<GetProductResponseDto>), 200)]
        public async Task<IActionResult> Get([FromBody] FilterParamsDto filterParams)
        {
            var result = await _mediator.Send(new GetProductsQuery(filterParams));

            return Ok(result);
        }

        [HttpGet("GetById")]
        [ProducesResponseType(typeof(GetProductResponseDto), 200)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetProductQuery(id));

            return Ok(result);
        }
    }
}