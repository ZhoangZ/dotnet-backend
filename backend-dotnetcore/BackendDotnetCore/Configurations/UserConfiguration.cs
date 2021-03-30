using BackendDotnetCore.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<Enitities.UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("user");
            builder.HasKey(e => e.id);
            builder.Property(e => e.username);
            builder.Property(e => e.email);
            builder.Property(e => e.provider);
            builder.Property(e => e.confirmed);
            builder.Property(e => e.blocked);
            builder.Property(e => e.active);
        }
    }
}
