using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceSystem.BLL.Services
{
   public class MailService
    {
        public MailService()
        {

        }
        public void SendEmail(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("facturatienet@gmail.com");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;

            NetworkCredential netCred = new NetworkCredential("facturatienet@gmail.com", "#Wachtwoord123");
            SmtpClient smtpobj = new SmtpClient("smtp.gmail.com", 587);
            smtpobj.UseDefaultCredentials = false;
            smtpobj.EnableSsl = true;
            smtpobj.Credentials = netCred;
            smtpobj.Send(mail);
        }
    }
}
