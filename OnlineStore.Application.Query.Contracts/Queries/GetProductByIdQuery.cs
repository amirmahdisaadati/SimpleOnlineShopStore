using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OnlineStore.Infrastructure.Shared;

namespace OnlineStore.Application.Query.Contracts.Queries
{
    public class GetProductByIdQuery: IRequest<Result<GetProductByIdResponse>>
    {
        public long ProductId { get; set; }
    }

    public class GetProductByIdResponse
    {
        public string Title { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
