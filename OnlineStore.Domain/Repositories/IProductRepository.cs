using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopStore.Domain.DomainModel.Models.Product;

namespace OnlineShopStore.Domain.DomainModel.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<bool> IsUniqueNameAsync(string title);
    }
}
