using BackendDotnetCore.EF;
using BackendDotnetCore.Enitities;
using BackendDotnetCore.Entities;
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
        public int Save(CommentEntity commentEntity)
        {
            var cmt = new CommentEntity()
            {
                active = 1,
                content = commentEntity.content,
                productID = commentEntity.productID,
                userID = commentEntity.userID
            };
            dbContext.Comments.Add(cmt);
            dbContext.SaveChanges();
            return cmt.id;
        }
    }
}
