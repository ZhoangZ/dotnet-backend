using BackendDotnetCore.DAO;
using BackendDotnetCore.DTO;
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
        public IActionResult postComment([FromBody] List<CommentDTO> commentPosts)
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
                foreach (CommentDTO cmtPost in commentPosts)
                {
                    if (cmtPost.idp == 0 || null == product2DAO.getProduct(cmtPost.idp)) //cần thêm kiểm tra trên order của khách hàng
                    {
                        return BadRequest(new { message = "Sản phẩm không tồn tại trong hệ thống!" });
                    }

                    else
                    {
                        CommentEntity commentResponse = new CommentEntity();
                        commentResponse.userID = user.Id;
                        commentResponse.user = userDAO.getOneById(user.Id);
                        commentResponse.fullName = user.Fullname;
                        //kiem tra xem trong order của user có productID này không ?
                        if (!new OrderDAO().checkCommentOrder(cmtPost.idp, user.Id, cmtPost.ido)) return BadRequest(new { message = "Đơn hàng của bạn không tồn tại sản phẩm này hoặc trạng thái đơn hàng không hợp lệ để đánh giá.\n Vui lòng đặt hàng và trải nghiệm trước khi sử dụng tính năng này nhé!" });
                        //
                        if (null != commentDAO.checkUserCommentProductById(cmtPost.idp, user.Id, cmtPost.ido)) return BadRequest(new { message = "Bạn đã đánh giá sản phẩm này rồi. Cảm ơn bạn đã mua sản phẩm!" });
                        //save to table
                        commentResponse.createdDate = System.DateTime.Now;
                        commentResponse.rate = cmtPost.rate;
                        commentResponse.orderID = cmtPost.ido;//new
                        commentResponse.order = new OrderDAO().getOrderByIDAndUserID(cmtPost.ido, user.Id);//new
                        commentResponse.productID = cmtPost.idp;
                        commentResponse.Product = product2DAO.getProduct(cmtPost.idp);
                        commentResponse.active = 1;
                        commentResponse.content = cmtPost.content;
                        int commentID = commentDAO.Save(commentResponse);
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
                }
                //customize response (06092021)
                List<CustomOrderResponse> listRes = new List<CustomOrderResponse>();
                List<OrderEntity> list = new OrderDAO().GetOrdersByUserID(user.Id, 10, 0);

                listRes = new CustomOrderResponse().toListCustomOrderResponse(list);
                PageResponse<CustomOrderResponse> pageResponse = new PageResponse<CustomOrderResponse>();
                pageResponse.Data = listRes;
                pageResponse.Pagination = new Pagination(10, 0, new OrderDAO().GetCountOrdersByUserID(user.Id));
                return Ok(pageResponse);
                //return Ok(cmtRp);
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
