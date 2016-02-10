using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace HERO.Services
{
    public class AuthMessageSender : IEmailSender
    {
        public string SendGridUser { get; set; }
        public string SendGridPass { get; set; }

        public AuthMessageSender()
        {
            SendGridUser = ConfigurationManager.AppSettings["SendGridUser"];
            SendGridPass = ConfigurationManager.AppSettings["SendGridPass"];
        }

        public Task SendEmailAsync(string email, string subject, string body)
        {
            SendGridMessage message = new SendGridMessage();
            message.AddTo(email);
            message.From = new System.Net.Mail.MailAddress("admin@hero.com", "HERO CrossFit Management");
            message.Subject = subject;
            message.Html = body;

            NetworkCredential credentials = new NetworkCredential(SendGridUser, SendGridPass);

            Web transportWeb = new Web(credentials);

            if (transportWeb != null)
            {
                return transportWeb.DeliverAsync(message);
            } else
            {
                return Task.FromResult(0);
            }
        }
    }
}