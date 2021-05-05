using BackendDotnetCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Configurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
           /* builder.ToTable("role")
                .HasKey(e => e.Id).HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);

          */

            /*builder.HasMany(x => x.UserRoles)
              .WithOne(x => x.Role)
              .HasForeignKey("role_id");

            builder.Navigation(b => b.UserRoles)
            .UsePropertyAccessMode(PropertyAccessMode.Property);
            */



        }
    }
}
