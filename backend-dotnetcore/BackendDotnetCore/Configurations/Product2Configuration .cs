using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendDotnetCore.Enitities;

namespace BackendDotnetCore.Configurations
{
    class Product2Configuration : IEntityTypeConfiguration<Enitities.Product2>
    {
        public void Configure(EntityTypeBuilder<Product2> builder)
        {
            builder.ToTable("product_2")
                .HasKey(e => e.Id)
                ;
               
                 
            
            builder.Navigation(b => b.Images)
        .UsePropertyAccessMode(PropertyAccessMode.Property);
            //forgein key brand


        }
    }
}
