using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.DTO
{
    public class CustomCommentResponse
    {
        public int id { set; get; }
        public string content { set; get; }
        public int active { set; get; }
        public UserComment userComment { set; get; }
        public DateTime createdDate { set; get; }
        public double rate { set; get; }
        public ProductComment productComment { set; get; }

        public CustomCommentResponse()
        {

        }
        public CustomCommentResponse(int id, string content, int active, UserComment userComment, DateTime createdDate, double rate, ProductComment productComment)
        {
            this.id = id;
            this.content = content;
            this.active = active;
            this.userComment = userComment;
            this.createdDate = createdDate;
            this.rate = rate;
            this.productComment = productComment;
        }

        public CustomCommentResponse toCustomCommentResponse(CommentEntity commentEntity)
        {
            id = commentEntity.id;
            content = commentEntity.content;
            active = commentEntity.active;
            userComment = new UserComment().toUserComment(new UserDAO().getOneById(commentEntity.userID));//new UserDAO().getOneById(commentEntity.userID)
            createdDate = commentEntity.createdDate;
            rate = commentEntity.rate;
            productComment = new ProductComment().toProductComment(new Product2DAO().getProduct(commentEntity.productID));//new Product2DAO().getProduct(commentEntity.productID)
            return new CustomCommentResponse(id, content, active, userComment, createdDate, rate, productComment);
        }

        public List<CustomCommentResponse> toListCustomCommentResponses(List<CommentEntity> comments)
        {
            List<CustomCommentResponse> ls = new List<CustomCommentResponse>();
            foreach(CommentEntity ce in comments)
            {
                ls.Add(toCustomCommentResponse(ce));
            }
            return ls;
        }




    }
    public class ProductComment {
        public int id { set; get; }
        public string productImg { set; get; }

        public ProductComment()
        {

        }
        public ProductComment(int id, string productImg)
        {
            this.id = id;
            this.productImg = productImg;
        }

        public ProductComment toProductComment(Product2 product)
        {
            id = product.Id;
            productImg = product.Images.ToArray()[0].Image;

            return new ProductComment(id, productImg);
        }

    }
    public class UserComment
    {
        public int id { set; get; }
        public string fullName { set; get; }
        public string avatar { set; get; }


        public UserComment()
        {

        }
        public UserComment(int id, string fullName, string avatar)
        {
            this.id = id;
            this.fullName = fullName;
            this.avatar = avatar;
        }

        public UserComment toUserComment(UserEntity userEntity)
        {
            id = userEntity.Id;
            fullName = userEntity.Fullname;
            avatar = userEntity.Avatar;

            return new UserComment(id, fullName, avatar);
        }
        
    }
}
