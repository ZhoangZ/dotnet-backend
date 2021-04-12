using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendDotnetCore.Enitities;

namespace BackendDotnetCore.Configurations
{
    class ImageProductConfiguration : IEntityTypeConfiguration<Enitities.ImageProduct>
    {
        public void Configure(EntityTypeBuilder<ImageProduct> builder)
        {
            builder.ToTable("image_product")
                .HasKey(e => e.Id);
            builder.Property<int>("product_id");
            
            builder.HasOne(x => x.Product)
                .WithMany(x => x.Images)
                .HasForeignKey("product_id");



        }
    }
}
