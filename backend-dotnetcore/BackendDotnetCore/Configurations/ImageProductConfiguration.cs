using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendDotnetCore.Entities;

namespace BackendDotnetCore.Configurations
{
    class ImageProductConfiguration : IEntityTypeConfiguration<Entities.ImageProduct>
    {
        public void Configure(EntityTypeBuilder<ImageProduct> builder)
        {
            builder.ToTable("image_product")
                .HasKey(e => e.Id);
           
            
            builder.HasOne(x => x.Product)
                .WithMany(x => x.Images)
                .HasForeignKey(X=>X.ProductId);



        }
    }
}
