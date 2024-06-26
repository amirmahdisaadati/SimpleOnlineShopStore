using Moq;
using OnlineShopStore.Application.Command.Contracts.Commands;
using OnlineShopStore.Application.Command.Implementation;
using OnlineShopStore.Domain.DomainModel.Models.Product;
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

namespace OnlineShopStore.Application.Test.Unit
{
    public  class AddProductCommandHandlerTests
    {

        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<ICacheProvider> _mockCacheProvider;
        private readonly AddProductCommandHandler _handler;

        public AddProductCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockCacheProvider = new Mock<ICacheProvider>();
            _handler = new AddProductCommandHandler(_mockUnitOfWork.Object, _mockProductRepository.Object, _mockCacheProvider.Object);
        }

        [Fact]
        public async Task Handle_ProductTitleNotUnique_ReturnsNotValidResult()
        {
            // Arrange
            var command = new AddProductCommand
            {
                Title = "Test Product",
                Price = 100,
                Discount = 1,
                InventoryCount = 10
            };
            _mockProductRepository.Setup(repo => repo.IsUniqueNameAsync(command.Title)).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);


            // Assert
            var expectedResult = OperationResult.NotValid;
            result.OperationResult.Should().Be(expectedResult);
            result.Error.Should().Be("Product Name Must Be Unique");
            _mockProductRepository.Verify(repo => repo.IsUniqueNameAsync(command.Title), Times.Once);
        }

        [Fact]
        public async Task Handle_ProductTitleUnique_AddsProductAndCommits()
        {
            var command = new AddProductCommand
            {
                Title = "Unique Product",
                Price = 100,
                Discount = 1,
                InventoryCount = 10
            };
            _mockProductRepository.Setup(repo => repo.IsUniqueNameAsync(command.Title)).ReturnsAsync(true);
            _mockProductRepository.Setup(repo => repo.Add(It.IsAny<Product>())).Returns(Task.CompletedTask);
            _mockCacheProvider.Setup(cache => cache.Set(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<TimeSpan>())).ReturnsAsync(true);
            _mockUnitOfWork.Setup(uow => uow.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

            var result = await _handler.Handle(command, CancellationToken.None);


            var expectedResult = OperationResult.Succeeded;
            result.OperationResult.Should().Be(expectedResult);
            _mockProductRepository.Verify(repo => repo.IsUniqueNameAsync(command.Title), Times.Once);
            _mockProductRepository.Verify(repo => repo.Add(It.IsAny<Product>()), Times.Once);
            _mockCacheProvider.Verify(cache => cache.Set(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<TimeSpan>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
