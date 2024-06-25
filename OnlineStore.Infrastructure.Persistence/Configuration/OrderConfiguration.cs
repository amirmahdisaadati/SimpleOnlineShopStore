using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopStore.Domain.DomainModel.Models.Order;

namespace OnlineShopStore.Infrastructure.Persistence.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.CreationDate)
                .IsRequired();

            builder.HasOne(o => o.Buyer)
                .WithMany(u => (IEnumerable<Order>)u.Orders)
                .HasForeignKey(o => o.BuyerId);

            builder.HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId);
        }
    }
}
