using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Ultis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private RoleDAO roleDAO = new RoleDAO();
        private UserDAO userDAO = new UserDAO();

        [HttpGet]
        public List<RoleEntity> GetAllElementRole()
        {
            List<RoleEntity> roles = roleDAO.getAllRole();
            return roles;
        }

        //add new a role
        [HttpPost("role")]
        public IActionResult CreateNewRole(RoleEntity roleEntity)
        {
            if (roleEntity.checkRoleInfo() == true)
            {
                RoleEntity rs = roleDAO.Save(roleEntity);
                return Ok(rs);
            }
            else
            {
                return BadRequest(roleEntity);
            }
        }


        //admin get all users
        [HttpGet("users")]
        [Authorize]
        public List<UserEntity> GetAllUsers()
        {
            //lay tai khoan dang dang nhap tu token
            UserEntity userAction = (UserEntity)HttpContext.Items["User"];
            Console.WriteLine("userAction = "+userAction.ToString());
            if (userDAO.isAdmin(userAction.Id) == false) return new List<UserEntity>();

            List<UserEntity> users = userDAO.GetListUsers();
            return users;
        }

        //admin blocked a account user
        [HttpPost("users/blocked")]
        [Authorize]
        public IActionResult BlockedAUser([FromBody] int userID)
        {
            //lay tai khoan dang dang nhap tu token
            UserEntity userAction = (UserEntity)HttpContext.Items["User"];
            if (userDAO.isAdmin(userAction.Id) == false) return BadRequest(new { message = "Không có quyền truy xuất. Thử lại với tài khoản quản trị viên." });

            UserEntity userBlocked;
            if(null == (userBlocked = userDAO.getOneById(userID)))
            return BadRequest(new { message = "Không tồn tại tài khoản người dùng trong hệ thống!" });

            if (true == userDAO.BlockedOneUser(userID)) return Ok(userBlocked);
            return BadRequest(new { message = "Hệ thống đang gặp sự cố. Vui lòng thử lại sau!" });
        }


        
    }
}
