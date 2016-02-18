using HERO.Models;
using HERO.Models.Objects;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HERO.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private GymContext db;

        public HomeController(GymContext context)
        {
            db = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Reminders()
        //{
        //    string userId = HttpContext.User.Identity.GetUserId();
        //    List<Reminder> reminders = GetMissingPerformanceReminders(userId);
        //    return View(reminders);
        //}

        //public List<Reminder> GetMissingPerformanceReminders(string userId)
        //{
        //    int athleteId = db.Athletes.Select(a => new { Id = a.Id, appId = a.ApplicationUserId }).Single(c => c.appId.Equals(userId)).Id;
        //    var classData = db.Classes.AsNoTracking().Select(c => new { Id = c.Id, Type = c.Type, Date = c.Time, Performances = c.Performances, Attendance = c.Attendance })
        //        .Where(p => p.Attendance.Select(a => a.Id).Contains(athleteId) && p.Date.Value <= DateTime.Now && !p.Performances.Select(d => d.Athlete.Id).Contains(athleteId)).Select(a => new { Id = a.Id, Type = a.Type, Date = a.Date } );
        //    List<Reminder> reminders = new List<Reminder>();
        //    foreach(var item in classData)
        //    {
        //        reminders.Add(new Reminder
        //        {
        //            Type = ReminderType.RecordWod,
        //            ClickId = item.Id,
        //            Date = item.Date,
        //            Message = String.Format("Remember to record your performance for {0} on {1}.", item.Type, item.Date.Value.ToString("MM/dd/yyyy"))
        //        });
        //    };
        //    return reminders;
        //}
    }
}