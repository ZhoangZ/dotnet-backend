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
            builder.ToTable("role")
                .HasKey(e => e.Id).HasAnnotation("DatabaseGenerated", DatabaseGeneratedOption.Identity);
            //builder.Property(typeof(string), "name");
            //builder.Property(typeof(string), "description");
            //builder.Property(typeof(string), "type");
            //builder.Property(typeof(string), "created_by");
            //builder.Property(typeof(string), "updated_by");
            //builder.Property(typeof(string), "active");

            /*builder.HasMany(x => x.UserRoles)
              .WithOne(x => x.Role)
              .HasForeignKey("role_id");*/

            /*builder.Navigation(b => b.UserRoles)
            .UsePropertyAccessMode(PropertyAccessMode.Property);*/

           

        }
    }
}
