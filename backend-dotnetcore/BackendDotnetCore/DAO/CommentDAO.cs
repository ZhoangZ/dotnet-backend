using BackendDotnetCore.EF;
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

        //phuong thuc lay toan bo cac comment theo productID voi comment da duoc active
        public List<CommentEntity> getAllByProductID(int productID)
        {
            List<CommentEntity> listRs = dbContext.Comments.Where(x => x.active == 1 && x.Product.Id == productID).ToList<CommentEntity>();
            Console.WriteLine(listRs.Count);
            return listRs;
        }
    }
}
