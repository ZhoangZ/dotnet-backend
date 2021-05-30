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
                return Ok(rs);
            }
            else
            {
                return BadRequest(roleEntity);
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
