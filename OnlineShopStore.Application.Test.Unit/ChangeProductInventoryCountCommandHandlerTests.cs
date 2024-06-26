using Moq;
using OnlineShopStore.Application.Command.Contracts.Commands;
using OnlineShopStore.Application.Command.Implementation;
using OnlineShopStore.Domain.DomainModel.Models.Product;
using OnlineShopStore.Domain.DomainModel.Repositories;
using OnlineShopStore.Infrastructure.Enums;
using OnlineShopStore.Infrastructure.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OnlineShopStore.Test.Utils.Utils;

namespace OnlineShopStore.Application.Test.Unit
{
    public  class ChangeProductInventoryCountCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ChangeProductInventoryCountCommandHandler _handler;

        public ChangeProductInventoryCountCommandHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IProductRepository>();
            _handler = new ChangeProductInventoryCountCommandHandler(_mockUnitOfWork.Object, _mockProductRepository.Object);
        }
        [Fact]
        public async Task Handle_ProductNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var command = new ChangeProductInventoryCountCommand
            {
                ProductId = 1,
                InventoryCount = 10
            };
            _mockProductRepository.Setup(repo => repo.Get(command.ProductId)).ReturnsAsync((Product)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            var expectedOperationResult = OperationResult.NotFound;
            var expectedError = "Product was not found";
            result.OperationResult.Should().Be(expectedOperationResult);
            result.Error.Should().Be(expectedError);
            _mockProductRepository.Verify(repo => repo.Get(command.ProductId), Times.Once);
        }

        [Fact]
        public async Task Handle_ProductFound_UpdatesInventoryCountAndCommits()
        {
            // Arrange
            var command = new ChangeProductInventoryCountCommand
            {
                ProductId = 1,
                InventoryCount = 10
            };
            var product = new ProductBuilder().Build();
            _mockProductRepository.Setup(repo => repo.Get(command.ProductId)).ReturnsAsync(product);
            _mockUnitOfWork.Setup(uow => uow.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            var expectedOperationResult = OperationResult.Succeeded;
            var expectedInventoryCount = 10;
            result.OperationResult.Should().Be(expectedOperationResult);
            product.InventoryCount.Should().Be(expectedInventoryCount);
            _mockProductRepository.Verify(repo => repo.Get(command.ProductId), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
