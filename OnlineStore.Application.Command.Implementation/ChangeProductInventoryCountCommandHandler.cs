using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OnlineShopStore.Application.Command.Contracts.Commands;
using OnlineShopStore.Domain.DomainModel.Repositories;
using OnlineShopStore.Infrastructure.Enums;
using OnlineShopStore.Infrastructure.Persistence.UnitOfWork;
using OnlineShopStore.Infrastructure.Shared;

namespace OnlineShopStore.Application.Command.Implementation
{
    public class ChangeProductInventoryCountCommandHandler : IRequestHandler<ChangeProductInventoryCountCommand, Result<ChangeProductInventoryCountResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public ChangeProductInventoryCountCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }
        public async Task<Result<ChangeProductInventoryCountResponse>> Handle(ChangeProductInventoryCountCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Get(request.ProductId);
            if (product is null)
                return new Result<ChangeProductInventoryCountResponse>(OperationResult.NotFound) { Error = "Product was not found" };
            product.UpdateInventoryCount(request.InventoryCount);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result<ChangeProductInventoryCountResponse>(OperationResult.Succeeded) { Data = new ChangeProductInventoryCountResponse() };

        }
    }
}
