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
    [Route("/auth/local/")]
    public class ResgiterController : ControllerBase
    {
        private UserDAO UserDAO = new UserDAO();
        
        [HttpPost("register")]
        public UserEntity DoRegisterAccount([FromBody]UserEntity UserEntity)
        {
            Console.WriteLine(UserEntity.Id);
            UserDAO.Save(UserEntity);
            //roleDAO.Save(UserEntity.Role);
            return UserEntity;
        }



       
    }
}
