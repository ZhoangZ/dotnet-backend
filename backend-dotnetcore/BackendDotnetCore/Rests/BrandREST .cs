using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Rests
{
    [ApiController]
    [Route("api/brand")]
    public class BrandREST:ControllerBase
    {


        BrandDAO brandDAO;
        public BrandREST()
        {
            
            brandDAO = new BrandDAO();
        }

        [HttpPost]
        [Authorize]
        //[Authorize(Roles = "Admin")]
        public ActionResult ram([FromBody] Brand entity)
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            var a =brandDAO.AddEntity(entity);


            return Ok(a);

        }

        [HttpPut("{id}")]
        [Authorize]
        //[Authorize(Roles = "Admin")]
        public ActionResult ramUpdate([FromBody] Brand entity, int id)
        {
            entity.Id = id;
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            var a=brandDAO.UpdateRAM(entity);


            return Ok(a);

        }

        [HttpDelete("{id}")]
        [Authorize]
        //[Authorize(Roles = "Admin")]
        public ActionResult ramD([FromBody] Brand entity, int id)
        {
            entity.Id = id;
            entity.Deleted = true;
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            var a = brandDAO.UpdateRAM(entity);


            return Ok(a);

        }

        [HttpGet("{id}")]
        [Authorize]
        //[Authorize(Roles = "Admin")]
        public ActionResult getRom(int id)
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            var a = brandDAO.getEntityById(id);
            return Ok(a);

        }


    }
}
