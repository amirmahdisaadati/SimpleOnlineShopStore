using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopStore.Domain.DomainModel.Models.User;

namespace OnlineShopStore.Test.Utils.Utils
{
    public class UserBuilder
    {
        private string _name = "TestUser";


        public UserBuilder WithName(string name)
        {
            _name = name;
            return this;
        }
        public User Build()
        {
            return new User(_name);
        }
    }
}
