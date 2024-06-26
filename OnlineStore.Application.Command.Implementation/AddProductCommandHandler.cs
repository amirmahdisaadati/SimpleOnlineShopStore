using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OnlineShopStore.Application.Command.Contracts.Commands;
using OnlineShopStore.Domain.DomainModel.Models.Product;
using OnlineShopStore.Domain.DomainModel.Repositories;
using OnlineShopStore.Infrastructure.Cache;
using OnlineShopStore.Infrastructure.Enums;
using OnlineShopStore.Infrastructure.Persistence.UnitOfWork;
using OnlineShopStore.Infrastructure.Shared;

namespace OnlineShopStore.Application.Command.Implementation
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result<AddProductResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly ICacheProvider _cacheProvider;
        public AddProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository,
            ICacheProvider cacheProvider)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _cacheProvider = cacheProvider;
        }
        public async Task<Result<AddProductResponse>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            
            var isProductTitleUnique = await _productRepository.IsUniqueNameAsync(request.Title);
            if (!isProductTitleUnique)
                return new Result<AddProductResponse>(OperationResult.NotValid) { Error = "Product Name Must Be Unique" };
            var product = new Product(request.Title, request.Price, request.Discount, request.InventoryCount);
            var addProductTask = _productRepository.Add(product);
            var setCacheTask = _cacheProvider.Set($"{product.Id}", product, TimeSpan.FromDays(1));
            var commitTask = _unitOfWork.CommitAsync(cancellationToken);
            await Task.WhenAll(addProductTask, setCacheTask, commitTask);
            return new Result<AddProductResponse>(OperationResult.Succeeded) { Data = new AddProductResponse() };
        }
    }
}
