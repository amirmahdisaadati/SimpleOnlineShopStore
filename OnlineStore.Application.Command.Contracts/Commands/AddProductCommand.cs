using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OnlineStore.Infrastructure.Shared;

namespace OnlineStore.Application.Command.Contracts.Commands
{
    public class AddProductCommand:IRequest<Result<AddProductResponse>>

    {
    public string Title { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }

    public int InventoryCount { get; set; }
    }

    public class AddProductResponse
    {

    }
}
