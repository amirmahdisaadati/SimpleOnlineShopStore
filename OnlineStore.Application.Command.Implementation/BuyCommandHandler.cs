﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShopStore.Application.Command.Contracts.Commands;
using OnlineShopStore.Domain.DomainModel.Models.Order;
using OnlineShopStore.Domain.DomainModel.Models.Product;
using OnlineShopStore.Domain.DomainModel.Repositories;
using OnlineShopStore.Infrastructure.Cache;
using OnlineShopStore.Infrastructure.Enums;
using OnlineShopStore.Infrastructure.Persistence.Context;
using OnlineShopStore.Infrastructure.Persistence.UnitOfWork;
using OnlineShopStore.Infrastructure.Shared;

namespace OnlineShopStore.Application.Command.Implementation
{
    public class BuyCommandHandler : IRequestHandler<BuyCommand, Result<BuyResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheProvider _cacheProvider;

        public BuyCommandHandler(IUserRepository userRepository, IProductRepository productRepository, IOrderRepository orderRepository, IUnitOfWork unitOfWork,ICacheProvider cacheProvider)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _cacheProvider = cacheProvider;
        }
        public async Task<Result<BuyResponse>> Handle(BuyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.Get(request.UserId);
                if (user is null)
                    return new Result<BuyResponse>(OperationResult.NotFound) { Error = "User Not Found" };
                Product product;
                var cacheProduct = await _cacheProvider.Get<Product>($"{request.ProductId}");
                if (cacheProduct is null)
                {
                    product = await _productRepository.Get(request.ProductId); ;
                }
                else
                {
                    product = cacheProduct;
                }

                if (product is null)
                    return new Result<BuyResponse>(OperationResult.NotFound) { Error = "Product Not Found" };

                var order = new Order(user, product);
                user.Buy(order);
                await _orderRepository.Add(order);
                await _unitOfWork.CommitAsync(cancellationToken);
                return new Result<BuyResponse>(OperationResult.Succeeded) { Data = new BuyResponse() { IsSuccess = true } };
            }
            catch (DbUpdateConcurrencyException e)
            {
                return new Result<BuyResponse>(OperationResult.Failed) { Error = "Concurrency Error,Please Try Again Later" };
            }
            


        }
    }
}
