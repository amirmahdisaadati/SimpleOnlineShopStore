using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using OnlineShopStore.Domain.DomainModel.Models.Order;
using OnlineShopStore.Domain.DomainModel.Models.User.Exeptions;

namespace OnlineShopStore.Domain.DomainModel.Models.User
{
    public class User
    {
        public User(string name)
        {
            ValidateUserName(name);
            Name = name;
        }
        public long Id { get; private set; }
        public string Name { get; private set; }

        private readonly List<Order.Order> _orders = new();
        public IEnumerable<Order.Order> Orders => _orders.AsReadOnly();




        #region Methods

        public void Buy(Order.Order order)
        {
            _orders.Add(order);
            order.Product.DecreaseInventoryCount();
        }

        public void SetIdForSeeding(long id)
        {
            this.Id = id;
        }


        #endregion

        #region Validation

        private void ValidateUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new EmptyUserNameException();
        }


        #endregion
        /// <summary>
        /// For EF
        /// </summary>
        protected User()
        {
            
        }
       

    }
}
