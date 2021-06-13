using BackendDotnetCore.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackendDotnetCore.Entities
{
    [Table("payment")]
    public class PaymentEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("ip_address")]
        public string IpAddress { get; set; }
        [Column("user_id")]
        public long userId { get; set; }

        [Column("currcode")]
        public string CurrCode { get; set; }

        [Column("create_time")]
        public DateTime CreateTime { get; set; }

        [Column("amount")]
        public Decimal Amount { get; set; }

        [Column("transaction_Status")]
        public string TransactionStatus { get; set; }

        [Column("url_pay")]
        public string UrlPay { get; set; }
        [Column("url_return")]
        public string UrlReturn { get; set; }

        [Column("url_status")]
        public string UrlStatus { get; set; }

        [Column("params_url_status")]
        public string ParamsUrlStatus { get; set; }

        public string gender( string returnUrl)
        {
            
            Dictionary<string, string> vnp_Params = new Dictionary<string, string>();
            vnp_Params["vnp_Version"] = VNPayConfig.vnp_Version;
            vnp_Params["vnp_Command"] = VNPayConfig.vnp_Command;
            vnp_Params["vnp_TmnCode"] = VNPayConfig.vnp_TmnCode;
            vnp_Params["vnp_Amount"] = this.Amount+"";
            vnp_Params["vnp_CurrCode"] = this.CurrCode;
            vnp_Params["vnp_IpAddr"] = this.IpAddress;
            vnp_Params["vnp_Locale"] = "vn";
            vnp_Params["vnp_OrderInfo"] = "dotnet";
            if (!returnUrl.EndsWith('/'))
            {
                returnUrl += "/" + this.Id;
            }
            else
            {
                returnUrl += this.Id;
            }

            vnp_Params["vnp_ReturnUrl"] = returnUrl;
            vnp_Params["vnp_TxnRef"] = this.Id+"";
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
            this.UrlPay = paymentUrl;
            return paymentUrl;
        } 
        public PaymentEntity querry(HttpClient httpClient)
        {
            Dictionary<string, string> vnp_Params = new Dictionary<string, string>();
            vnp_Params["vnp_Version"] = VNPayConfig.vnp_Version;
            vnp_Params["vnp_Command"] = "querydr";
            vnp_Params["vnp_TmnCode"] = VNPayConfig.vnp_TmnCode;
            vnp_Params["vnp_IpAddr"] = this.IpAddress;
            vnp_Params["vnp_OrderInfo"] = "dotnet "+ "Truy van luc "+ new DateTime().ToString("yyyyMMddHHmmss");
            vnp_Params["vnp_TxnRef"] = this.Id+"";
            vnp_Params["vnp_TransDate"] = this.CreateTime.ToString("yyyyMMddHHmmss");
            vnp_Params["vnp_CreateDate"]= new DateTime().ToString("yyyyMMddHHmmss");


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
            String paymentUrl = VNPayConfig.vnp_apiUrl + "?" + queryUrl;
            this.UrlStatus = paymentUrl;
            //Console.WriteLine(paymentUrl);


            string responseBody=CreateRequestGet(httpClient, paymentUrl).Result;
            Regex regex = new Regex("vnp_TransactionStatus=(?<vnp_TransactionStatus>\\d+)");
            Match match = regex.Match(responseBody);
            if (match.Success)
            {
                Console.WriteLine("vnp_TransactionStatus :" + match.Groups["vnp_TransactionStatus"]);
                this.ParamsUrlStatus = responseBody;
                if (match.Groups["vnp_TransactionStatus"].ToString().Equals("00")){
                    this.TransactionStatus = "SUCCESS";

                }

            }
            else
            {
                Console.WriteLine("Spam VNPAY");
               // this.ParamsUrlStatus = responseBody;
            }

            return this;
        }

        async Task<string> CreateRequestGet(HttpClient httpClient, string url)
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                //Console.WriteLine(responseBody);
                return responseBody;

                
                //string vnp_TransactionStatus= match.Groups["vnp_TransactionStatus"];
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return "";
            }
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
