using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendDotnetCore.Entities;

namespace BackendDotnetCore.Configurations
{
    class RomConfiguration : IEntityTypeConfiguration<RomEntity>
    {
        public void Configure(EntityTypeBuilder<RomEntity> builder)
        {
            builder.ToTable("rom")
                .HasKey(e => e.Id);

           






        }
    }
}
