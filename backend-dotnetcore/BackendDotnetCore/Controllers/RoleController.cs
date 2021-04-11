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
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        private RoleDAO roleDAO;

        public RoleController(RoleDAO roleDAO)
        {
            this.roleDAO = roleDAO;
        }

        [HttpGet]
        public string GetHelloMessage()
        {
            return "Hello";
        }

        [HttpPost("new")]
        public string CreateRole(RoleEntity roleEntity)
        {
            if (roleEntity.checkRoleInfo() == true)
            {
                roleDAO.Save(roleEntity);
                return "a record has be insert into table!";
            }
            else
            {
                return "RoleEntity info not correct!";
            }
        }

       
    }
}
