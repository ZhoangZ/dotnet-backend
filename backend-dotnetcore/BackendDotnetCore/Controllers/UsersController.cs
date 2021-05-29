using BackendDotnetCore.Entities;
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

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
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
       

        [HttpGet("forgot-pass")]
        public string ForgotPassword(string email)
        {
            if (_userService.checkEmail(email) == false) return "Email không tồn tại trong hệ thống. Vui lòng thực hiện lại!";
            string rdOtp = new Random().Next(10000000).ToString();
            return SendEmail(email, rdOtp);
        }
        public string SendEmail(string email, string opt)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                string SystemMail = "ongdinh1099@gmail.com";
                message.From = new MailAddress("ongdinh1099@gmail.com");
                message.To.Add(new MailAddress(email));
                message.Subject = "DHDTMobile: Yêu cầu cấp mật khẩu ?";
                message.IsBodyHtml = true; //to make message body as html  
                StringBuilder sb = new StringBuilder();
                sb.Append("Yêu cầu xác thực mật khẩu nhập, bạn vui lòng nhập mã sau trong quá trình thiết lập mật khẩu mới: \n");
                sb.Append("Mã OPT: " + opt+"\n");
                sb.Append("Lưu ý: Đây là mã bảo mật, vì vậy không được tiết lộ cho bất kỳ cá nhân hay tổ chức nào, điều này có thể khiến bạn mất tài khoản. Mã này sẽ tự động hủy sau 15 phút!");
                message.Body = sb.ToString();
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(SystemMail, "ongdinh101199");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                //save opt on database
                UserEntity ueRequest = _userService.getUserByEmail(email);
                ueRequest.optCode = opt;
                _userService.save(ueRequest);

                return "Yêu cầu đã được tiếp nhận. Hãy kiểm tra email của bạn!";
            }
            catch (Exception) { return "Hệ thống đang gặp sự cố! Vui lòng quay lại sau ít phút"; }
        }

        [HttpPost("reset-pass")]
        public string ResetPass([FromBody] ResetPassForm form)
        {
            //check email and update password for user account
            if (_userService.checkEmail(form.email) == false) return "Email không hợp lệ! Vui lòng kiểm tra lại.";
            UserEntity ueResetPass = _userService.getUserByEmail(form.email);
            if (EncodeUltis.MD5(form.newpass).Equals(ueResetPass.Password)) return "Bạn đã sử dụng mật khẩu này gần đây. Hãy thử lại với mật khẩu mới!";
            if (!form.newpass.Equals(form.repass)) return "Mật khẩu không trùng khớp!";
            string optCodeDB = ueResetPass.optCode;
            if (!optCodeDB.Equals(form.opt)) return "Mã opt không hợp lệ. Vui lòng kiểm tra email và thực hiện lại.";
            else
            {
                ueResetPass.Password = EncodeUltis.MD5(form.newpass);
                ueResetPass.optCode = null;
                //update on database
                _userService.save(ueResetPass);
                return "Thiết lập mật khẩu thành công. Hãy truy cập website bằng mật khẩu mới này nhé!";
            }
            //check timeout for opt code
        }


        [HttpPut("edit/{id}")]
        public IActionResult EditUserInfo(int id, UserEntity info)
        {
            UserEntity ueUpdate = _userService.getUserById(id);
            if (null != ueUpdate)
            {
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
                    return BadRequest(new { message = " Hệ thống đang gặp sự cố!" });
                }
            }
            else
            {
                return  BadRequest(new { message= " Có lỗi xảy ra với user!"});
            }
        }

       
    }
}
