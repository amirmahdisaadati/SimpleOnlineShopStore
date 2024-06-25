using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Domain.DomainModel.Models.User.Exeptions;

namespace OnlineStore.Domain.DomainModel.Models.User
{
    public class User
    {
        public User(string name)
        {
            ValidateUserName(name);
                this.Name = name;
        }
        public long Id { get; private set; }
        public  string Name { get; private  set; }

        private readonly List<Order.Order> _orders = new();
        public IEnumerable<Order.Order> Orders=> _orders.AsReadOnly();




        #region Methods

        public void Buy(Order.Order order)
        {
            this._orders.Add(order);
            order.Product.DecreaseInventoryCount();
        }
        

        #endregion

        #region Validation

        public void ValidateUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new EmptyUserNameException();
        }
        

        #endregion

    }
}
