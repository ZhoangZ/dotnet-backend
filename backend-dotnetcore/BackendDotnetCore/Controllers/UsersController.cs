using Microsoft.AspNetCore.Mvc;
using BackendDotnetCore.Models;
using BackendDotnetCore.Services;
using System;
using BackendDotnetCore.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Forms;

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

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            
            Account user= (Account)HttpContext.Items["User"];
            Console.WriteLine(user);
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
        [HttpPut("{id}")]
        public string UpdateAction(int id, [FromBody] Account accountInfo)
        {
            Account ac = _userService.getAccountById(id);
            if (ac.Email.Equals(accountInfo.Email))
            {
                ac.Email = accountInfo.Email;
            }
            else
            if (_userService.checkEmail(accountInfo.Email) == true)
            {
                return "Email đã tồn tại. Thử lại!";
            }else
            {
                ac.Email = accountInfo.Email;
            }
            //checkFields
            ac.Username = accountInfo.Username;
            _userService.save(ac);


            return "Cập nhật thành công !"; 
        }

        [HttpGet("user")]
        //phuong thuc lay ra mot user bang id
        public Account GetAccountById(int id)
        {
            Account account = null;
            if(null != (account = _userService.getAccountById(id)))
            {
                return account;
            }
            else
            {
                return account;
            }
        }

        ///

        ///
        [HttpPut("edit-ver2/{id}")]
        public IActionResult EditUserInfo(int id, UserEntity info)
        {
            Console.WriteLine("Edit user: "+id+", "+info.Fullname);
            UserEntity ueUpdate = _userService.getUserById(id);
            if (null != ueUpdate)
            {
                info.Id = id;
                info.Password = ueUpdate.Password;
                if (!ueUpdate.Email.Equals(info.Email))
                    if (_userService.checkEmail(info.Email) == true) return BadRequest(new { message = "Email cập nhật đã tồn tại trong hệ thống! Thử lại với một mail khác." });
                ueUpdate = info;
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
