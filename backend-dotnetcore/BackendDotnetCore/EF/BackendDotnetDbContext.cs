using Microsoft.EntityFrameworkCore;
using BackendDotnetCore.Enitities;
using System;
using System.Collections.Generic;
using System.Text;


namespace BackendDotnetCore.EF
{
    class BackendDotnetDbContext:DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product2> Products { get; set; }
        public DbSet<BaseEnity> BaseEnities { get; set; }
        public DbSet<UserEntity> users { set; get; }
        public DbSet<RoleEntity> roles { set; get; }
        public DbSet<ImageProduct> image_product { set; get; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.AccountConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Product2Configuration());
            //modelBuilder.ApplyConfiguration(new Configurations.BaseEnityConfiguration());

            modelBuilder.ApplyConfiguration(new Configurations.UserConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ImageProductConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.UserRoleConfiguration());
            modelBuilder.ConvertToSnakeCase();


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=dotnet;user=root;password=");
            //SnakeCaseExtensions.ConvertToSnakeCase(optionsBuilder);
            



        }
    }
}
