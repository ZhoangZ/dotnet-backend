﻿using BackendDotnetCore.Entities;
using BackendDotnetCore.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Forms;
using Microsoft.AspNetCore.Mvc;
using System;
using BackendDotnetCore.DAO;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Text;
using BackendDotnetCore.Ultis;
using System.Collections.Generic;
using System.Collections;
using BackendDotnetCore.DTO;
using BackendDotnetCore.Helpers;
using BackendDotnetCore.Models;
using BackendDotnetCore.FileProcess;
using System.IO;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private OrderDAO orderDAO;

        public UsersController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            orderDAO = new OrderDAO();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            Console.WriteLine(user);
            HttpContext.Items["User"] = null;
            return Ok(user);
        }

        /*
         * USER MANAGEMENT YOURSELF ACCOUNT
         */
        [HttpPost("forgot-pass")]
        public IActionResult ForgotPassword([FromBody] ResetPassForm fr)
        {
            if (_userService.checkEmail(fr.email) == false) return BadRequest( new { message = "Email không tồn tại trong hệ thống. Vui lòng thực hiện lại!" });
            string rdOtp = new Random().Next(10000000).ToString();

            StringBuilder sb = new StringBuilder();
            sb.Append("Yêu cầu xác thực mật khẩu nhập, bạn vui lòng nhập mã sau trong quá trình thiết lập mật khẩu mới: \n");
            sb.Append("Mã OPT: " + rdOtp + "\n");
            sb.Append("Lưu ý: Đây là mã bảo mật, vì vậy không được tiết lộ cho bất kỳ cá nhân hay tổ chức nào, điều này có thể khiến bạn mất tài khoản. Mã này sẽ tự động hủy sau 15 phút!");
            UserEntity userAction = _userService.getUserByEmail(fr.email);
            //saved otp code into table
            userAction.optCode = rdOtp;
            _userService.save(userAction);
            if (SendMailService.SendEmail("DHDT: Yêu cầu cấp mật khẩu mới ?", fr.email, sb.ToString())) return Ok("Hệ thống đã nhận được yêu cầu. Hãy kiểm tra email của bạn!");
            return BadRequest(new { message = "Hệ thống đang gặp sự cố. Vui lòng thực hiện sau ít phút!" });
        }

        //remove this function
        public IActionResult SendEmail(string subject, string toEmail, string body, string otp)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                string SystemMail = "ongdinh1099@gmail.com";
                message.From = new MailAddress("ongdinh1099@gmail.com");
                message.To.Add(new MailAddress(toEmail));
                message.Subject = "DHDTMobile: Yêu cầu cấp mật khẩu ?";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(SystemMail, "ongdinh101199");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                //save opt on database
                UserEntity ueRequest = _userService.getUserByEmail(toEmail);
                ueRequest.optCode = otp;
                _userService.save(ueRequest);

                return Ok("Yêu cầu đã được tiếp nhận. Hãy kiểm tra email của bạn!");
            }
            catch (Exception) { return BadRequest(new { message = "Hệ thống đang gặp sự cố! Vui lòng quay lại sau ít phút" }); }
        }

        [HttpPost("reset-pass")]
        public IActionResult ResetPass([FromBody] ResetPassForm form)
        {
            //check email and update password for user account
            if (_userService.checkEmail(form.email) == false) return BadRequest(new { message = "Email không hợp lệ! Vui lòng kiểm tra lại." });
            UserEntity ueResetPass = _userService.getUserByEmail(form.email);
            if (EncodeUltis.MD5(form.newpass).Equals(ueResetPass.Password)) return BadRequest(new { message = "Bạn đã sử dụng mật khẩu này gần đây. Hãy thử lại với mật khẩu mới!" });
            if (!form.newpass.Equals(form.repass)) return BadRequest(new { message = "Mật khẩu không trùng khớp!" });
            string optCodeDB = ueResetPass.optCode;
            if (!optCodeDB.Equals(form.opt)) return BadRequest(new { message = "Mã opt không hợp lệ. Vui lòng kiểm tra email và thực hiện lại." });
            else
            {
                ueResetPass.Password = EncodeUltis.MD5(form.newpass);
                ueResetPass.optCode = null;
                //update on database
                _userService.save(ueResetPass);
                return Ok("Thiết lập mật khẩu thành công. Hãy truy cập website bằng mật khẩu mới này nhé!");
            }
            //check timeout for opt code
        }

        [Authorize]
        [HttpPost("update-pass")]
        public IActionResult UpdatePassword(ResetPassForm form)
        {
            //if(form.id==0) return BadRequest(new { message = "Thông tin request không hợp lệ 'id = 0'!" });
            //UserEntity ueUpdate = _userService.getUserById(form.id);
            UserEntity ueUpdate = (UserEntity) HttpContext.Items["User"];
            HttpContext.Items["User"] = null;

            if (null != ueUpdate)
            {
                form.currentPass = EncodeUltis.MD5(form.currentPass);//check password hiện tại có đúng không ?
                if (form.checkOldPass(ueUpdate.Password) == false) return BadRequest(new { message = "Mật khẩu hiện tại không đúng!" });
                if (form.checkRepass() == false) return BadRequest(new { message = "Mật khẩu không trùng khớp!" });
                form.newpass = EncodeUltis.MD5(form.newpass);
                if (form.checkNewPassEqualsOldPass(ueUpdate.Password) == true) return BadRequest(new { message = "Mật khẩu mới hiện tại đang được sử dụng. Hãy thử với mật khẩu mới!" });

                //saved password on database
                ueUpdate.Password = form.newpass;
                _userService.save(ueUpdate);

                return Ok(_userService.createUserJWT(ueUpdate));
            }
            else
            {
                return BadRequest(new { message = "Hệ thống đang gặp sự cố. Vui lòng thực hiện sau!" });
            }
        }

        [Authorize]
        [HttpPut("edit/{id}")]
        public IActionResult EditUserInfo(int id, UserEntity info)
        {
            //UserEntity ueUpdate = _userService.getUserById(id);
            UserEntity ueUpdate = (UserEntity) HttpContext.Items["User"];
            HttpContext.Items["User"] = null;
            if (null != ueUpdate)
            {
                info.Email = ueUpdate.Email;//12092021
                info.Active = ueUpdate.Active;//12092021
                info.Id = id;
                info.Password = ueUpdate.Password;
                if (!ueUpdate.Email.Equals(info.Email))
                    if (_userService.checkEmail(info.Email) == true) return BadRequest(new { message = "Email cập nhật đã tồn tại trong hệ thống! Thử lại với một mail khác." });
                ueUpdate = info;
                ueUpdate.UserRoles = new UserRoleDAO().getAllRoleOfUserId(id);
                bool updated = _userService.save(ueUpdate);
                if (updated == true)
                {
                    return Ok(_userService.createUserJWT(ueUpdate));
                }
                else
                {
                    return BadRequest(new { message = "Hệ thống đang gặp sự cố!" });
                }
            }
            else
            {
                return  BadRequest(new { message= "Có lỗi xảy ra với user!"});
            }
        }
        //END
        //update user info version 2
        [HttpPut("edit-version2")]
        public async Task<IActionResult> EditUserInfoVersion2Async([FromForm] UserEntity info, [FromForm] IFormFile fileAvatar)
        {
            //getuser action 
            UserEntity ueUpdate = (UserEntity) HttpContext.Items["User"];
            if (null == ueUpdate) return BadRequest(new { message = "Vui lòng đăng nhập để sử dụng chức năng này!" });
            //xoa bo nhơ đệm
            HttpContext.Items["User"] = null;
            string nameImage = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + ueUpdate.Id;
            string filePath = FileProcess.getFullPath("image-avatar\\" + nameImage);
            info.Avatar = nameImage;
            Console.WriteLine("avatar update = " + info.Avatar);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileAvatar.CopyToAsync(stream);
            }

            if (null != ueUpdate)
            {
                info.Id = ueUpdate.Id;
                info.Password = ueUpdate.Password;
                if (!ueUpdate.Email.Equals(info.Email))
                    if (_userService.checkEmail(info.Email) == true) return BadRequest(new { message = "Email cập nhật đã tồn tại trong hệ thống! Thử lại với một mail khác." });
                ueUpdate = info;
                ueUpdate.UserRoles = new UserRoleDAO().getAllRoleOfUserId(ueUpdate.Id);
                bool updated = _userService.save(ueUpdate);
                if (updated == true)
                {
                    return Ok(_userService.createUserJWT(ueUpdate));
                }
                else
                {
                    return BadRequest(new { message = "Hệ thống đang gặp sự cố!" });
                }
            }
            else
            {
                return BadRequest(new { message = "Có lỗi xảy ra với user!" });
            }
        }
        //END

        /*
         * USER ORDER MANAGEMENT
         * 
        */
        //lay toan bo danh sach don hang theo user
        [HttpGet("orders-manage")]
        [Authorize]
        public List<CustomOrderResponse> GetListOrder()
        {
            List<CustomOrderResponse> listResponse = new List<CustomOrderResponse>();
            //lay user tu token
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            ICollection listOrder = orderDAO.GetOrdersByUserID(user.Id);
            foreach(OrderEntity oe in listOrder)
            {
                CustomOrderResponse coresp = new CustomOrderResponse();
                coresp.name = oe.Fullname;
                coresp.phone = oe.Phone;
                listResponse.Add(coresp.toOrderResponse(oe));
            }

            listResponse.Sort();
            listResponse.Reverse();
            return listResponse;
        } 

        //huy don hang, tham so id
        [HttpPut("orders-manage/deny/{id}")]
        [Authorize]
        public IActionResult DenyAOrder(int id, int _limit =10, int _page = 1)
        {
            OrderEntity orderDeny;
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            HttpContext.Items["User"] = null;//xoa bo nho dem
            if (null == (orderDeny = orderDAO.GetOrderByID(id))||orderDeny.UserId != user.Id )
            return BadRequest(new { message = "Đơn hàng của bạn không tồn tại!" });

            if (true == orderDAO.DenyOrderByID(id))
            {
                orderDeny.Status = 4;
                orderDAO.UpdateOrder(orderDeny);//update status order into db
                //to list
                List<CustomOrderResponse> listRes = new List<CustomOrderResponse>();
                List<OrderEntity> list = orderDAO.GetOrdersByUserID(user.Id, _limit, _page);

                listRes = new CustomOrderResponse().toListCustomOrderResponse(list);
                PageResponse<CustomOrderResponse> pageResponse = new PageResponse<CustomOrderResponse>();
                pageResponse.Data = listRes;
                pageResponse.Pagination = new Pagination(_limit, _page, orderDAO.GetCountOrdersByUserID(user.Id));
                return Ok(pageResponse);
            }
            return BadRequest(new { message = "Thông tin đơn hàng muốn hủy không hợp lệ hoặc đơn hàng đã được hủy từ trước!" });
        }

        //lay du lieu order bang status
        [HttpGet("orders-manage/status/{status}")]
        [Authorize]
        public List<CustomOrderResponse> GetOrdersByStatus(int status)
        {
            List<CustomOrderResponse> listRes = new List<CustomOrderResponse>();
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            ICollection<OrderEntity> list = orderDAO.GetOrdersByUserIDAndStatus(user.Id, status);

            foreach (OrderEntity oe in list)
            {
                CustomOrderResponse coresp = new CustomOrderResponse();
                coresp.name = user.Fullname;
                coresp.phone = user.phone;
                listRes.Add(coresp.toOrderResponse(oe));
            }

            listRes.Sort();
            listRes.Reverse();
            return listRes;

        }

        /*
         * quản lý đơn hàng (version 2)
         * lấy ds đơn hàng (lọc theo tình trạng, pagination, tìm kiếm đơn hàng bằng mã)
         */
        [HttpGet("orders")]
        [Authorize]
        public IActionResult GetListMyOrder(int _orderID = 0, int _status = 0, int _limit = 10, int _page = 1)
        {
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            if (user == null) return BadRequest(new { message = "Vui lòng đăng nhập trước khi sử dụng chức năng này!" });
           
            if (_orderID == 0)
            {
                List<CustomOrderResponse> listRes = new List<CustomOrderResponse>();
                List<OrderEntity> list = (_status != 0) ? orderDAO.GetOrdersByUserIDAndStatus(user.Id, _status, _limit, _page) : orderDAO.GetOrdersByUserID(user.Id, _limit, _page);

                listRes = new CustomOrderResponse().toListCustomOrderResponse(list);
                PageResponse<CustomOrderResponse> pageResponse = new PageResponse<CustomOrderResponse>();
                pageResponse.Data = listRes;
                pageResponse.Pagination = new Pagination(_limit, _page, orderDAO.GetCountOrdersByUserID(user.Id));
                return Ok(pageResponse);
            }
            else
            {
                if (null == orderDAO.getOrderByIDAndUserID(_orderID, user.Id)) return BadRequest(new { message = "Không tìm thấy đơn hàng có mã là " + _orderID + " trong danh sách!" });
                return Ok(new CustomOrderResponse().toOrderResponse(orderDAO.GetOrderByID(_orderID)));
            }
        }

        /*
         * USER UNBLOCKED ACCOUNT
         */
        //contact amdin unblocked account
        [HttpPost("unblocked")]
        public IActionResult UnblockedAccount([FromBody] UnblockedFromRequest req)
        {
            UserEntity user;
            if (null == (user = _userService.getUserByEmail(req.email)))
            {
                return BadRequest(new { message = "Email không tồn tại trong hệ thống!" });
            }
            else
            {
                if (user.Active == 1) return Ok("Tài khoản của bạn đã được mở khóa. Hãy trải nghiệm sản phẩm tại website bạn nhé!");
                StringBuilder sb = new StringBuilder();
                sb.Append("<p>Hệ thống nhận được yêu cầu mở khóa từ người dùng.</p>Nội dung mở khóa cho tài khoản: "+req.email+"</br>");
                sb.Append("<b><a>Truy cập website với tài khoản quản trị viên để mở khóa tài khoản người dùng!</b>");
                if (SendMailService.SendEmail("DHDTMobile: YÊU CẦU TỪ NGƯỜI DÙNG", UserEntity.EmailAdminFinal, sb.ToString()))
                {
                    return Ok("Chúng tôi đã nhận được yêu cầu của bạn. Vui lòng kiểm tra email thường xuyên để nhận được phản hồi từ quản trị viên!");
                }
                return BadRequest(new { message = "Hệ thống đang gặp sự cố. Vui lòng thực hiện lại sau ít phút!" });
            }
            
        }

    }
}
