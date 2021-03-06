using BackendDotnetCore.Entities;
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
            builder.HasKey(e => e.id);
            builder.Property(e => e.content).IsRequired().HasColumnName("content");

            builder.HasOne(x => x.user).WithMany(u => u.comments).HasForeignKey(x => x.userID);//moi them include voi user
            builder.Property(x => x.productID).HasColumnName("product_id").IsRequired();
            builder.HasOne(x => x.Product).WithMany(p => p.comments).IsRequired();



        }
    }
}
