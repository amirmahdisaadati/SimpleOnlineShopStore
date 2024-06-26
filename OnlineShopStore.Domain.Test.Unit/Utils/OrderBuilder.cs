﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopStore.Domain.DomainModel.Models.Order;
using OnlineShopStore.Domain.DomainModel.Models.Product;
using OnlineShopStore.Domain.DomainModel.Models.User;

namespace OnlineShopStore.Domain.Test.Unit.Utils
{
    public  class OrderBuilder
    {
        private  User _user=new UserBuilder().Build();
        private Product _product=new ProductBuilder().Build();

        public OrderBuilder WithUser(User? user)

        {
            this._user = user;
            return this;
        }

        public OrderBuilder WithProduct(Product product)
        {
            this._product = product;
            return this;
        }
        public Order Build()
        {
            return new Order(_user, _product);
        }
    }
}
