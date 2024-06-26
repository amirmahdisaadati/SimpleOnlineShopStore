using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopStore.Domain.DomainModel.Models.User;

namespace OnlineShopStore.Domain.Test.Unit.Utils
{
    public class UserBuilder
    {
        private string _name="TestUser";


        public UserBuilder WithName(string name)
        {
            this._name=name;
            return this;
        }
        public User Build()
        {
            return new User(_name);
        }
    }
}
