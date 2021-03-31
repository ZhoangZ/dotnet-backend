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

        public MessageResponse test()

        {
            return new MessageResponse("Hello");
        }
        [HttpGet("actionresult")]

        public ActionResult test2()

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
        public ActionResult testMySQL()

        {

            return Ok(dao.getAccount(1));
        }
        [HttpGet("include")]
        public ActionResult testInclude()

        {
            Product p=productDAO.getProduct(1);                      
            return Ok(p);
        }

        [HttpGet("product/list")]
        public ActionResult testListProduct(int _limit,int _page, string sort ="idaz", int lte=-1, int gte=-1)

        {
          
            List<Product> lst = productDAO.getList(_page,_limit, sort,lte, gte);

            lst.setRequset(Request);

            return Ok(lst);
        }
       
    }
}
