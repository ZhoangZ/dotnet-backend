using BackendDotnetCore.DAO;
using BackendDotnetCore.Enitities;
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
        public string GetAllElementRole(string roleName)
        {
            return "";
        }

        [HttpPost("role")]
        public IActionResult CreateNewRole(RoleEntity roleEntity)
        {
            if (roleEntity.checkRoleInfo() == true)
            {
                RoleEntity rs = roleDAO.Save(roleEntity);
                //return "a record has be insert into table!";
                return Ok(rs);
            }
            else
            {
                return BadRequest(roleEntity);
            }
        }

        [HttpPost("user")]
        public UserEntity CreateUser(UserEntity userEntity)
        {
            if (userEntity.checkUserInfo() == true)
            {
              
                userEntity.Active = 1;
                var role=userDAO.GetRoleFirst();
                /*   userEntity.Roles = new List<RoleEntity>();
                   userEntity.Roles.Add(role);*/
                UserRole us = new UserRole();
                us.Role = role;
                us.User = userEntity;
                userEntity.UserRoles = new List<UserRole>();
                userEntity.UserRoles.Add(us);
                Console.WriteLine(userEntity);
                userDAO.Save2(userEntity);
                return userEntity;
            }
            else
            {
                return null;
            }
        }

        [HttpGet("users")]
        public List<UserEntity> GetAllUsers()
        {
            List<UserEntity> users = userDAO.getAll();
            return users;
        }

    }
}
