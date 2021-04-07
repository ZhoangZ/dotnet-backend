using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendDotnetCore.Enitities;

namespace BackendDotnetCore.Configurations
{
    class ProductConfiguration : IEntityTypeConfiguration<Enitities.Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
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
