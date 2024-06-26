using FluentAssertions;
using OnlineShopStore.Domain.DomainModel.Models.Product.Exceptions;
using OnlineShopStore.Test.Utils.Utils;

namespace OnlineShopStore.Domain.Test.Unit
{
    public class ProductTest
    {
        [Fact]
        public void Product_Name_Cannot_Be_Empty_When_Construct_New_Product()
        {
            
            Action createProduct = () => new ProductBuilder()
                .WithTitle(string.Empty)
                .Build();
            createProduct.Should().Throw<EmptyProductNameException>();

        }

        [Fact]
        public void Product_Name_Must_Be_Less_Than_40_Characters()
        {
            Action createProduct = () => new ProductBuilder()
                .WithTitle(RandomStringGenerator.GenerateRandomString(50))
                .Build();
            createProduct.Should().Throw<InvalidProductNameException>();
        }

        [Fact]
        public void Product_Inventory_Count_Cannot_Be_Negative_Value()
        {
            Action createProduct = () => new ProductBuilder()
                .WithInventoryCount(-10)
                .Build();

            createProduct.Should().Throw<InvalidProductInventoryCountException>();
        }

        [Fact]
        public void Product_Price_Cannot_Be_Zero_When_Construct_New_Product()
        {
            Action createProduct = () => new ProductBuilder()
                .WithPrice(decimal.Zero)
                .Build();
            createProduct.Should().Throw<InvalidProductPriceException>();
        }

        [Fact]
        public void Discount_Of_Product_Price_Should_Be_Calculate_Properly()
        {
            var product = new ProductBuilder()
                .WithPrice(100)
                .WithDiscount(20)
                .Build();

            var finalPrice = product.GetFinalPriceBasedOnDiscount();

            var expectedFinalPrice = 80;
            finalPrice.Should().Be(expectedFinalPrice);
        }
    }
}