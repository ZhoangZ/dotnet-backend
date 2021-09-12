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
    [Route("api/revenue")]
    public class RevenueREST : ControllerBase
    {

        RevenueEntityDAO revenueEntityDAO;
        public RevenueREST()
        {

            revenueEntityDAO = new RevenueEntityDAO();
        }

        [HttpGet("month/{year}/{month}")]
        [Authorize]
        public ActionResult month(int year,int month)
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            var a = revenueEntityDAO.getEntity(year,month);
            if (a == null)
            {
                return BadRequest("Không có dữ liệu");
            }


            return Ok(a);

        }

        [HttpGet("month")]
        [Authorize]
        public ActionResult monthNow()
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            int year = DateTime.Now.Year;
            int  month= DateTime.Now.Month;
            int beforeMonth = month-1;
            int beforeYear = year;
            var now = revenueEntityDAO.getEntity(year,month);
            if (month == 0)
            {
                beforeYear = beforeYear - 1;
                beforeMonth = 12;
            }
            var before = revenueEntityDAO.getEntity(beforeYear, beforeMonth);

            if (now == null)
            {
                now = new RevenueEntity();
                //return BadRequest("Tháng này chưa có thông tin ");
            }
            if (before == null)
            {
                before = new RevenueEntity();
                //return BadRequest("Tháng trước chưa có thông tin");
            }
            decimal risingMoney;
            if (before.Money == 0)
            {
                risingMoney = ((now.Money - before.Money) / 1) * 100;
            }
            else risingMoney = ((now.Money - before.Money) / before.Money) * 100;
            decimal risingQuantity;
            if (before.Quantity == 0)
            {
                risingQuantity = ((now.Quantity - before.Quantity) / 1) * 100;
            }
            
            else risingQuantity = ((now.Quantity - before.Quantity) / before.Quantity) * 100;
            //Console.WriteLine("rising" + rising);
            return Ok(new {now=now, before=before, risingMoney= Math.Round(risingMoney,2) , risingQuantity= Math.Round(risingQuantity,2) });

        }

        [HttpGet("year")]
        [Authorize]
        public ActionResult year()
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            int year = DateTime.Now.Year;
          
            var now = revenueEntityDAO.sumYearMoney(year);          
            var now2 = revenueEntityDAO.sumYearQuantity(year);



            return Ok(new { sumMoneyOfYear = now, sumQuantityOfYear = now2});

        }

        [HttpGet("year/{year}")]
        [Authorize]
        public ActionResult year(int year)
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");


            var now = revenueEntityDAO.sumYearMoney(year);
            var now2 = revenueEntityDAO.sumYearQuantity(year);



            return Ok(new { sumMoneyOfYear = now, sumQuantityOfYear = now2 });

        }





        [HttpGet("chart/money")]
        [Authorize]
        public ActionResult chartMoney()
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            int year = DateTime.Now.Year;
            var entitys = revenueEntityDAO.getEntitys(year);
            if (entitys == null) return BadRequest("Năm nay chưa có thông tin ");
            var a=entitys.Select(x => new { name=x.Month,money=x.Money});
            return Ok(a);

        }

        [HttpGet("chart/quantity")]
        [Authorize]
        public ActionResult chartQuantity()
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
            int year = DateTime.Now.Year;
            var entitys = revenueEntityDAO.getEntitys(year);
            if (entitys == null) return BadRequest("Năm nay chưa có thông tin ");
            var a = entitys.Select(x => new { name = x.Month, quantity = x.Quantity });
            return Ok(a);

        }

        [HttpGet("chart/money/year/{year}")]
        [Authorize]
        public ActionResult chartMoney(int year)
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");
           
            var entitys = revenueEntityDAO.getEntitys(year);
            if (entitys == null) return BadRequest("Năm nay chưa có thông tin ");
            var a = entitys.Select(x => new { name = x.Month, money = x.Money });
            return Ok(a);

        }

        [HttpGet("chart/quantity/year/{year}")]
        [Authorize]
        public ActionResult chartQuantity(int year)
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            if (!user.IsAdmin) return BadRequest("Không phải tài khoản admin");

            var entitys = revenueEntityDAO.getEntitys(year);
            if (entitys == null) return BadRequest("Năm nay chưa có thông tin ");
            var a = entitys.Select(x => new { name = x.Month, Quantity = x.Quantity });
            return Ok(a);

        }


    }
}
