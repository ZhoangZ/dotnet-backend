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
            builder.HasKey(e => e.id);
            builder.Property(e => e.content).IsRequired().HasColumnName("content");

            builder.HasOne(e => e.product).WithMany(b => b.comments);
            builder.HasOne(e => e.user).WithOne(b => b.comment).IsRequired();
          
                   



        }
    }
}
