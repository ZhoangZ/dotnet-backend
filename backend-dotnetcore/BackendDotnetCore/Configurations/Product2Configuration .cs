﻿using System;
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
            builder.Navigation(b => b.Informations)
       .UsePropertyAccessMode(PropertyAccessMode.Property);
            builder.Navigation(b => b.Specifics)
      .UsePropertyAccessMode(PropertyAccessMode.Property);


            builder.Property<int>("brand_id");
            builder.HasOne(x => x.Brand)
                .WithMany(x => x.Product2s)
                .HasForeignKey("brand_id");
            //forgein key brand


            //forgein key one to one with comment
            builder.HasMany(c => c.comments)
                   .WithOne(e => e.product);


        }
    }
}
