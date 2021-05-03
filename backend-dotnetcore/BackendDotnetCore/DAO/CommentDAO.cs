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
        public int Save(CommentEntity commentEntity)
        {
            int rs = 0;
            var newComment = new CommentEntity()
            {
                active = 1,
                content = commentEntity.content,
                product = commentEntity.product,
                user = commentEntity.user,

            };
            dbContext.Comments.Update(commentEntity);
            rs = dbContext.SaveChanges();
            return rs;
        }
    }
}
