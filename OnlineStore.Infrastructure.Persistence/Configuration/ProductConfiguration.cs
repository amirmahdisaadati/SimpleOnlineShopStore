using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Domain.DomainModel.Models.Product;

namespace OnlineStore.Infrastructure.Persistence.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasIndex(p => p.Title).IsUnique();
            builder.Property(p => p.Title)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(p => p.RowVersion)
                .IsRowVersion();
        }
    }
}
