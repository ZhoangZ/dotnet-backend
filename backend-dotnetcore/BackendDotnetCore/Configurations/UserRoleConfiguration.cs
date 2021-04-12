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
            builder.ToTable("user_role").HasKey(e => e.Id); ;
            builder.Property<int>("users_id");
            builder.Property<int>("role_id");
          
            builder.HasOne(bc => bc.User)
                  .WithMany(b => b.UserRoles)
                  .HasForeignKey("users_id");
            
             builder.HasOne(bc => bc.Role)
                 .WithMany(c => c.UserRoles)
                 .HasForeignKey("role_id");
          /*  builder.Navigation(b => b.Role)
           .UsePropertyAccessMode(PropertyAccessMode.Property);*/



        }
    }
}
