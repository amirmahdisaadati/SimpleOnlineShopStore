using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Domain.DomainModel.Models.Order;
using OnlineStore.Domain.DomainModel.Models.Product;
using OnlineStore.Domain.DomainModel.Models.User;
using System.Reflection;
using OnlineStore.Infrastructure.Persistence.Configuration;

namespace OnlineStore.Infrastructure.Persistence.Context
{
    public  class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order>Orders { get; set; }
        public DbSet<Product>Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ProductConfiguration)));
        }
    }
}
