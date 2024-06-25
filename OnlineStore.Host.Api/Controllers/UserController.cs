using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Command.Contracts.Commands;
using OnlineStore.Host.Api.Extentions;

namespace OnlineStore.Host.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("buy")]
        public async Task<ActionResult<BuyResponse>> Buy([FromBody]BuyCommand request)
        {
            var result = await _mediator.Send(request);
            return result.HttpResult(result.Data);
        }
    }
}
