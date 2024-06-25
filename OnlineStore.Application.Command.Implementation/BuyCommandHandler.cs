using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Application.Command.Contracts.Commands;
using OnlineStore.Domain.DomainModel.Models.Order;
using OnlineStore.Domain.DomainModel.Repositories;
using OnlineStore.Infrastructure.Enums;
using OnlineStore.Infrastructure.Persistence.Context;
using OnlineStore.Infrastructure.Persistence.UnitOfWork;
using OnlineStore.Infrastructure.Shared;

namespace OnlineStore.Application.Command.Implementation
{
    public class BuyCommandHandler : IRequestHandler<BuyCommand, Result<BuyResponse>>
    {
        private readonly DatabaseContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BuyCommandHandler(IUserRepository userRepository, IProductRepository productRepository, IOrderRepository orderRepository, IUnitOfWork unitOfWork, DatabaseContext context)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<Result<BuyResponse>> Handle(BuyCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.UserId);
            if (user is null)
                return new Result<BuyResponse>(OperationResult.NotFound) { Error = "User Not Found" };
            var product = await _productRepository.Get(request.ProductId);
            if (product is null)
                return new Result<BuyResponse>(OperationResult.NotFound) { Error = "Product Not Found" };

            try
            {
                _context.Entry(product).Property(p => p.InventoryCount).OriginalValue = product.InventoryCount;
                var order = new Order(user, product);
                user.Buy(order);
                await _orderRepository.Add(order);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return new Result<BuyResponse>(OperationResult.Succeeded) { Data = new BuyResponse() };
            }
            catch (DbUpdateConcurrencyException e)
            {
                return new Result<BuyResponse>(OperationResult.Failed) { Error = "A concurrency error occurred. Please try again." };
            }
           
        }
    }
}
