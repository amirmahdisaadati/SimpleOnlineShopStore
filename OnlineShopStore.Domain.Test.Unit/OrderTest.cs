using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OnlineShopStore.Domain.DomainModel.Models.Order.Exceptions;
using OnlineShopStore.Domain.Test.Unit.Utils;

namespace OnlineShopStore.Domain.Test.Unit
{
    public  class OrderTest
    {
        [Fact]
        public void All_Orders_Should_Have_User_When_Order_Construct()
        {
            Action createOrder = () => new OrderBuilder()
                .WithUser(null)
                .Build();

            createOrder.Should().Throw<EmptyUserOrderException>();

        }

        [Fact]
        public void All_Orders_Should_Have_Product_When_Order_Construct()
        {
            Action createOrder = () => new OrderBuilder()
                .WithProduct(null)
                .Build();

            createOrder.Should().Throw<EmptyProductOrderException>();
        }

    }
}
