using Moq;
using OnlineShopStore.Application.Command.Contracts.Commands;
using OnlineShopStore.Application.Command.Implementation;
using OnlineShopStore.Domain.DomainModel.Models.Product;
using OnlineShopStore.Domain.DomainModel.Models.User;
using OnlineShopStore.Domain.DomainModel.Repositories;
using OnlineShopStore.Infrastructure.Cache;
using OnlineShopStore.Infrastructure.Enums;
using OnlineShopStore.Infrastructure.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OnlineShopStore.Domain.DomainModel.Models.Order;
using OnlineShopStore.Domain.Test.Unit.Utils;

namespace OnlineShopStore.Application.Test.Unit
{
    public  class BuyCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ICacheProvider> _mockCacheProvider;
        private readonly BuyCommandHandler _handler;

        public BuyCommandHandlerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockCacheProvider = new Mock<ICacheProvider>();
            _handler = new BuyCommandHandler(_mockUserRepository.Object, _mockProductRepository.Object, _mockOrderRepository.Object, _mockUnitOfWork.Object, _mockCacheProvider.Object);
        }

        [Fact]
        public async Task Handle_UserNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var command = new BuyCommand { UserId = 1, ProductId = 1 };
            _mockUserRepository.Setup(repo => repo.Get(command.UserId)).ReturnsAsync((User)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            var expectedOperationResult = OperationResult.NotFound;
            var expectedError = "User Not Found";
            result.OperationResult.Should().Be(expectedOperationResult);
            result.Error.Should().Be(expectedError);
            _mockUserRepository.Verify(repo => repo.Get(command.UserId), Times.Once);
        }

        [Fact]
        public async Task Handle_ProductNotInCacheOrRepository_ReturnsNotFoundResult()
        {
            // Arrange
            var command = new BuyCommand { UserId = 1, ProductId = 1 };
            var user = new UserBuilder().WithName("Test User").Build();
            var product = new ProductBuilder().WithDiscount(10)
                .WithTitle("Test Product")
                .WithInventoryCount(10)
                .WithPrice(100)
                .Build();
            _mockUserRepository.Setup(repo => repo.Get(command.UserId)).ReturnsAsync(user);
            _mockCacheProvider.Setup(cache => cache.Get<Product>(It.IsAny<string>())).ReturnsAsync((Product)null);
            _mockProductRepository.Setup(repo => repo.Get(command.ProductId)).ReturnsAsync((Product)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            var expectedOperationResult = OperationResult.NotFound;
            var expectedError = "Product Not Found";
            result.OperationResult.Should().Be(expectedOperationResult);
            result.Error.Should().Be(expectedError);
            _mockUserRepository.Verify(repo => repo.Get(command.UserId), Times.Once);
            _mockCacheProvider.Verify(cache => cache.Get<Product>(It.IsAny<string>()), Times.Once);
            _mockProductRepository.Verify(repo => repo.Get(command.ProductId), Times.Once);
        }

        [Fact]
        public async Task Handle_SuccessfulPurchase_AddsOrderAndCommits()
        {
            // Arrange
            var command = new BuyCommand { UserId = 1, ProductId = 1 };
            var user = new UserBuilder().WithName("Test User").Build();
            var product = new ProductBuilder().WithDiscount(10)
                .WithTitle("Test Product")
                .WithInventoryCount(10)
                .WithPrice(100)
                .Build(); 

            _mockUserRepository.Setup(repo => repo.Get(command.UserId)).ReturnsAsync(user);
            _mockCacheProvider.Setup(cache => cache.Get<Product>(It.IsAny<string>())).ReturnsAsync((Product)null);
            _mockProductRepository.Setup(repo => repo.Get(command.ProductId)).ReturnsAsync(product);
            _mockOrderRepository.Setup(repo => repo.Add(It.IsAny<Order>())).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(uow => uow.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            var expectedOperationResult = OperationResult.Succeeded;
            result.OperationResult.Should().Be(expectedOperationResult);

            _mockUserRepository.Verify(repo => repo.Get(command.UserId), Times.Once);
            _mockCacheProvider.Verify(cache => cache.Get<Product>(It.IsAny<string>()), Times.Once);
            _mockProductRepository.Verify(repo => repo.Get(command.ProductId), Times.Once);
            _mockOrderRepository.Verify(repo => repo.Add(It.IsAny<Order>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
