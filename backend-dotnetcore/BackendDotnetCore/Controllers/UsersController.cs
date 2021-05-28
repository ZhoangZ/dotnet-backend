using BackendDotnetCore.Entities;
using BackendDotnetCore.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BackendDotnetCore.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

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
       
    }
}
