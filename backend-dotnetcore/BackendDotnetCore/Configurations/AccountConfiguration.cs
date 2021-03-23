using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BackendDotnetCore.Enitities;

namespace BackendDotnetCore.Configurations
{
    class AccountConfiguration : IEntityTypeConfiguration<Enitities.Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("accounts");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Username);
            builder.Property(e => e.Password);
            builder.Property(e => e.Email);
            builder.Property(e => e.Active);
            builder.Property(e => e.Delete);
            builder.Property(e => e.Level);
            builder.Property(e => e.Avatar);
        }
    }
}
