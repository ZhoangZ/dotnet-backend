using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendDotnetCore.Enitities;

namespace BackendDotnetCore.Configurations
{
    class BaseEnityConfiguration : IEntityTypeConfiguration<Enitities.BaseEnity>
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
