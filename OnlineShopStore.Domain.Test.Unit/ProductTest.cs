using FluentAssertions;
using OnlineShopStore.Domain.DomainModel.Models.Product.Exceptions;
using OnlineShopStore.Domain.Test.Unit.Utils;

namespace OnlineShopStore.Domain.Test.Unit
{
    public class ProductTest
    {
        [Fact]
        public void Product_Name_Cannot_Be_Empty_When_Create_New_Product()
        {
            
            Action createProduct = () => new ProductBuilder()
                .WithTitle(string.Empty);
            createProduct.Should().Throw<EmptyProductNameException>();

        }

        [Fact]
        public void Product_Name_Must_Be_Less_Than_40_Char()
        {
            Action createProduct = () => new ProductBuilder()
                .WithTitle("")
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
        public void Product_Price_Cannot_Be_Zero_When_Constrcut_New_Product()
        {
            Action createProduct = () => new ProductBuilder()
                .WithPrice(decimal.Zero)
                .Build();
            createProduct.Should().Throw<InvalidProductPriceException>();
        }
    }
}