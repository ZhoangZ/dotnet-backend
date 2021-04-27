using BackendDotnetCore.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    [Table("payment")]
    public class PaymentEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("ip_address")]
        public string ipAddress { get; set; }

        [Column("currcode")]
        public string CurrCode { get; set; }

        [Column("create_time")]
        public DateTime CreateTime { get; set; }

        [Column("amount")]
        public long Amount { get; set; }

        [Column("transaction_Status")]
        public string TransactionStatus { get; set; }

        [Column("url_pay")]
        public string urlPay { get; set; }

        [Column("url_status")]
        public string UrlStatus { get; set; }

        public string gender()
        {
            String vnp_IpAddr = "119.17.249.22";
            Dictionary<string, string> vnp_Params = new Dictionary<string, string>();
            vnp_Params["vnp_Version"] = VNPayConfig.vnp_Version;
            vnp_Params["vnp_Command"] = VNPayConfig.vnp_Command;
            vnp_Params["vnp_TmnCode"] = VNPayConfig.vnp_TmnCode;
            vnp_Params["vnp_Amount"] = 1000000 + "";
            vnp_Params["vnp_CurrCode"] = "VND";
            vnp_Params["vnp_IpAddr"] = vnp_IpAddr;
            vnp_Params["vnp_Locale"] = "vn";
            vnp_Params["vnp_OrderInfo"] = "dotnet";
            vnp_Params["vnp_ReturnUrl"] = "www.localhost.com";
            vnp_Params["vnp_TxnRef"] = "1";
            this.CreateTime=new DateTime();
            vnp_Params["vnp_CreateDate"]= this.CreateTime.ToString("yyyyMMddHHmmss");

            ArrayList fieldNames = new ArrayList(vnp_Params.Keys);
            fieldNames.Sort();
           
            StringBuilder hashData = new StringBuilder();
            StringBuilder query = new StringBuilder();
            for (int i = 0; i < fieldNames.Count; ++i)
            {
                string fieldName =(string) fieldNames[i];
                string fieldValue = vnp_Params[fieldName];
                if ((fieldValue != null) && (fieldValue.Count() > 0))
                {
                    //Build hash data
                    hashData.Append(fieldName);
                    hashData.Append('=');
                    hashData.Append(fieldValue);
                    //Build query
                   /* query.Append(URLEncoder.encode(fieldName, StandardCharsets.US_ASCII.toString()));
                    query.Append('=');
                    query.Append(URLEncoder.encode(fieldValue, StandardCharsets.US_ASCII.toString()));
*/
                    query.Append(fieldName);
                    query.Append('=');
                    query.Append(fieldValue);

                    if (i!=fieldNames.Count-1)
                    {
                        query.Append('&');
                        hashData.Append('&');
                    }
                }
            }
           
                
            

            String queryUrl = query.ToString();
            String vnp_SecureHash = ComputeSha256Hash(VNPayConfig.vnp_HashSecret + hashData.ToString());
            queryUrl += "&vnp_SecureHashType=SHA256&vnp_SecureHash=" + vnp_SecureHash;
            String paymentUrl = VNPayConfig.vnp_PayUrl + "?" + queryUrl;
            this.urlPay = paymentUrl;
            return paymentUrl;
        }
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
