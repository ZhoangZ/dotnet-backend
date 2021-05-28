using Microsoft.AspNetCore.Mvc;
using BackendDotnetCore.MoMo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Collections;
using BackendDotnetCore.Response;
using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using System.Net.Http;

namespace BackendDotnetCore.Controllers
{
    [ApiController]
    [Route("payment")]
    public class VNPayREST : ControllerBase
    {
        private PaymentDAO paymentDAO;
        private HttpClient httpClient;
        public VNPayREST(PaymentDAO dao, HttpClient httpClient)
        {
            this.paymentDAO = dao;
            this.httpClient = httpClient;
        }

        [HttpPost("donate")]
        [Authorize]
        public ActionResult donate([FromBody] DonateRequest donateRequest)
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;


            PaymentEntity paymentEntity = new PaymentEntity();
            paymentEntity.userId = user.Id;
            paymentEntity.Amount = donateRequest.Amount;
            paymentEntity.CurrCode = donateRequest.Currcode;
            paymentEntity.UrlReturn = donateRequest.UrlReturn;
            paymentEntity.CreateTime =  DateTime.Now;
            paymentEntity.IpAddress = "119.17.249.22";
            paymentEntity = paymentDAO.AddPayment(paymentEntity);
            paymentEntity.gender("https://localhost:25002/payment/donate");
            paymentEntity = paymentDAO.UpdatePayment(paymentEntity);
            return Ok(paymentEntity);

        }
        [HttpGet("donate/{id}")]
        public ActionResult GetPayment(int id)
        {
            if (id == 0) return BadRequest(new MessageResponse("Thiếu param id", "param request"));
            try
            {
                PaymentEntity paymentEntity  = this.paymentDAO.getPayment(id);
                paymentEntity= paymentEntity.querry(httpClient);
                paymentEntity = this.paymentDAO.UpdatePayment(paymentEntity);
                string urlReturn=paymentEntity.UrlReturn;
                urlReturn=urlReturn.Substring(0, urlReturn.LastIndexOf("?"));
                urlReturn += "?"+paymentEntity.ParamsUrlStatus;
                return Ok(new { Url = urlReturn }) ;
                //return Ok(paymentEntity);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
          

            

        }


    }
    public class DonateRequest
    {
        public string Currcode { get; set; }
        public string UrlReturn { get; set; }
        public long Amount { get; set; }
    }
   
}
