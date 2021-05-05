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
    public class UserConfiguration: IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("users");
            builder.HasKey(e => e.Id);
            builder.Navigation(b => b.UserRoles)
            .UsePropertyAccessMode(PropertyAccessMode.Property);
            

            /*  builder.Navigation(b => b.Roles)
               .UsePropertyAccessMode(PropertyAccessMode.Property);*/


            /*builder.HasMany(x => x.UserRoles)
             .WithOne(x => x.User)
             .HasForeignKey("role_id");*/

            /*builder.HasMany(p => p.Roles)
             .WithMany(p => p.Users)
             .UsingEntity<UserRole>(
                  j =>
                  {
                      j
                        .HasOne(pt => pt.User)
                        .WithMany(t => t.UserRoles)
                        .HasForeignKey("users_id");
                  },
                 j => {
                     j
                   .HasOne(pt => pt.Role)
                   .WithMany(p => p.UserRoles)
                   .HasForeignKey("role_id");
                 },
                 j =>
                 {

                     j.HasKey(t => t.i);
                 }

                 );*/

            /*builder.HasMany(p => p.Roles)
            .WithMany(p => p.Users)
            .UsingEntity<Dictionary<string, object>>("user_role",
                 j => j
                    .HasOne<UserEntity>()
                    .WithMany()
                    .HasForeignKey("users_id"),
                j => j
                    .HasOne<RoleEntity>()
                    .WithMany()
                    .HasForeignKey"role_id"));*/

         
         

        }
    }
}
