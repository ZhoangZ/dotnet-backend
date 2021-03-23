using Microsoft.AspNetCore.Mvc;
using BackendDotnetCore.MoMo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BackendDotnetCore.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController:ControllerBase
    {
        public const string PARTNER_CODE = "MOMOUL6R20210302";
        public const string ACCESS_KEY = "49QZtzGfsBBGWYWF";
        public const string SERECT_KEY = "gqS9XTcwYzFIa0lORfX0te7zloslxZL4";

        public const string RETURN_URL = "localhost:5000//payment//returnurl";
        public const string NOTIFY_URL = "localhost:5000//payment//notifyurl";
        public string payment()
        {
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = PARTNER_CODE;
            string accessKey = ACCESS_KEY;
            string serectKey = SERECT_KEY;
            string orderInfo = "DH" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string returnUrl = RETURN_URL;
            string notifyUrl = NOTIFY_URL;

            string amount = "10000";
            string orderId = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();
            string extraData = "";
            string rawHash = "partnerCode=" +
            partnerCode + "&accessKey=" +
            accessKey + "&requestId=" +
            requestId + "&amount=" +
            amount + "&orderId=" +
            orderId + "&orderInfo=" +
            orderInfo + "&returnUrl=" +
            returnUrl + "&notifyUrl=" +
            notifyUrl + "&extraData=" +
            extraData;

           MoMoSecurity crypto = new MoMoSecurity();
            string signature = crypto.signSHA256(rawHash, serectKey);
            
           JObject message = new JObject{
                 {"partnerCode",partnerCode},
                 {"accessKey",accessKey},
                 {"requestId",requestId},
                 {"amount",amount},
                {"orderId",orderId},
                {"orderInfo",orderInfo},
                {"returnUrl",returnUrl},
                {"notifyUrl",notifyUrl},
                {"requestType","captureMoMoWallet"},
                {"signature",signature}
                };
            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
            JObject jmessage =  JObject.Parse(responseFromMomo);
            return jmessage.GetValue("payUrl").ToString();
            
        }
        [HttpGet("returnurl")]
        public void ReturnUrl()
        {
            string param = Request.QueryString.ToString().Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
            param = WebUtility.UrlDecode(param);
            MoMoSecurity crypto = new MoMoSecurity();
            string serectKey = SERECT_KEY;
            string signature = crypto.signSHA256(param, serectKey);
            if (signature != Request.Query["signature"].ToString())
            {
                
                Console.WriteLine("Thông tin Request không hợp lệ");
                
            }
            if (!Request.Query["errorCode"].Equals("0"))
            {
                Console.WriteLine("Thanh toán thất bại");
                
            }
            else
            {
                Console.WriteLine("Thanh toán thành công");
                

            }
           
        }
        [HttpPost("notifyurl")]
        public void NotifyUrl()
        {
            string param = "partner_code=" + Request.Query["partner_code"] +
            "&access_key=" + Request.Query["access_key"] +
            "&amount=" + Request.Query["amount"] +
            "&order_id=" + Request.Query["order_id"] +
            "&order_info=" + Request.Query["order_info"] +
            "&order_type=" + Request.Query["order_type"] +
            "&transaction_id=" + Request.Query["transaction_id"] +
            "&message=" + Request.Query["message"] +
            "&response_time=" + Request.Query["response_time"] +
            "&status_code=" + Request.Query["status_code"];
                        param = WebUtility.UrlDecode(param);
            MoMoSecurity crypto = new MoMoSecurity();
            string serectKey = SERECT_KEY;
            string status_code = Request.Query["status_code"].ToString();
            if ((status_code != "0"))
            {
                //Thất bại - Cập nhật trạng thái đơn hàng
            }
            else
            {
                //Thành công - Cập nhật trạng thái đơn hàng
            }
            //return new Newtonsoft.Json.JsonSerializerSettings();
        }
    }
}
