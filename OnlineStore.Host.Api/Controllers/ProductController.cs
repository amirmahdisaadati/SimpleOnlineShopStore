using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShopStore.Application.Command.Contracts.Commands;
using OnlineShopStore.Application.Query.Contracts.Queries;
using OnlineShopStore.Host.Api.Extentions;

namespace OnlineShopStore.Host.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<GetProductByIdResponse>> GetProduct([FromRoute] long id)
        {
            var query = new GetProductByIdQuery() { ProductId = id };
            var result = await _mediator.Send(query);
            return result.HttpResult(result.Data);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ChangeProductInventoryCountResponse>> ChangeInventoryCount(
            [FromBody] ChangeProductInventoryCountCommand request)
        {
            var result = await _mediator.Send(request);
            return result.HttpResult(result.Data);
        }

        [HttpPost]
        public async Task<ActionResult<AddProductResponse>> AddProduct([FromBody] AddProductCommand request)
        {
            var result = await _mediator.Send(request);
            return result.HttpResult(result.Data);  
        }
    }
}
