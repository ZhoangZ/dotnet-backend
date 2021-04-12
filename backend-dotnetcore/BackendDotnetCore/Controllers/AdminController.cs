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
        public string CreateNewRole(RoleEntity roleEntity)
        {
            if (roleEntity.checkRoleInfo() == true)
            {
                roleDAO.Save(roleEntity);
                return "a record has be insert into table!";
            }
            else
            {
                return "Role info not correct!";
            }
        }

        [HttpPost("user")]
        public string CreateUser(UserEntity userEntity)
        {
            if (userEntity.checkUserInfo() == true)
            {
                //guest dang ki thi luon luon duoc gan quyen la USER
                userEntity.Active = 1;
                UserRole userRole = new UserRole(userEntity.Id, 2);
                userEntity.UserRoles.Add(userRole);
                userDAO.Save(userEntity);
                return "a record user has be insert into table!";
            }
            else
            {
                return "UserInfo is not correct!";
            }
        }

    }
}
