using BackendDotnetCore.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
    {
        public void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            builder.ToTable("comment");
            builder.HasKey(e => e.id).HasAnnotation("DatabaseGenerated", "DatabaseGeneratedOption.Identity");
            //builder.Property(e => e.userID).IsRequired().HasColumnName("user_id");
            //builder.Property(e => e.productID).IsRequired().HasColumnName("product_id");
            builder.Property(e => e.content).IsRequired().HasColumnName("content");
            builder.HasOne(a => a.product)
                   .WithOne(b => b.comment)
                   .HasForeignKey<Product2>(b => b.Id).IsRequired();
            builder.HasOne(a => a.user)
                 .WithOne(b => b.comment)
                 .HasForeignKey<UserEntity>(b => b.Id).IsRequired();


        }
    }
}
