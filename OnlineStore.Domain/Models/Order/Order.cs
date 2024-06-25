﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.DomainModel.Models.Order
{
    public  class Order
    {
        public Order(User.User user ,Product.Product product)
        {
            this.CreationDate = DateTime.Now;
            this.Buyer = user;
            this.Product = product;
        }
        public long Id { get; private set; }
        public DateTime? CreationDate { get; private set; }
        public User.User Buyer { get;private set; }
        public Product.Product Product { get; private set; }
        public long BuyerId { get; private set; }
        public long ProductId { get; private set; }


    }
}
