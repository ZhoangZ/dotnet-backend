using BackendDotnetCore.EF;
using BackendDotnetCore.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.DAO
{
    public class CommentDAO
    {
        private BackendDotnetDbContext dbContext;
        public CommentDAO()
        {
            this.dbContext = new BackendDotnetDbContext();
        }

        //phuong thuc add comment
        public CommentEntity Save(CommentEntity commentEntity)
        {
            Console.WriteLine("Save comment="+commentEntity);
            dbContext.comments.AddAsync(commentEntity);
            dbContext.SaveChangesAsync();
            return commentEntity;
        }
    }
}
