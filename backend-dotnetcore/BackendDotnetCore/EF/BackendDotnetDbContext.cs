﻿using Microsoft.EntityFrameworkCore;
using BackendDotnetCore.Entities;
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
        public DbSet<RevenueEntity> Revenues { get; set; }
        public DbSet<RamEntity> Rams { get; set; }
       
        public DbSet<Product2> Products { get; set; }
        public DbSet<InformationProduct> InformationProducts { get; set; }
        public DbSet<UserEntity> users { set; get; }
        public DbSet<RoleEntity> roles { set; get; }
        public DbSet<ImageProduct> Images { set; get; }
        public DbSet<UserRole> UserRoles { get; set; }
       
        public DbSet<CommentEntity> Comments { set; get; }
        public DbSet<CartItemEntity> CartItems { set; get; }
        public DbSet<OrderItemEntity> OrderItems { set; get; }
        public DbSet<CartEntity> Carts { set; get; }
        public DbSet<OrderEntity> Orders { set; get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfiguration(new Configurations.Product2Configuration());
            //modelBuilder.ApplyConfiguration(new Configurations.BaseEnityConfiguration());

            modelBuilder.ApplyConfiguration(new Configurations.UserConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ImageProductConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.InformationProductConfiguration());
           
            modelBuilder.ApplyConfiguration(new Configurations.RamConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.RomConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CommentConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CartConfiguration());

            modelBuilder.ConvertToSnakeCase();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=dotnet;user=root;password=");
            //SnakeCaseExtensions.ConvertToSnakeCase(optionsBuilder);
            



        }
    }
}
