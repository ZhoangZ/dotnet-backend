using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Models;
using BackendDotnetCore.Services;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Configurations
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private IUserService userService;
        private UserDAO userDAO;
        private Product2DAO product2DAO;
        private CommentDAO commentDAO;

        public CommentController(IUserService userService)
        {
            this.userService = userService;
            this.userDAO = new UserDAO();
            this.product2DAO = new Product2DAO();
            this.commentDAO = new CommentDAO();
        }

        [HttpPost("new")]
        public IActionResult postComment([FromBody] CommentDTO commentPost)
        {
            CommentResponse cmtRp = new CommentResponse();
            ICollection<CommentEntity> listCommentOfProduct;
            if (commentPost.userID == 0)
            {
                Console.WriteLine("Vui lòng đăng nhập trước khi thực hiện chức năng này.");
                return null;
            }
            else
            {
                CommentEntity commentResponse = new CommentEntity();
                commentResponse.userID = commentPost.userID;
                commentResponse.user = userDAO.getOneById(commentPost.userID);
                if (commentPost.productID == 0) //cần thêm kiểm tra trên order của khách hàng
                {
                    return BadRequest(new { message = "Lỗi request không có productID." });
                }
                else
                {
                    //save to table
                    commentResponse.createdDate = System.DateTime.Now;
                    commentResponse.rate = commentPost.rate;
                    Console.WriteLine("rating post = " + commentPost.rate+", db="+commentResponse.rate);

                    commentResponse.productID = commentPost.productID;
                    commentResponse.Product = product2DAO.getProduct(commentPost.productID);
                    commentResponse.user = userDAO.getOneById(commentPost.userID);
                    commentResponse.active = 1;
                    commentResponse.content = commentPost.content;
                    int commentID = commentDAO.Save(commentResponse);
                    //kiem tra xem trong order của user có productID này không ?
                    //TODO
                    //
                    if (commentID == 0)
                    {
                        return BadRequest(new { message = "Hệ thống đang gặp sự cố!" });
                    }
                    else
                    {
                        commentResponse.id = commentID;
                        Console.WriteLine("Thanh cong! Them vao id = " + commentID);
                        listCommentOfProduct = (ICollection<CommentEntity>)commentDAO.getAllByProductID(commentResponse.productID);
                        cmtRp.listCommentByProduct = listCommentOfProduct;
                        cmtRp.computeSumOfList();
                        cmtRp.computeTbcRate();
                    }

                }
                return Ok(cmtRp);
            }
        }

        [HttpGet("all")]
        public IActionResult getAllCommentByProductID(int productID)
        {
            CommentResponse cmtRp = new CommentResponse();
            if (null == product2DAO.getProduct(productID)) return BadRequest(new { message = "Sản phẩm không tồn tại trong hệ thống!" });
            ICollection<CommentEntity> listResult = commentDAO.getAllByProductID(productID);
            if(listResult.Count == 0)
            {
                cmtRp.tbcRate = 0.0;
                cmtRp.tongCmt = 0;
                cmtRp.listCommentByProduct = new List<CommentEntity>();
                return Ok(cmtRp);
            }
            cmtRp.listCommentByProduct = listResult;
            cmtRp.computeSumOfList();
            cmtRp.computeTbcRate();
            return Ok(cmtRp);
        }
    }
}
