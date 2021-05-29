using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Forms;
using BackendDotnetCore.Services;
using BackendDotnetCore.Ultis;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BackendDotnetCore.Controllers
{
    [ApiController]
    [Route("auth/local")]
    public class ResgiterController : ControllerBase
    {
        private UserDAO UserDAO;
        private IUserService _userService;

      

        public ResgiterController(UserDAO userDAO, IUserService userService)
        {
            this.UserDAO = userDAO;
            this._userService = userService;
        }

        [HttpPost("register")]
        public IActionResult DoRegisterVersion2([FromBody] RegisterForm registerForm)
        {
            if (UserDAO.getOneByUsername(registerForm.username) != null) return BadRequest(new { message = "Username đã tồn tại trong hệ thống!" });
            if (null != UserDAO.getOneByEmail(registerForm.email)) return BadRequest(new { message = "Email đã tồn tại trong hệ thống!" });
            if (registerForm.checkInfo().Equals("success"))
            {
                UserEntity userEntity = registerForm.parseEntity();
                string json = JsonConvert.SerializeObject(userEntity);

                userEntity.Active = 1;
                RoleEntity role = UserDAO.GetRoleFirst();
                UserRole us = new UserRole();
                us.Role = role;
                us.User = userEntity;
                userEntity.UserRoles.Add(us);
                userEntity.Password = EncodeUltis.MD5(userEntity.Password);
                UserEntity uResp = UserDAO.Save(userEntity);
                var response = _userService.createUserJWT(uResp);
                if(null == response) return BadRequest(new { message = "Hệ thống đang gặp sự cố!" });
                return Ok(response);
            }
            else
            {
                return BadRequest(new { message = "Error:"+ registerForm.checkInfo()});
            }
        }
        
    }
}
