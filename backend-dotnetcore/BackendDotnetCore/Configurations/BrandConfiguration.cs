using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendDotnetCore.Enitities;

namespace BackendDotnetCore.Configurations
{
    class BrandConfiguration : IEntityTypeConfiguration<Enitities.Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("brand")
                .HasKey(e => e.Id);

           






        }
    }
}
