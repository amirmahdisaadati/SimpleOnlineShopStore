using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopStore.Domain.DomainModel.Models.Order;
using OnlineShopStore.Domain.DomainModel.Repositories;
using OnlineShopStore.Infrastructure.Persistence.Context;

namespace OnlineShopStore.Infrastructure.Persistence.Repositories
{
    public  class OrderRepository:Repository<Order>,IOrderRepository
    {
        public OrderRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
