using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using OnlineShopStore.Application.Query.Contracts.Queries;
using OnlineShopStore.Domain.DomainModel.Models.Product;
using OnlineShopStore.Domain.DomainModel.Repositories;
using OnlineShopStore.Infrastructure.Cache;
using OnlineShopStore.Infrastructure.Enums;
using OnlineShopStore.Infrastructure.Shared;

namespace OnlineShopStore.Application.Query.Implementation
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdResponse>>
    {

        private readonly IProductRepository _productRepository;
        private readonly ICacheProvider _cacheProvider;
        public GetProductByIdQueryHandler(IProductRepository productRepository,ICacheProvider cacheProvider)
        {
            _productRepository = productRepository;
            _cacheProvider = cacheProvider;
        }

        public async Task<Result<GetProductByIdResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheProduct = await _cacheProvider.Get<Product>($"{request.ProductId}");
            if (cacheProduct is not null)
            {
                return new Result<GetProductByIdResponse>(OperationResult.Succeeded)
                {
                    Data = new GetProductByIdResponse()
                    {
                        Discount = cacheProduct.Discount,
                        OriginalPrice = cacheProduct.Price,
                        Title = cacheProduct.Title,
                        FinalPrice = cacheProduct.GetFinalPriceBasedOnDiscount()
                    }
                };
            }
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
