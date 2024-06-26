using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopStore.Domain.DomainModel.Models.User;

namespace OnlineShopStore.Infrastructure.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name)
                .IsRequired();
            var alice = new User("Alice");
            alice.SetIdForSeeding(2);
            var bob = new User("Bob");
            bob.SetIdForSeeding(3);
            builder.HasData(alice, bob);
        }
    }
}
