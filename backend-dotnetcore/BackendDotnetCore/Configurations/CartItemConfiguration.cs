using BackendDotnetCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItemEntity>
    {
        public void Configure(EntityTypeBuilder<CartItemEntity> builder)
        {


            builder.HasOne(x => x.Cart)
                .WithMany(x => x.Items)
                .HasForeignKey(X => X.CartId);

            //builder.Navigation(b => b.Pr).UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}
