using Microsoft.AspNetCore.Mvc;
using BackendDotnetCore.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendDotnetCore.DAO;
using BackendDotnetCore.Enitities;
using System.Collections;

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
        [HttpGet("actionresult")]

        public ActionResult Test2()

        {
            
            return Ok(new MessageResponse("Hello"));
        }

        private AccountDAO dao;
        private ProductDAO productDAO;
        public TestRest(AccountDAO dao, ProductDAO productDAO)
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
            Product p=productDAO.getProduct(1);                      
            return Ok(p);
        }

        [HttpGet("list")]
        public ActionResult TestListProduct(int _limit,int _page, string sort ="idaz")

        {
            List<Product> lst = productDAO.getList(_page,_limit, sort);
            return Ok(lst);
        }
       
    }
}
