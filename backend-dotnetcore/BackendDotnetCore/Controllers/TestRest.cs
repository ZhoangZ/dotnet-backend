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

namespace BackendDotnetCore.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestRest:ControllerBase
    {
        [HttpGet("hello")]

        public MessageResponse Test()

        {
            return new MessageResponse("Hello");
        }
        [HttpGet("/")]

        public MessageResponse Mai()

        {
            return new MessageResponse("Hello");
        }
        [HttpGet("actionresult")]

        public ActionResult Test2()

        {
            
            return Ok(new MessageResponse("Hello"));
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
        [HttpGet("onep")]
        public ActionResult TestOneP()

        {
            
            return Ok(productDAO.GetOneP());
        }

        [HttpGet("product/list")]
        public ActionResult testListProduct(int _limit,int _page, string sort ="idaz", int lte=-1, int gte=-1)


        {
          
            List<Product2> lst = productDAO.getList(_page,_limit, sort,lte, gte);

            lst.setRequset(Request);

            return Ok(lst);
        }
       
    }
}
