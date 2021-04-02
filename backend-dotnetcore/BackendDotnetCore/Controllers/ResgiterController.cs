using BackendDotnetCore.DAO;
using BackendDotnetCore.Enitities;
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
        private UserDAO UserDAO = new UserDAO();

        public ResgiterController(UserDAO userDAO)
        {
            this.UserDAO = userDAO;
        }
        
        [HttpPost("register")]
        public UserEntity DoRegisterAccount([FromBody]UserEntity UserEntity)
        {
            Console.WriteLine(UserEntity);
            UserDAO.Save(UserEntity);
            //roleDAO.Save(UserEntity.Role);
            return UserEntity;
        }



       
    }
}
