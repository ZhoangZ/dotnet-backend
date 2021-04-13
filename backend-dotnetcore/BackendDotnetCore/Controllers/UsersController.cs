using Microsoft.AspNetCore.Mvc;
using BackendDotnetCore.Models;
using BackendDotnetCore.Services;
using System;
using BackendDotnetCore.Enitities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
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
            var users = _userService.GetAll();
            return Ok(users);
        }
        [HttpGet("test")]
        public IActionResult Test()
        {
            var users = _userService.GetAll();
            return Ok(users);
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

    }
}
