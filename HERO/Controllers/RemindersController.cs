using System;
using HERO.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HERO.Models.Objects;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace HERO.Controllers
{
    public class RemindersController : Controller
    {
        private GymContext db;

        public RemindersController(GymContext context)
        {
            db = context;
        }

        // GET: Reminders
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reminders/AddRecurring
        public ActionResult AddRecurring()
        {
            return View(db.WeeklyClasses.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddRecurring(int weeklyId)
        {
            string userId = HttpContext.User.Identity.GetUserId();
            int athleteId = db.Athletes.AsNoTracking().Select(a => new { Id = a.Id, AppId = a.ApplicationUserId }).Single(b => b.AppId.Equals(userId)).Id;
            ClassReminders reminders = await db.ClassReminders.FindAsync(athleteId);

            if (reminders.Reminders == null)
            {
                reminders.Reminders = new List<Class>();
            }

            List<Class> classes = db.Classes.Where(a => a.WeeklyClass.Id.Equals(weeklyId)).ToList();
            reminders.Reminders.ToList().AddRange(classes);

            foreach(var cls in classes)
            {
                cls.AttachedReminders.Add(reminders);
            }

            await db.SaveChangesAsync();
            return RedirectToAction("AddRecurring", new { controller = "Reminders" });
        }
    }
}