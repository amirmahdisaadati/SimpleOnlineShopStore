using Moq;
using OnlineShopStore.Application.Query.Contracts.Queries;
using OnlineShopStore.Application.Query.Implementation;
using OnlineShopStore.Domain.DomainModel.Models.Product;
using OnlineShopStore.Domain.DomainModel.Repositories;
using OnlineShopStore.Infrastructure.Cache;
using OnlineShopStore.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OnlineShopStore.Test.Utils.Utils;

namespace OnlineShopStore.Application.Test.Unit
{
    public  class GetProductByIdQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<ICacheProvider> _mockCacheProvider;
        private readonly GetProductByIdQueryHandler _handler;

        public GetProductByIdQueryHandlerTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockCacheProvider = new Mock<ICacheProvider>();
            _handler = new GetProductByIdQueryHandler(_mockProductRepository.Object, _mockCacheProvider.Object);
        }

        [Fact]
        public async Task Handle_ProductFoundInCache_ReturnsProductFromCache()
        {
            // Arrange
            var query = new GetProductByIdQuery { ProductId = 1 };
            var cacheProduct = new ProductBuilder().WithTitle("Cached Product")
                .WithPrice(100)
                .WithDiscount(10)
                .Build();
            _mockCacheProvider.Setup(cache => cache.Get<Product>($"{query.ProductId}")).ReturnsAsync(cacheProduct);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            var expectedOperationResult = OperationResult.Succeeded;
            var expectedDiscount = 10;
            var expectedOriginalPrice = 100;
            var expectedFinalPrice = 90;
            var expectedProductTitle = "Cached Product";
            result.OperationResult.Should().Be(expectedOperationResult);
            result.Data.Title.Should().Be(expectedProductTitle);
            result.Data.Discount.Should().Be(expectedDiscount);
            result.Data.OriginalPrice.Should().Be(expectedOriginalPrice);
            result.Data.FinalPrice.Should().Be(expectedFinalPrice);
            _mockCacheProvider.Verify(cache => cache.Get<Product>($"{query.ProductId}"), Times.Once);
            _mockProductRepository.Verify(repo => repo.Get(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ProductNotFoundInCacheOrRepository_ReturnsNotFoundResult()
        {
            // Arrange
            var query = new GetProductByIdQuery { ProductId = 1 };
            _mockCacheProvider.Setup(cache => cache.Get<Product>($"{query.ProductId}")).ReturnsAsync((Product)null);
            _mockProductRepository.Setup(repo => repo.Get(query.ProductId)).ReturnsAsync((Product)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            var expectedOperationResult = OperationResult.NotFound;
            var expectedError = "Product was not found";
            result.OperationResult.Should().Be(expectedOperationResult);
            result.Error.Should().Be(expectedError);
            _mockCacheProvider.Verify(cache => cache.Get<Product>($"{query.ProductId}"), Times.Once);
            _mockProductRepository.Verify(repo => repo.Get(query.ProductId), Times.Once);
        }

        [Fact]
        public async Task Handle_ProductFoundInRepository_ReturnsProductFromRepository()
        {
            // Arrange
            var query = new GetProductByIdQuery { ProductId = 1 };
            var product = new ProductBuilder().WithTitle("Repository Product")
                .WithPrice(200)
                .WithDiscount(20)
                .Build();
            _mockCacheProvider.Setup(cache => cache.Get<Product>($"{query.ProductId}")).ReturnsAsync((Product)null);
            _mockProductRepository.Setup(repo => repo.Get(query.ProductId)).ReturnsAsync(product);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            
            // Assert
            var expectedOperationResult = OperationResult.Succeeded;
            var expectedDiscount = 20;
            var expectedOriginalPrice = 200;
            var expectedFinalPrice = 160;
            var expectedProductTitle = "Repository Product";

            result.OperationResult.Should().Be(expectedOperationResult);
            result.Data.Title.Should().Be(expectedProductTitle);
            result.Data.Discount.Should().Be(expectedDiscount);
            result.Data.OriginalPrice.Should().Be(expectedOriginalPrice);
            result.Data.FinalPrice.Should().Be(expectedFinalPrice);
            _mockCacheProvider.Verify(cache => cache.Get<Product>($"{query.ProductId}"), Times.Once);
            _mockProductRepository.Verify(repo => repo.Get(query.ProductId), Times.Once);
        }
    }
}
