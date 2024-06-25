using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OnlineShopStore.Infrastructure.Shared;

namespace OnlineShopStore.Application.Command.Contracts.Commands
{
    public class AddProductCommand : IRequest<Result<AddProductResponse>>

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
