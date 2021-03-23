using Microsoft.EntityFrameworkCore;
using BackendDotnetCore.Enitities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackendDotnetCore.EF
{
    class BackendDotnetCoreDbContext:DbContext
    {
        public DbSet<Account> Accounts { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.AccountConfiguration());

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=dotnet;user=root;password=");
        }
    }
}
