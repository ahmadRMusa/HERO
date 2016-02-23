using HERO.Models;
using HERO.Models.Objects;
using Ninject;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HERO.Services
{
    public class ReminderSender : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            GymContext db = new GymContext();

            var athletes = db.Athletes.ToList();

            foreach (var athlete in athletes)
            {
                #region Class Reminders
                TimeSpan fullDay = new TimeSpan(1, 0, 0, 0, 0);
                TimeSpan noTime = new TimeSpan(0, 0, 0);

                List<Class> classesToRemind = athlete.Reminders.Reminders.Where(c => c.Time - DateTime.Now <= fullDay && c.Time - DateTime.Now >= noTime).ToList();

                foreach(var cls in classesToRemind)
                {
                    SendClassReminder(athlete.EmailAddress, cls);
                    athlete.Reminders.Reminders.Remove(cls);
                }

                db.SaveChanges();
                #endregion
            }
        }

        public async void SendClassReminder(string email, Class cls)
        {
            SmtpMessageSender sender = new SmtpMessageSender();
            string subject = String.Format("Don't Forget to Signup for {0}!", cls.Type);
            string greeting = String.Format("<h1>Hey there!</h1>");
            string body = String.Format("<p>You still haven't signed up for your {0} class on {1}!</p>", cls.Type, cls.Time.Value.ToString("MM/dd/yyyy"));
            string link = String.Format(@"<p><a href = ""http://localhost:11700/Classes/Details?Id={0}"">Click here to signup!</a></p>", cls.Id);

            string message = greeting + body + link;

            await sender.SendEmailAsync(email, subject, message);
        }
    }
}