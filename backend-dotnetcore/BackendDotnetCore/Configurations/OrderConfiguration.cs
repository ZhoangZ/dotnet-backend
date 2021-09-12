using BackendDotnetCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
          
            builder.Navigation(b => b.Items).UsePropertyAccessMode(PropertyAccessMode.Property);
            //builder.HasOne(x => x.User).WithMany(X => X.orders).HasForeignKey(x=>x.UserId);//moithem 07092021
        }
    }
}
