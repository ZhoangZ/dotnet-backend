using BackendDotnetCore.DAO;
using BackendDotnetCore.DTO;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Forms;
using BackendDotnetCore.Helpers;
using BackendDotnetCore.Models;
using BackendDotnetCore.Services;
using BackendDotnetCore.Ultis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            if (userAction.IsAdmin == false) return BadRequest(new { message = "Không có quyền truy xuất. Thử lại với tài khoản quản trị viên." });

            UserEntity userBlocked;
            if (null == (userBlocked = userDAO.getOneById(userID)))
                return BadRequest(new { message = "Không tồn tại tài khoản người dùng trong hệ thống!" });
            bool blocked = userBlocked.Active == 0 ? true : false;

            if (blocked == false)
                if (SendMailFeedBack(userBlocked) == false) return BadRequest(new { message = "Hệ thống không thể gửi mail đến người dùng!" });
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
        
        //admin create new account
        [HttpPost("users/new")]
        [Authorize]
        public IActionResult createdNewAccount([FromBody] RegisterForm registerForm)
        {
            //lay tai khoan dang dang nhap tu token
            UserEntity userAction = (UserEntity)HttpContext.Items["User"];
            if (!userAction.IsAdmin) return BadRequest(new { message = "Hạn chế bởi quyền truy cập. Vui lòng thử lại với tài khoản quản trị viên!"});
            HttpContext.Items["User"] = null;//xoa du lieu bo nho
            if (null != userDAO.getOneByEmail(registerForm.email)) return BadRequest(new { message = "Email đã tồn tại trong hệ thống. Thử lại với email khác!" });
            if (registerForm.checkInfo().Equals("success"))
            {
                UserEntity userEntity = registerForm.parseEntity();
                userEntity.Active = 1;
                RoleEntity role = userDAO.GetRoleByID(registerForm.role);
                UserRole us = new UserRole();
                us.Role = role;
                us.User = userEntity;
                userEntity.UserRoles.Add(us);
                userEntity.Password = EncodeUltis.MD5(userEntity.Password);

                UserEntity uResp = userDAO.Save(userEntity);
                if (uResp.Id == 0) return BadRequest(new { message = "Không thể lưu dữ liệu. Hệ thống đang gặp sự cố. Quay lại sau!" });
                //lưu thành công
                return Ok(userDAO.GetListUsers());
            }
            else
            {
                return BadRequest(new { message = registerForm.checkInfo() });
            }
        }
        
        //admin modify role of an account
        [HttpPut("users/role/{id}/{role}")]
        [Authorize]
        public IActionResult updateRoleUserByID(int id, int role)
        {
            //access user acction
            UserEntity userAction = (UserEntity) HttpContext.Items["User"];
            if (!userAction.IsAdmin) return BadRequest("Hạn chế bởi quyền truy cập. Thử lại với tài khoản quản trị viên!");
            HttpContext.Items["User"] = null;//xoa bo nho dem

            UserEntity userEdited;
            if (null == (userEdited = userDAO.getOneById(id))) return BadRequest("Thông tin mã người dùng không hợp lệ!");
            //todo 
            bool updated = userEdited.updateRole(userDAO.GetRoleByID(role));
            //update into db
            userDAO.Save(userEdited);
            return updated==true?Ok(userDAO.GetListUsers()):BadRequest("Hệ thống không thể cập nhật quyền truy cập người dùng. Thử lại sau!");
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


        //admin get orders with filter and pagination (get one order by id)
        [HttpGet("orders-version2")]
        [Authorize]
        public IActionResult GetAllOrdersPage(int _status = 0, int _limit = 10, int _page = 1, int _orderID = 0)
        {
            _page = (_page <= 0) ? 1 : _page;
            //get and check user access modifier
            UserEntity userAction = (UserEntity)HttpContext.Items["User"];
            if (!userAction.IsAdmin) return BadRequest(new { message = "Hạn chế bởi quyền truy cập. Thử lại với tài khoản quản trị viên!" });
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            PageResponse<CustomOrderResponse> pageResponse = new PageResponse<CustomOrderResponse>();
            if (_status < 0 && _status > 4) return BadRequest(new { message = "Trạng thái đơn hàng không hợp lệ!" });
            if (_orderID == 0)
            {
                var listOrders = orderDAO.GetListOrdersPage(_limit, _page, _status);
                List<CustomOrderResponse> ls = new CustomOrderResponse().toListCustomOrderResponse(listOrders);
                pageResponse.Data = ls;
                pageResponse.Pagination = new Pagination(_limit, _page, orderDAO.GetCountOrdersByStatus(_status));
                return Ok(pageResponse);
            }
            else
            {
                OrderEntity oe;
                if (null == (oe = orderDAO.GetOrderByID(_orderID))) return BadRequest(new { message = "Không tồn tại đơn hàng có mã " + _orderID + " trong hệ thống!" });
                return Ok(new CustomOrderResponse().toOrderResponse(oe));
            }
        }

        //accept a order by id
        [HttpPut("orders/active/{orderID}")]
        [Authorize]
        public IActionResult ActiveOrderPending(int orderID)
        {
            UserEntity userAction = (UserEntity)HttpContext.Items["User"];
            if (!userAction.IsAdmin) return BadRequest(new { message = "Hạn chế bởi quyền truy cập. Thử lại với tài khoản quản trị viên!" });
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
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
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
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
        public IActionResult GetAllCommentsPagination(int _limit = 10, int _commentID = 0, int _active = -1, int _page = 1)
        {
            UserEntity ue =(UserEntity) HttpContext.Items["User"];
            if (!ue.IsAdmin) return BadRequest(new { message = "Giới hạn bởi quyền truy cập. Hãy thử với tài khoản admin!" });
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            PageResponse<CustomCommentResponse> pageResponse = new PageResponse<CustomCommentResponse>();
            if (_commentID == 0)
            {
                var listComments = commentDAO.GetAllComments(_limit, _page, _active);
                List<CustomCommentResponse> listResponse = new CustomCommentResponse().toListCustomCommentResponses(listComments);
                pageResponse.Data = listResponse;
                pageResponse.Pagination = new Pagination(_limit, _page, new CommentDAO().GetCountComments());
                return Ok(pageResponse);
            }
            else
            {
                CommentEntity cmt = commentDAO.GetCommentByID(_commentID);
                if (null == cmt) return BadRequest(new { message = "Không tồn tại comment có id " + _commentID + " trong hệ thống!" });
                return Ok(new CustomCommentResponse().toCustomCommentResponse(cmt));
            }
            
        }

        [HttpPut("comments/{_commentID}")]
        [Authorize]
        public IActionResult ActiveDisableAComment(int _commentID = 0, int _page = 1, int _limit = 10)
        {
            UserEntity ue = (UserEntity)HttpContext.Items["User"];
            if (!ue.IsAdmin) return BadRequest(new { message = "Giới hạn bởi quyền truy cập. Hãy thử với tài khoản admin!" });
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (null == commentDAO.GetCommentByID(_commentID)) return BadRequest(new { message = "Không có comment nào có mã "+_commentID+" trong hệ thống!" });
            if (commentDAO.ActiveAComment(_commentID) == false) return BadRequest(new { message = "Hệ thống đang gặp sự cố, không thể sao lưu dữ liệu!" });

            var listComments = commentDAO.GetAllComments(_limit, _page, -1);
            List<CustomCommentResponse> listResponse = new CustomCommentResponse().toListCustomCommentResponses(listComments);
            PageResponse<CustomCommentResponse> pageResponse = new PageResponse<CustomCommentResponse>();
            pageResponse.Data = listResponse;
            pageResponse.Pagination = new Pagination(_limit, _page, new CommentDAO().GetCountComments());
            return Ok(pageResponse);
        }

        /*
         * ADMIN THỐNG KÊ API CHART (DỮ LIỆU HIỆN TẠI ĐANG LÀ DỮ LIỆU GIẢ)
         * Doanh thu theo tháng (tháng, doanh thu) + Thống kê thể loại bán được (tên hãng, số tiền, số lượng)
         */
        [HttpGet("thong-ke")]
        [Authorize]
        public IActionResult getChartData()
        {
            UserEntity userAction = (UserEntity) HttpContext.Items["User"];
            if (!userAction.IsAdmin) return BadRequest(new { message = "Giới hạn bởi quyền truy cập. Hãy thử với tài khoản quản trị viên!" });
            HttpContext.Items["User"] = null;//xoa bo nho dem
            BieuDo bieuDo = new BieuDo();//du lieu fake
            return Ok(bieuDo);
        }

    }
}
