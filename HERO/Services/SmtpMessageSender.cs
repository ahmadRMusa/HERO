using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace HERO.Services
{
    public class SmtpMessageSender : IEmailSender
    {
        public string EmailUserName { get; set; }
        public string EmailPassword { get; set; }

        public SmtpMessageSender()
        {
            EmailUserName = ConfigurationManager.AppSettings["EmailAddress"];
            EmailPassword = ConfigurationManager.AppSettings["EmailPassword"];
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(email));
            message.From = new MailAddress("alexmorask@gmail.com");
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                smtp.Credentials = new NetworkCredential
                {
                    UserName = EmailUserName,
                    Password = EmailPassword
                };

                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                await smtp.SendMailAsync(message);
            }
        }
    }
}