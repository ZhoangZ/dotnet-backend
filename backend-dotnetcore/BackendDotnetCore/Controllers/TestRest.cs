using Microsoft.AspNetCore.Mvc;
using BackendDotnetCore.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendDotnetCore.DAO;
using BackendDotnetCore.Enitities;
using System.Collections;
using BackendDotnetCore.Ultis;
using BackendDotnetCore.Models;

namespace BackendDotnetCore.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestRest:ControllerBase
    {
        [HttpGet("hello")]

        public MessageResponse Test()

        {
            return new MessageResponse("Xin chào","Hello");
        }
        [HttpGet("/")]

        public MessageResponse Mai()

        {
            return new MessageResponse("Xin chào", "Hello");
        }
        [HttpGet("actionresult")]

        public ActionResult Test2()

        {
            
            return Ok(new MessageResponse("Xin chào","Hello"));
        }

        private AccountDAO dao;
        private Product2DAO productDAO;
        public TestRest(AccountDAO dao, Product2DAO productDAO)
        {
            this.dao = dao;
            this.productDAO = productDAO;
        }
        [HttpGet("mysql")]
        public ActionResult TestMySQL()

        {

            return Ok(dao.getAccount(1));
        }
        [HttpGet("include")]
        public ActionResult TestInclude()

        {
            Product2 p=productDAO.getProduct(1);                      
            return Ok(p);
        }

        [HttpGet("many")]
        public ActionResult TestMany()

        {
            UserDAO p = new UserDAO();
            return Ok(p.GetUserFirst());
        }
        [HttpGet("mid")]
        public ActionResult TestMid()

        {
            UserDAO p = new UserDAO();
            return Ok(p.GetUserRolesFirst());
        }
       

       
       
    }
}
