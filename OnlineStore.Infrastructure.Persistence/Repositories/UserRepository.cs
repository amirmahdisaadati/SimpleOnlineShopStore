using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopStore.Domain.DomainModel.Models.User;
using OnlineShopStore.Domain.DomainModel.Repositories;
using OnlineShopStore.Infrastructure.Persistence.Context;

namespace OnlineShopStore.Infrastructure.Persistence.Repositories
{
    public  class UserRepository:Repository<User>,IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
