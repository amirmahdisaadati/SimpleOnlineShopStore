using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OnlineShopStore.Application.Query.Contracts.Queries;
using OnlineShopStore.Domain.DomainModel.Repositories;
using OnlineShopStore.Infrastructure.Enums;
using OnlineShopStore.Infrastructure.Shared;

namespace OnlineShopStore.Application.Query.Implementation
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdResponse>>
    {

        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<GetProductByIdResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Get(request.ProductId);
            if (product is null)
                return new Result<GetProductByIdResponse>(OperationResult.NotFound) { Error = "Product was not found" };
            return new Result<GetProductByIdResponse>(OperationResult.Succeeded)
            {
                Data = new GetProductByIdResponse()
                {
                    Discount = product.Discount,
                    OriginalPrice = product.Price,
                    Title = product.Title,
                    FinalPrice = product.GetFinalPriceBasedOnDiscount()
                }
            };

        }
    }
}
