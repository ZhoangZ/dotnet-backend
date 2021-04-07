using BackendDotnetCore.DAO;
using BackendDotnetCore.Enitities;
using BackendDotnetCore.Forms;
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
        private RoleDAO RoleDAO;
        public ResgiterController(UserDAO userDAO)
        {
            this.UserDAO = userDAO;
        }
        [HttpPost("register")]
        public UserEntity DoRegisterAccount([FromBody]RegisterForm RegisterEntity)
        {
            Console.WriteLine("Create a new users:"+ RegisterEntity);
            UserEntity userEntity = new UserEntity();
            userEntity.Username = RegisterEntity.username;
            userEntity.Password = RegisterEntity.password;
            userEntity.Email = RegisterEntity.email;
            userEntity.Active = true;
            userEntity.Blocked = true;//blocked ?
            if (RegisterEntity.password.Equals(RegisterEntity.repassword)) {
                userEntity.Confirmed = true;
                UserDAO.Save(userEntity);
            }
            else
            {
                userEntity.Confirmed = false;
            }
            return userEntity;
        }
    }
}
