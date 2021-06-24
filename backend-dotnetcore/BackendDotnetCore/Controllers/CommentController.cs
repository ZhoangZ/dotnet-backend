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
        [Authorize]
        public IActionResult postComment([FromBody] CommentDTO commentPost)
        {
            CommentResponse cmtRp = new CommentResponse();
            ICollection<CommentEntity> listCommentOfProduct;
            //lay user tu token
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            if (null == user)
            {
               return BadRequest(new { message = "Vui lòng đăng nhập trước khi thực hiện chức năng này." });
            }
            else
            {
                CommentEntity commentResponse = new CommentEntity();
                commentResponse.userID = user.Id;
                commentResponse.user = userDAO.getOneById(user.Id);
                if (commentPost.productID == 0 || null == product2DAO.getProduct(commentPost.productID)) //cần thêm kiểm tra trên order của khách hàng
                {
                    return BadRequest(new { message = "Sản phẩm không tồn tại trong hệ thống!" });
                }
                else
                {
                    if (null != commentDAO.checkUserCommentProductById(commentPost.productID, user.Id)) return BadRequest(new { message = "Bạn đã đánh giá sản phẩm này rồi. Cảm ơn bạn đã mua sản phẩm!" });
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
                    //if (!new OrderDAO().checkCommentOrder(commentPost.productID, user.Id)) return BadRequest(new { message = "Sản phẩm không có trong đơn hàng nào của bạn.\n Vui lòng đặt hàng trước khi trải nghiệm tính năng này nhé!" });
                    //
                    if (commentID == 0)
                    {
                        return BadRequest(new { message = "Hệ thống đang gặp sự cố!" });
                    }
                    else
                    {
                        commentResponse.id = commentID;
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
