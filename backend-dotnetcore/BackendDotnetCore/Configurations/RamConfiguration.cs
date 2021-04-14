using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendDotnetCore.Enitities;

namespace BackendDotnetCore.Configurations
{
    class RamConfiguration : IEntityTypeConfiguration<Enitities.RamEntity>
    {
        public void Configure(EntityTypeBuilder<RamEntity> builder)
        {
            builder.ToTable("ram")
                .HasKey(e => e.Id);

           
          







        }
    }
}
