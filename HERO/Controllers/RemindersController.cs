using HERO.Models;
using HERO.Models.Objects;
using HERO.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HERO.Controllers
{
    public class RemindersController : Controller
    {
        private GymContext db;

        public RemindersController(GymContext context)
        {
            db = context;
        }

        public ActionResult Performance()
        {
            string userId = HttpContext.User.Identity.GetUserId();
            int athleteId = db.Athletes.AsNoTracking().Select(a => new { Id = a.Id, AppId = a.ApplicationUserId }).Single(b => b.AppId.Equals(userId)).Id;

            List<Class> allClasses = db.Athletes.Single(a => a.Id.Equals(athleteId)).Classes.ToList();
            List<Class> pastClassesNoPerformance = allClasses.Where(c => !c.Performances.Select(p => p.AthleteId).Contains(athleteId) && c.Time < DateTime.Now).ToList();

            return View(pastClassesNoPerformance);
        }

        public JsonResult PerformanceReminderCount()
        {
            string userId = HttpContext.User.Identity.GetUserId();
            int athleteId = db.Athletes.AsNoTracking().Select(a => new { Id = a.Id, AppId = a.ApplicationUserId }).Single(b => b.AppId.Equals(userId)).Id;

            List<Class> classes = db.Athletes.Single(a => a.Id.Equals(athleteId)).Classes.ToList();
            classes = classes.Where(c => !c.Performances.Select(p => p.AthleteId).Contains(athleteId) && c.Time < DateTime.Now).ToList();

            return Json(classes.Count(), JsonRequestBehavior.AllowGet);
        }

        // GET: Reminders
        public ActionResult Class()
        {
            string userId = HttpContext.User.Identity.GetUserId();
            int athleteId = db.Athletes.AsNoTracking().Select(a => new { Id = a.Id, AppId = a.ApplicationUserId }).Single(b => b.AppId.Equals(userId)).Id;

            List<WeeklyClassSetup> classesWithReminders = db.WeeklyClasses.Where(w => w.AttachedReminders.Select(a => a.AthleteId).Contains(athleteId)).ToList();

            List<int> classesWithRemindersIds = classesWithReminders.Select(c => c.Id).ToList();

            List<WeeklyClassSetup> classesNoReminders = db.WeeklyClasses.Where(w => !classesWithRemindersIds.Contains(w.Id)).ToList();

            var model = new ManageRemindersViewModel
            {
                ClassesWithReminders = classesWithReminders,
                ClassesNoReminders = classesNoReminders
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRecurring(int weeklyId)
        {
            string userId = HttpContext.User.Identity.GetUserId();
            int athleteId = db.Athletes.AsNoTracking().Select(a => new { Id = a.Id, AppId = a.ApplicationUserId }).Single(b => b.AppId.Equals(userId)).Id;

            WeeklyClassSetup weeklyClassSetup = await db.WeeklyClasses.FindAsync(weeklyId);
            ClassReminders reminders = await db.ClassReminders.FindAsync(athleteId);

            List<Class> classes = db.Classes.Where(a => a.WeeklyClass.Id.Equals(weeklyId)).ToList();
            reminders.Reminders.RemoveAll(c => classes.Contains(c));

            foreach(var cls in classes)
            {
                cls.AttachedReminders.Remove(reminders);
            }

            reminders.WeeklyClassSetups.Remove(weeklyClassSetup);
            weeklyClassSetup.AttachedReminders.Remove(reminders);

            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { controller = "Reminders" });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRecurring(int weeklyId)
        {
            string userId = HttpContext.User.Identity.GetUserId();
            int athleteId = db.Athletes.AsNoTracking().Select(a => new { Id = a.Id, AppId = a.ApplicationUserId }).Single(b => b.AppId.Equals(userId)).Id;

            WeeklyClassSetup weeklyClassSetup = await db.WeeklyClasses.FindAsync(weeklyId);
            ClassReminders reminders = await db.ClassReminders.FindAsync(athleteId);

            List<Class> classes = db.Classes.Where(a => a.WeeklyClass.Id.Equals(weeklyId)).ToList();
            reminders.Reminders.AddRange(classes);

            foreach (var cls in classes)
            {
                cls.AttachedReminders.Add(reminders);
            }
                        
            reminders.WeeklyClassSetups.Add(weeklyClassSetup);
            weeklyClassSetup.AttachedReminders.Add(reminders);

            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { controller = "Reminders" });
        }
    }
}