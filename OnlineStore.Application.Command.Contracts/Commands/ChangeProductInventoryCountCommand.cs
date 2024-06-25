using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OnlineShopStore.Infrastructure.Shared;

namespace OnlineShopStore.Application.Command.Contracts.Commands
{
    public class ChangeProductInventoryCountCommand : IRequest<Result<ChangeProductInventoryCountResponse>>
    {
        public long ProductId { get; set; }
        public int InventoryCount { get; set; }
    }

    public class ChangeProductInventoryCountResponse
    {

    }
}
