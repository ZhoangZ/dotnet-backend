using Microsoft.AspNetCore.Mvc;
using BackendDotnetCore.Consts;
using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Froms;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendDotnetCore.Services;
using Microsoft.AspNetCore.Http;
using BackendDotnetCore.DTO;
using BackendDotnetCore.Helpers;

namespace BackendDotnetCore.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private UserDAO userDAO;
        private IUserService _userService;
        private CartDAO cartDAO;

        public LoginController(IUserService userService)
        {
            this.userDAO = new UserDAO();
            this._userService = userService;
            cartDAO = new CartDAO();
        }

        [HttpPost("user")]
        public IActionResult DoLoginVer2([FromBody] LoginForm loginForm)
        {
            if (userDAO.getOneByEmail(loginForm.Email) == null) return BadRequest(new { message = "Email không tồn tại trong hệ thống!" });
            var response = _userService.loginAuthenticateByEmail(loginForm);
            if (response == null) return BadRequest(new { message = "Mật khẩu không đúng!" });

            //Lấy Cart
            CartEntity c = cartDAO.getCart(response.user.Id);
            response.cart = new CartDTO(c);
            //check user has been blocked by admin
            if (response.user.Active == 0) return BadRequest(new { message = "Tài khoản hiện đang bị khóa. Vui lòng liên hệ quản trị viên để được trợ giúp!" });
            return Ok(response);

        }

    }
}
