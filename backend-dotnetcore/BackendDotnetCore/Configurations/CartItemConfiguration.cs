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
            builder.ToTable("cart_item");
            builder.HasKey(e => e.id);

            builder.HasOne(x => x.product)
                .WithMany(p => p.cartItems)
                .HasForeignKey("product_id");

            builder.HasOne(x => x.user)
                .WithMany(u => u.CartItemEntities)
                .HasForeignKey("user_id");
        }
    }
}
