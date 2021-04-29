using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Controllers
{
    public class VNPayConfig
    {
        public const string vnp_PayUrl = "http://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        public const string vnp_TmnCode = "RSS5QRAC";
        public const string vnp_HashSecret = "KSPHIBBGESKEZTZTFDJMVXJKAVASTPGC";
        public const string vnp_apiUrl = "http://sandbox.vnpayment.vn/merchant_webapi/merchant.html";
        public const string vnp_Version = "2.0.0";
        public const string vnp_Command = "pay";
    }
}
