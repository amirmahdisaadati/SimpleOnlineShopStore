using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Domain.DomainModel.Models.Product;

namespace OnlineStore.Domain.DomainModel.Repositories
{
    public  interface IProductRepository:IRepository<Product>
    {
         Task<bool> IsUniqueNameAsync(string title);
    }
}
