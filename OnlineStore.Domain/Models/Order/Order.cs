using OnlineShopStore.Domain.DomainModel.Models.Order.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopStore.Domain.DomainModel.Models.Order
{
    public class Order
    {
        public Order(User.User? user, Product.Product? product)
        {
            ValidateUserOrder(user);
            ValidateProductOrder(product);
            CreationDate = DateTime.Now;
            Buyer = user;
            Product = product;
        }
        public long Id { get; private set; }
        public DateTime? CreationDate { get; private set; }
        public User.User Buyer { get; private set; }
        public Product.Product Product { get; private set; }
        public long BuyerId { get; private set; }
        public long ProductId { get; private set; }


        private void ValidateUserOrder(User.User
            ? user
        )
        {
            if (user is null)
                throw new EmptyUserOrderException();
        }

        private void ValidateProductOrder(Product.Product? product)
        {
            if (product is null)
                throw new EmptyProductOrderException();
        }

        /// <summary>
        /// For EF
        /// </summary>
        private Order()
        {
            
        }

    }
}
