using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendDotnetCore.Entities;

namespace BackendDotnetCore.Configurations
{
    class Product2SpecificConfiguration : IEntityTypeConfiguration<Product2Specific>
    {
        public void Configure(EntityTypeBuilder<Product2Specific> builder)
        {
            builder.ToTable("product_specific")
                .HasKey(e => e.Id);

           
            builder.Property<int>("product_id");

            ;
            builder.HasOne(x => x.Product)
                .WithMany(x => x.Specifics)
                .HasForeignKey("product_id");

            builder.Property<int>("ram_id");

            
            builder.HasOne(x => x.Ram)
                .WithMany(x => x.Product2Specifics)
                .HasForeignKey("ram_id");


            
            builder.Property<int>("rom_id");
            builder.HasOne(x => x.Rom)
                .WithMany(x => x.Product2Specifics)
                .HasForeignKey("rom_id");
            








        }
    }
}
