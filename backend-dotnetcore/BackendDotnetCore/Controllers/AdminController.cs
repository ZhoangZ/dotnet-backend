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
                var role = userDAO.GetRoleFirst();
                
                UserRole us = new UserRole();
                us.Role = role;
                us.User = userEntity;
                userEntity.UserRoles = new List<UserRole>();
                userEntity.UserRoles.Add(us);
                userEntity.Password = EncodeUltis.MD5(userEntity.Password);
                Console.WriteLine(userEntity);
                int id = userDAO.Save(userEntity);
                userEntity.Id = id;
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
