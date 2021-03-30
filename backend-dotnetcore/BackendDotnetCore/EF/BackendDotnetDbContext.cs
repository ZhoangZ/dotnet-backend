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
        public DbSet<Product> Products { get; set; }

        public DbSet<BaseEnity> BaseEnities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.AccountConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ProductConfiguration());
            //modelBuilder.ApplyConfiguration(new Configurations.BaseEnityConfiguration());

            modelBuilder.ConvertToSnakeCase();


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=dotnet;user=root;password=");
            //SnakeCaseExtensions.ConvertToSnakeCase(optionsBuilder);
            



        }
    }
}
