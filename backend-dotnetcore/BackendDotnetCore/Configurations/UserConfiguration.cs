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
            //builder.Navigation(b => b.Cart).UsePropertyAccessMode(PropertyAccessMode.Property);




            /*builder.HasOne(a => a.roleCreate)
             .WithOne(b => b.userCreate)
             .HasForeignKey<RoleEntity>(b => b.Created_by);

            builder.HasOne(a => a.roleUpdate)
           .WithOne(b => b.userUpdate)
           .HasForeignKey<RoleEntity>(b => b.Update_by);*/


        }
    }
}
