using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OnlineShopStore.Application.Command.Contracts.Commands;
using OnlineShopStore.Domain.DomainModel.Models.Product;
using OnlineShopStore.Domain.DomainModel.Repositories;
using OnlineShopStore.Infrastructure.Enums;
using OnlineShopStore.Infrastructure.Persistence.UnitOfWork;
using OnlineShopStore.Infrastructure.Shared;

namespace OnlineShopStore.Application.Command.Implementation
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result<AddProductResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public AddProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }
        public async Task<Result<AddProductResponse>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            
            var isProductTitleUnique = await _productRepository.IsUniqueNameAsync(request.Title);
            if (!isProductTitleUnique)
                return new Result<AddProductResponse>(OperationResult.NotValid) { Error = "Product Name Must Be Unique" };
            var product = new Product(request.Title, request.Price, request.Discount, request.InventoryCount);
            await _productRepository.Add(product);
            _ = await _unitOfWork.CommitAsync(cancellationToken);
            return new Result<AddProductResponse>(OperationResult.Succeeded) { Data = new AddProductResponse() };
        }
    }
}
