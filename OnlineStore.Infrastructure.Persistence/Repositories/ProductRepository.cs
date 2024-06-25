using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.DomainModel.Models.Product;
using OnlineStore.Domain.DomainModel.Repositories;
using OnlineStore.Infrastructure.Persistence.Context;

namespace OnlineStore.Infrastructure.Persistence.Repositories
{
    public  class ProductRepository:Repository<Product>,IProductRepository
    {
        public ProductRepository(DatabaseContext context):base(context)
        {
            
        }

        public async Task<bool> IsUniqueNameAsync(string title)
        {
            return !await Context.Products.AnyAsync(p => p.Title == title);
        }
    }
}
