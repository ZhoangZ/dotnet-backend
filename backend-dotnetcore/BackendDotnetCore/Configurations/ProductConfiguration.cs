using BackendDotnetCore.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Enitities.Product>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("products");
            builder.HasKey(e => e.Active);
            builder.Property(e => e.Username);
            builder.Property(e => e.Password);
            builder.Property(e => e.Email);
            builder.Property(e => e.Active);
            builder.Property(e => e.Delete);
            builder.Property(e => e.Level);
            builder.Property(e => e.Avatar);
        }

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            throw new NotImplementedException();
        }
    }
}
