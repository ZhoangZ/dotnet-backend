using BackendDotnetCore.DAO;
using BackendDotnetCore.DTO;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Helpers;
using BackendDotnetCore.Services;
using BackendDotnetCore.Ultis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private RoleDAO roleDAO = new RoleDAO();
        private UserDAO userDAO = new UserDAO();
        private OrderDAO orderDAO = new OrderDAO();
        private CommentDAO commentDAO = new CommentDAO();
        private static string emailUserRequestActive = "";

        [HttpGet]
        public List<RoleEntity> GetAllElementRole()
        {
            List<RoleEntity> roles = roleDAO.getAllRole();
            return roles;
        }
        /*
         * ADMIN USER AND ROLE MANAGEMENT
         */
        //add new a role
        [HttpPost("role")]
        public IActionResult CreateNewRole(RoleEntity roleEntity)
        {
            if (roleEntity.checkRoleInfo() == true)
            {
                RoleEntity rs = roleDAO.Save(roleEntity);
                return Ok(rs);
            }
            else
            {
                return BadRequest(roleEntity);
            }
        }

        //admin get all users
        [HttpGet("users")]
        [Authorize]
        public List<UserEntity> GetAllUsers()
        {
            //lay tai khoan dang dang nhap tu token
            UserEntity userAction = (UserEntity)HttpContext.Items["User"];
            Console.WriteLine("userAction = "+userAction.ToString());
            if (userDAO.isAdmin(userAction.Id) == false) return new List<UserEntity>();

            List<UserEntity> users = userDAO.GetListUsers();
            return users;
        }

        //admin blocked and unblocked a account user
        [HttpPut("users/blocked/{userID}")]
        [Authorize]
        public IActionResult BlockedAndUnblockedAUser(int userID)
        {
            //lay tai khoan dang dang nhap tu token
            UserEntity userAction = (UserEntity)HttpContext.Items["User"];
            if (userDAO.isAdmin(userAction.Id) == false) return BadRequest(new { message = "Không có quyền truy xuất. Thử lại với tài khoản quản trị viên." });

            UserEntity userBlocked;
            if(null == (userBlocked = userDAO.getOneById(userID)))
            return BadRequest(new { message = "Không tồn tại tài khoản người dùng trong hệ thống!" });
            bool blocked = userBlocked.Active == 0 ? true : false;

            if (blocked == false && SendMailFeedBack(userBlocked))
            if (true == userDAO.BlockedAndUnblockedOneUser(userID, blocked)) return Ok(userDAO.GetListUsers());
            if (true == userDAO.BlockedAndUnblockedOneUser(userID, blocked)) return Ok(userDAO.GetListUsers());
            return BadRequest(new { message = "Hệ thống đang gặp sự cố. Vui lòng thử lại sau!" });
        }

        //Admin send email when check active user
        private bool SendMailFeedBack(UserEntity userRequest)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<p>Tài khoản của bạn đã được mở khóa thành công. Hãy truy cập website và sử dụng các tính năng có trong hệ thống.</p> <p>Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi!</p>");
            sb.Append("<b><a href=\"http://localhost:3000\">NHẤN VÀO ĐÂY ĐỂ TRUY CẬP WEBSITE!</a></b>");
            if (SendMailService.SendEmail("DHDTMobile: Mở khóa tài khoản người dùng", userRequest.Email, sb.ToString()))
            {
                return true;
            }
            return false;
        }


        /*
         * ADMIN ORDERS MANAGEMENT
         */
        [HttpGet("orders")]
        [Authorize]
        public IActionResult GetAllOrders()
        {
            UserEntity userAction = (UserEntity)HttpContext.Items["User"];
            if (!userAction.IsAdmin) return BadRequest(new { message = "Hạn chế bởi quyền truy cập. Thử lại với tài khoản quản trị viên!" });
            var listOrders = orderDAO.GetAllOrders();
            
            return Ok(new CustomOrderResponse().toListCustomOrderResponse(listOrders));
        }

        //get orders by status
        [HttpGet("orders/status/{status}")]
        [Authorize]
        public IActionResult GetAllOrdersByStatus(int status)
        {
            UserEntity userAction = (UserEntity)HttpContext.Items["User"];
            if (!userAction.IsAdmin) return BadRequest(new { message = "Hạn chế bởi quyền truy cập. Thử lại với tài khoản quản trị viên!" });
            var listOrders = new List<OrderEntity>();
            if (status == 0)
                listOrders = orderDAO.GetAllOrders();
            else listOrders = orderDAO.GetAllOrdersByStatus(status);

            return Ok(new CustomOrderResponse().toListCustomOrderResponse(listOrders));
        }
        
        //accept a order by id
        [HttpPut("orders/active/{orderID}")]
        [Authorize]
        public IActionResult ActiveOrderPending(int orderID)
        {
            UserEntity userAction = (UserEntity)HttpContext.Items["User"];
            if (!userAction.IsAdmin) return BadRequest(new { message = "Hạn chế bởi quyền truy cập. Thử lại với tài khoản quản trị viên!" });
            if (orderDAO.AcceptOrderPending(orderID))
            {
                CustomOrderResponse cs = new CustomOrderResponse();
                OrderEntity oe = orderDAO.GetOrderByID(orderID);
                return Ok(cs.toOrderResponse(oe));
            }
            else
            {
                return BadRequest("Đơn hàng không được phép hủy!");
            }
        }

        //tim kiem don hang bang ma don hang
        [HttpGet("orders/{orderID}")]
        [Authorize]
        public IActionResult FindOrderByID(int orderID)
        {
            UserEntity userAction = (UserEntity)HttpContext.Items["User"];
            if (!userAction.IsAdmin) return BadRequest(new { message = "Hạn chế bởi quyền truy cập. Thử lại với tài khoản quản trị viên!" });

            CustomOrderResponse cs = new CustomOrderResponse();
            OrderEntity oe = orderDAO.GetOrderByID(orderID);
            if (null == oe) return BadRequest(new { message = "Không tồn tại đơn hàng có mã {0} trong hệ thống!", orderID});
            return Ok(cs.toOrderResponse(oe));
        }



        /*
         * ADMIN COMMENTS MANAGEMENT
         */
        [HttpGet("comments")]
        [Authorize]
        public IActionResult GetAllComments()
        {
            UserEntity ue =(UserEntity) HttpContext.Items["User"];
            if (!ue.IsAdmin) return BadRequest(new { message = "Giới hạn bởi quyền truy cập. Hãy thử với tài khoản admin!" });
            var listComments = commentDAO.GetAllComments();

            return Ok(listComments);
        }

        [HttpPost("comments/active")]
        [Authorize]
        public IActionResult ActiveDisableAComment(int commentID, bool isActive)
        {
            Console.WriteLine("Disable or active a comment {0}, {1} ", commentID, isActive);
            UserEntity ue = (UserEntity)HttpContext.Items["User"];
            if (!ue.IsAdmin) return BadRequest(new { message = "Giới hạn bởi quyền truy cập. Hãy thử với tài khoản admin!" });
            var commentActive = commentDAO.ActiveAComment(commentID, isActive);

            return Ok("Response for request is "+commentActive);
        }
    }
}
