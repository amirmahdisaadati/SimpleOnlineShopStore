using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Infrastructure.Enums;
using OnlineStore.Infrastructure.Persistence.Context;
using OnlineStore.Infrastructure.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineStore.Host.Api.Extentions
{
    public static class HostExtensions
    {
        public static ActionResult HttpResult(this Result result, object? data = null)
        {
            return result.OperationResult.ToString() switch
            {
                nameof(OperationResult.Succeeded) => new OkObjectResult(data),
                nameof(OperationResult.NotFound) => new NotFoundResult(),
                nameof(OperationResult.NotValid) => new BadRequestObjectResult(new Infrastructure.Shared.Error(result.Error)),
                nameof(OperationResult.Failed) => new StatusCodeResult(StatusCodes.Status500InternalServerError),
                _ => throw new NotImplementedException()
            };
        }
    }
}
