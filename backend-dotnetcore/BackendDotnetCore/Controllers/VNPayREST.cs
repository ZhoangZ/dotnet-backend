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
using BackendDotnetCore.Enitities;

namespace BackendDotnetCore.Controllers
{
    [ApiController]
    [Route("payment")]
    public class VNPayREST : ControllerBase
    {
        private PaymentDAO paymentDAO;
        public VNPayREST(PaymentDAO dao)
        {
            this.paymentDAO = dao;
        }

        [HttpPost]
        public ActionResult payment()
        {
            PaymentEntity paymentEntity = new PaymentEntity();
            paymentEntity = this.paymentDAO.AddPayment(paymentEntity);
            paymentEntity.gender();
            paymentEntity = this.paymentDAO.UpdatePayment(paymentEntity);
            return Ok(paymentEntity);

        }
        [HttpGet("{id}")]
        public ActionResult GetPayment(int id)
        {
            if (id == 0) return BadRequest(new MessageResponse("Thiếu param id", "param request"));
            try
            {
                PaymentEntity paymentEntity  = this.paymentDAO.getPayment(id);

                return Ok(paymentEntity);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
          

            

        }

    }
}
