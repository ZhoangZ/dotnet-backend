using BackendDotnetCore.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<Enitities.UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("user_role");
            builder.HasKey(bc => new { bc.userID, bc.roleID });
            builder.HasOne(bc => bc.user)
                 .WithMany(b => b.UserRoles)
                 .HasForeignKey(bc => bc.userID);
            builder.HasOne(bc => bc.role)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(bc => bc.roleID);
        }
    }
}
