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
        public string DoRegisterAccount([FromBody] UserEntity userEntity)
        {
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
                UserDAO.Save(userEntity);
                return "a record user has be insert into table!";
            }
            else
            {
                return "AccountInfo is not correct!"; 
            }
        }
    }
}
