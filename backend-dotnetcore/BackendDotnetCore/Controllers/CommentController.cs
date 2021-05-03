using BackendDotnetCore.DAO;
using BackendDotnetCore.Enitities;
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
        public CommentEntity postComment([FromBody] CommentDTO commentPost)
        {
            Console.WriteLine(commentPost);
            if(commentPost.userID == 0) 
            { 
                Console.WriteLine("Vui lòng đăng nhập trước khi thực hiện chức năng comment.");
                return null;
            }
            else
             {
              
                CommentEntity commentResponse = new CommentEntity();
               // commentResponse.userID = commentPost.userID;
                commentResponse.user = userDAO.getOneById(commentPost.userID);
                if (commentPost.productID == 0)
                {
                    Console.WriteLine("Lỗi request không có productID");
                }
                else
                {
                    //save to table
                  
                    //commentResponse.productID = commentPost.productID;
                    commentResponse.product = product2DAO.getProduct(commentPost.productID);
                    commentResponse.active = 1;
                    commentResponse.content = commentPost.content;
                    int commentID = commentDAO.Save(commentResponse);
                    if(commentID == 0)
                    {
                        Console.WriteLine("Khong thanh cong!");
                    }
                    else
                    {
                        Console.WriteLine("Thanh cong! Them vao id = " + commentID);
                    }
                    
                }
                return commentResponse;
             }
        }
    }
}
