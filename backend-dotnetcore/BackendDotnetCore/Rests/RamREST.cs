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
    [Route("api/ram")]
    public class RamREST:ControllerBase
    {

        RamDAO ramDAO;

        public RamREST()
        {
            
            ramDAO = new RamDAO();
        }

        [HttpPost]
        [Authorize]
        //[Authorize(Roles = "Admin")]
        public ActionResult ram([FromBody] RamEntity entity)
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            var a =ramDAO.AddEntity(entity);


            return Ok(a);

        }

        [HttpPut("{id}")]
        [Authorize]
        //[Authorize(Roles = "Admin")]
        public ActionResult ramUpdate([FromBody] RamEntity entity, int id)
        {
            entity.Id = id;
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            var a=ramDAO.UpdateRAM(entity);


            return Ok(a);

        }

        [HttpDelete("{id}")]
        [Authorize]
        //[Authorize(Roles = "Admin")]
        public ActionResult ramD( int id)
        {
            var entity = ramDAO.getRamById(id);
            if (entity == null) return BadRequest("Không tìm thấy Ram");
            entity.Deleted = true;
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            var a = ramDAO.UpdateRAM(entity);


            return Ok(a);

        }

        [HttpGet("{id}")]
        [Authorize]
        //[Authorize(Roles = "Admin")]
        public ActionResult getRam(int id)
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            var a = ramDAO.getRamById(id);
            return Ok(a);

        }


    }
}
