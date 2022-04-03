using FunnySailAPI.ApplicationCore.Interfaces.InfrastructureServices;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace FunnySailAPI.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {

        }

        public bool SendEmail(string userEmail,string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("admin@funnysail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            //client.Credentials = new System.Net.NetworkCredential("care@yogihosting.com", "yourpassword");
            client.Host = "smtpout.secureserver.net";
            client.Port = 80;

            try
            {
                client.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }
    }
}
