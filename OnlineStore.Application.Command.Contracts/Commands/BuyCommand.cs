using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OnlineStore.Infrastructure.Shared;

namespace OnlineStore.Application.Command.Contracts.Commands
{
    public class BuyCommand:IRequest<Result<BuyResponse>>
    {
        public long UserId { get; set; }
        public  long ProductId { get; set; }

    }

    public class BuyResponse
    {

    }
}
