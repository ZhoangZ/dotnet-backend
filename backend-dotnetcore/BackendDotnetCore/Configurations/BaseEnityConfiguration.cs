using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendDotnetCore.Entities;

namespace BackendDotnetCore.Configurations
{
    class BaseEnityConfiguration : IEntityTypeConfiguration<Entities.BaseEnity>
    {
        public void Configure(EntityTypeBuilder<BaseEnity> builder)
        {
            builder.ToTable("BaseEnity")
                .HasKey(e => e.Id);
            /*builder.HasDiscriminator<string>("blog_type")
        .HasValue<Product>("blog_base")*/
        ;
           






        }
    }
}
