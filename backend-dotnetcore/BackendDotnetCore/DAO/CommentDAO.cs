﻿using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using Microsoft.EntityFrameworkCore;
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
            Console.WriteLine("BEFORE SAVE CMT = " + commentEntity.fullName);
            var cmt = new CommentEntity()
            {
                //active
                active = commentEntity.active,
                content = commentEntity.content,
                productID = commentEntity.productID,
                userID = commentEntity.userID,
                rate = commentEntity.rate,
                createdDate = commentEntity.createdDate,
                orderID = commentEntity.orderID,
                fullName = commentEntity.fullName
            };
            dbContext.Entry(commentEntity).Reference(x => x.user).IsModified = false;//moi them vao
            dbContext.Entry(commentEntity).Reference(x => x.Product).IsModified = false;//moi them vao(07092021)
            dbContext.Comments.Add(cmt);
            dbContext.SaveChanges();
            return cmt.id;
        }

        //phuong thuc lay toan bo cac comment theo productID voi comment da duoc active
        public ICollection<CommentEntity> getAllByProductID(int productID)
        {
            List<CommentEntity> listRs = new List<CommentEntity>();
            var ls = dbContext.Comments.Include(d => d.user)
                                    .ThenInclude(s => s.comments)
                .Where(x => x.active == 1 && x.Product.Id == productID).ToList<CommentEntity>();
            if (null != ls) listRs = ls;
            Console.WriteLine(ls.Count);
            return listRs;
        }

        public CommentEntity checkUserCommentProductById(int productID, int userID, int ido)
        {
            var cmt = dbContext.Comments.Where(x => x.productID == productID && x.userID == userID && x.orderID == ido).SingleOrDefault();
            return cmt;
        }


        /*
         * ADMIN ACTION WITH COMMENTS (Get all, Active/Disable a comment)
         */
        public int GetCountComments()
        {
            var ls = dbContext.Comments
                       .ToList();
            return ls.Count;
        }

        public List<CommentEntity> GetAllComments(int limit, int page, int active)
        {
            page = page <= 0 ? 1 : page;
            List<CommentEntity> listComments = new List<CommentEntity>();
            if (active == -1)
            {
                var ls = dbContext.Comments.Skip(limit * (page - 1)).Take(limit)
                        .ToList();

                listComments = ls;
            }
            else
            {
                var ls = dbContext.Comments.Where(x=>x.active == active).Skip(limit * (page - 1)).Take(limit)
                        .ToList();

                listComments = ls;
            }

            return listComments;
        }

        public bool ActiveAComment(int commentID)
        {
            int Active;
            var cmt = dbContext.Comments.Where(X => X.id == commentID).SingleOrDefault();
            Active = cmt.active;
            cmt.active = cmt.active == 1 ? 0 : 1;
            dbContext.Update(cmt);//update into db
            dbContext.SaveChanges();
            return cmt.active!=Active?true:false;
        }

        public CommentEntity GetCommentByID(int commentID)
        {
            var rs = dbContext.Comments.Where(x => x.id == commentID).SingleOrDefault();
            return rs;
        }

    }
}
