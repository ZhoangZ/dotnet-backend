using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendDotnetCore.Enitities;

namespace BackendDotnetCore.Configurations
{
    class InformationProductConfiguration : IEntityTypeConfiguration<Enitities.InformationProduct>
    {
        public void Configure(EntityTypeBuilder<InformationProduct> builder)
        {
            builder.ToTable("information_product")
                .HasKey(e => e.Id);
                builder.Property<int>("product_id");

            ;
            builder.HasOne(x => x.Product)
                .WithMany(x => x.Informations)
                .HasForeignKey("product_id");



        }
    }
}
