using BackendDotnetCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemEntity>
    {
        public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
        {


            builder.HasOne(x => x.Order)
                .WithMany(x => x.Items)
                .HasForeignKey(X => X.OrderId);

            //builder.Navigation(b => b.Pr).UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}
