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
    [Route("api/rom")]
    public class RomREST:ControllerBase
    {

        
        RomDAO romDAO;
        public RomREST()
        {
            
            romDAO = new RomDAO();
        }

        [HttpPost]
        [Authorize]
        //[Authorize(Roles = "Admin")]
        public ActionResult ram([FromBody] RomEntity entity)
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (user == null) return BadRequest("Chưa đăng nhập.");
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            var a =romDAO.AddEntity(entity);


            return Ok(a);

        }

        [HttpPut("{id}")]
        [Authorize]
        //[Authorize(Roles = "Admin")]
        public ActionResult ramUpdate([FromBody] RomEntity entity, int id)
        {
            entity.Id = id;
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (user == null) return BadRequest("Chưa đăng nhập.");
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            var a=romDAO.UpdateRAM(entity);


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
            if (user == null) return BadRequest("Chưa đăng nhập.");
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            var a = romDAO.getEntityById(id);
            return Ok(a);

        }

        [HttpDelete("{id}")]
        [Authorize]
        //[Authorize(Roles = "Admin")]
        public ActionResult ramD( int id)
        {
            var entity = romDAO.getEntityById(id);
            if (entity == null) return BadRequest("Không tìm thấy Rom");
            entity.Deleted = true;
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (user == null) return BadRequest("Chưa đăng nhập.");
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            var a = romDAO.UpdateRAM(entity);


            return Ok(a);

        }


    }
}
