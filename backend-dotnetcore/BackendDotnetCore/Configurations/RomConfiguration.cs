using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendDotnetCore.Enitities;

namespace BackendDotnetCore.Configurations
{
    class RomConfiguration : IEntityTypeConfiguration<Enitities.RomEntity>
    {
        public void Configure(EntityTypeBuilder<RomEntity> builder)
        {
            builder.ToTable("rom")
                .HasKey(e => e.Id);

           






        }
    }
}
