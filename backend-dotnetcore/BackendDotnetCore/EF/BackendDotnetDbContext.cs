using Microsoft.EntityFrameworkCore;
using BackendDotnetCore.Enitities;
using System;
using System.Collections.Generic;
using System.Text;


namespace BackendDotnetCore.EF
{
    class BackendDotnetDbContext:DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<PaymentEntity> Payments { get; set; }
        public DbSet<RomEntity> Roms { get; set; }
        public DbSet<RamEntity> Rams { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Product2> Products { get; set; }
        public DbSet<BaseEnity> BaseEnities { get; set; }
        public DbSet<UserEntity> users { set; get; }
        public DbSet<RoleEntity> roles { set; get; }
        public DbSet<ImageProduct> image_product { set; get; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Product2Specific> product2Specifics { set; get; }
        public DbSet<CommentEntity> comments { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.AccountConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Product2Configuration());
            //modelBuilder.ApplyConfiguration(new Configurations.BaseEnityConfiguration());

            modelBuilder.ApplyConfiguration(new Configurations.UserConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ImageProductConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.InformationProductConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.Product2SpecificConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.RamConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.RomConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CommentConfiguration());

            modelBuilder.ConvertToSnakeCase();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=dotnet;user=root;password=");
            //SnakeCaseExtensions.ConvertToSnakeCase(optionsBuilder);
            



        }
    }
}
