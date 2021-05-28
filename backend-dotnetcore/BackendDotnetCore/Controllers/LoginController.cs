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

namespace BackendDotnetCore.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private UserDAO userDAO;
        private IUserService _userService;
        public LoginController( IUserService userService)
        {
            this.userDAO = new UserDAO();
            this._userService = userService;
        }

        [HttpPost("user")]
        public IActionResult DoLoginVer2([FromBody] LoginForm loginForm)
        {
            if (userDAO.getOneByEmail(loginForm.Email) == null) return BadRequest(new { message = "Username hoặc password không đúng!" });
            var response = _userService.loginAuthenticateByEmail(loginForm);
            if (response == null) return BadRequest(new { message = "Username hoặc password không đúng!" });
            HttpContext.Session.SetInt32("idUserSession", response.user.Id);
            return Ok(response);

        }

    }
}
