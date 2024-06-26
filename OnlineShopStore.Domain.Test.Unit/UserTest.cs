using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OnlineShopStore.Domain.DomainModel.Models.Order.Exceptions;
using OnlineShopStore.Domain.DomainModel.Models.User.Exeptions;
using OnlineShopStore.Test.Utils.Utils;

namespace OnlineShopStore.Domain.Test.Unit
{
    public  class UserTest
    {
        [Fact]
        public void Name_Of_User_Cannot_Be_Empty_When_User_Construct()
        {
            
            Action createUser = () => new UserBuilder().
                WithName(string.Empty).
                Build();

            createUser.Should().Throw<EmptyUserNameException>();
        }

        [Fact]
        public void Order_Should_Be_Submit_In_Orders_Of_User()
        {
            var user = new UserBuilder().Build();
            var product = new ProductBuilder().Build();
            var order = new OrderBuilder()
                .WithProduct(product)
                .WithUser(user)
                .Build();

            user.Buy(order);

            user.Orders.Should().NotBeEmpty().And.HaveCount(1);
            user.Orders.FirstOrDefault().Should().Be(order);
        }
        [Fact]
        public void Inventory_Count_Of_Product_In_Order_Should_Be_Decrease_When_User_Buy()
        {
            var user = new UserBuilder().Build();
            var product = new ProductBuilder().WithInventoryCount(100).Build();
            var order = new OrderBuilder()
                .WithProduct(product)
                .WithUser(user)
                .Build();

            user.Buy(order);

            var expectedInventoryCount = 99;
            product.InventoryCount.Should().Be(expectedInventoryCount);
        }
    }
}
