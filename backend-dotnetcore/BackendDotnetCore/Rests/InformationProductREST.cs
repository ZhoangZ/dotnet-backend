﻿using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Rests
{
    [ApiController]
    [Route("api/information/{productId}")]
    public class InformationProductREST:ControllerBase
    {


        InformationProductDAO dao;
        public InformationProductREST()
        {

            dao = new InformationProductDAO();
        }

        [HttpPost]
        [Authorize]
        public ActionResult post([FromBody] InformationProduct entity, int productId)
        {
            entity.ProductId = productId;
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            var a =dao.AddEntity(entity);


            return Ok(a);

        }
        [HttpPost("many")]
        [Authorize]
        public ActionResult many([FromBody] ICollection<InformationProduct> entitys, int productId)
        {
           
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            //dao.DeleteAllByProductId(productId);
            if (entitys != null)
            {
                var rs = dao.AddEntitys(entitys, productId);


            return Ok(rs);

            }
            else
            {
                return BadRequest("Đã xóa hết tất cả information của product này.");
            }

        }

        [HttpPut("{id}")]
        [Authorize]
        //[Authorize(Roles = "Admin")]
        public ActionResult update([FromBody] InformationProduct entity, int id, int productId)
        {
            var befored = dao.getEntityById(id);
            if(befored.Id!=id) return BadRequest("Id không đúng");
            if(befored.ProductId!=productId) return BadRequest("Product Id không đúng");

            befored.content = entity.content;
            befored.name = entity.name;
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            var a=dao.UpdateEntity(befored);


            return Ok(a);

        }

        [HttpDelete("{id}")]
        [Authorize]
        //[Authorize(Roles = "Admin")]
        public ActionResult ramD( int id, int productId)
        {
            var entity = dao.getEntityById(id);

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            var a = dao.DeletedEntity(entity);


            return Ok(a);

        }

        [HttpGet("{id}")]
       
        //[Authorize(Roles = "Admin")]
        public ActionResult getRom(int id)
        {

          
           
            var a = dao.getEntityById(id);
            if(a!=null)
            return Ok(a);
            return BadRequest();

        }

        [HttpGet]
        
       
        public ActionResult getAll(int productId)
        {

           
            var a = dao.getEntitysByForeId(productId);
            if (a != null)
                return Ok(a);
            return BadRequest();

        }


    }
}
