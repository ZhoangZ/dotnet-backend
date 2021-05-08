using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Forms;
using BackendDotnetCore.Services;
using BackendDotnetCore.Ultis;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult DoRegisterAccount([FromBody] UserEntity userEntity)
        {
            Console.WriteLine("Password = "+userEntity.Password);
            if (UserDAO.getOneByUsername(userEntity.Username) != null) return BadRequest(new { message = "Username đã tồn tại trong hệ thống!" });
            if (UserDAO.getOneByEmail(userEntity.Email) != null) return BadRequest(new { message = "Email đã tồn tại trong hệ thống!" });
            if (userEntity.checkUserInfo() == true)
            {
                userEntity.Active = 1;
                RoleEntity role = UserDAO.GetRoleFirst();
                UserRole us = new UserRole();
                us.Role = role;
                us.User = userEntity;
                userEntity.UserRoles = new List<UserRole>();
                userEntity.UserRoles.Add(us);
                Console.WriteLine(userEntity);
                userEntity.Password = EncodeUltis.MD5(userEntity.Password);
                UserEntity uResp = UserDAO.Save(userEntity);
                uResp.Password = "****";
                return Ok(uResp);
            }
            else
            {
                return BadRequest(new { message = "Hệ thống đang gặp sự cố. Vui lòng quay lại sau ít phút!" });
            }
           
        }
    }
}
