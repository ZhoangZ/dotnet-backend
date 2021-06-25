using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BackendDotnetCore.Helpers
{
    public class SendMailService
    {

        public static bool SendEmail(string subject, string toEmail, string body)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                string SystemMail = "ongdinh1099@gmail.com";
                message.From = new MailAddress("ongdinh1099@gmail.com");
                message.To.Add(new MailAddress(toEmail));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(SystemMail, "ongdinh101199");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);

                return true;
            }
            catch (Exception) { return false; }
        }

    }
}
