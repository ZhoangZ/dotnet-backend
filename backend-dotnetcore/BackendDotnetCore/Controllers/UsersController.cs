using BackendDotnetCore.Entities;
using BackendDotnetCore.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Forms;
using Microsoft.AspNetCore.Mvc;
using System;
using BackendDotnetCore.DAO;

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
       

        [HttpGet("reset-pass")]
        public string ResetPassword(string email)
        {
            //chuoi tra ve cho nguoi dung xac nhan, co the check thoi gian ton tai cua ma
            string rd = new Random().Next(1000000).ToString();

            if (_userService.checkEmail(email) == true)
            {

                return rd;
            }
            else
            {
                return "Email không tồn tại!";
            }

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
