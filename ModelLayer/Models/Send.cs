using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Models
{
    public  class Send
    {
        public string SendingMail(string emailTo, string token)
        {
            string emailfrom = "saikumarjntu227@gmail.com";
            MailMessage mail = new MailMessage(emailfrom, emailTo);
            string mailBody = "Token Generated: " + token;
            mail.Subject = "Generated token will expire after 1 hour";
            mail.Body = mailBody;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential credential = new NetworkCredential("saikumarjntu227@gmail.com", "xpxz ffqj vliw suns");

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credential;

            smtpClient.Send(mail);
            return emailTo;

        }
    }
}
