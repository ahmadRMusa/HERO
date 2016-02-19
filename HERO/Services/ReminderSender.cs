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
        private GymContext _gymContext;
        private IEmailSender _emailSender;

        [Inject]
        public ReminderSender(GymContext gymContext, IEmailSender emailSender)
        {
            _gymContext = gymContext;
            _emailSender = emailSender;
        }

        public async void Execute(IJobExecutionContext context)
        {
            var athletes = _gymContext.Athletes.ToList();

            foreach (var athlete in athletes)
            {
                foreach(var cls in athlete.Reminders.Reminders)
                {
                    if (cls.Time - DateTime.Now <= new TimeSpan(24))
                    {
                        SendClassReminder(athlete.EmailAddress, cls);
                        athlete.Reminders.Reminders.Remove(cls);
                    }
                }
            }

            await _gymContext.SaveChangesAsync();
        }

        public async void SendClassReminder(string email, Class cls)
        {
            string subject = String.Format("Don't Forget to Signup for {0}!", cls.Type);
            string message = String.Format("You still haven't signed up for your {0} class on {1}!", cls.Type, cls.Time.Value.ToString("MM/dd/yyyy"));
            await _emailSender.SendEmailAsync(email, subject, message);
        }
    }
}