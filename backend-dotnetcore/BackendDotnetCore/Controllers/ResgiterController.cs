﻿using BackendDotnetCore.DAO;
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
        private UserDAO UserDAO;
        private RoleDAO RoleDAO;
        public ResgiterController(UserDAO userDAO)
        {
            this.UserDAO = userDAO;
        }
        [HttpPost("register")]
        public UserEntity DoRegisterAccount([FromBody]UserEntity UserEntity)
        {
            Console.WriteLine("Create a new users:"+UserEntity);
            UserDAO.Save(UserEntity);
            RoleDAO.Save(UserEntity.Role);
            return UserEntity;
        }
    }
}
