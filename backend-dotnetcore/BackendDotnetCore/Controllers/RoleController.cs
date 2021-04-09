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
    [ApiController]
    [Route("admin/role")]
    public class RoleController : ControllerBase
    {

        //public RoleDAO roleDAO;

        public RoleController(RoleDAO roleDAO)
        {
            //this.roleDAO = roleDAO;
        }

        [HttpGet("new")]
        public string createRole(string name)
        {
            //Console.WriteLine(roleEntity.ToString());
            //checkRoleInfo
            //if (roleEntity.checkRoleInfo() == true)
            //{
            //roleDAO.Save(roleEntity);
            //return roleEntity;
            //}
            //else
            //{
            //    return null;
            //}
            return "Hello "+name;
        }

    }
}
