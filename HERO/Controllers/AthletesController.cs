using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HERO.Models;
using HERO.Utilities;
using HERO.Models.Objects;
using HERO.Services;
using Microsoft.AspNet.Identity;

namespace HERO.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AthletesController : Controller
    {
        private GymContext _db;
        private UserManager<ApplicationUser> _userManager;
        private IEmailSender _emailSender;

        public AthletesController(GymContext db, IEmailSender emailSender, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        // GET: Athletes
        public async Task<ActionResult> Index()
        {
            return View(await _db.Athletes.ToListAsync());
        }

        // GET: Athletes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Athlete athlete = await _db.Athletes.FindAsync(id);

            if (athlete == null)
            {
                return HttpNotFound();
            }

            List<Subscription> subscriptions = _db.Subscriptions.ToList();

            ViewBag.SubscriptionLength = new SelectList(
                    Utilities.Constants.SubscriptionLengthOptions.Select(x => new { text = x.Key, value = x.Value }),
                    "value",
                    "text");

            ViewBag.SubscriptionId = new SelectList(
                    subscriptions.Select(x => new { text = x.Name, value = x.Id }),
                    "value",
                    "text");

            return View(athlete);
        }

        // GET: Athletes/Create
        public ActionResult Create()
        {
            List<Subscription> subscriptions = _db.Subscriptions.ToList();

            ViewBag.SubscriptionLength = new SelectList(
                    Utilities.Constants.SubscriptionLengthOptions.Select(x => new { text = x.Key, value = x.Value } ),
                    "value",
                    "text");

            ViewBag.SubscriptionId = new SelectList(
                    subscriptions.Select(x => new { text = x.Name, value = x.Id } ),
                    "value",
                    "text");

            return View();
        }

        // POST: Athletes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FirstName,LastName,EmailAddress,BirthDate,Gender,SubscriptionLength,SubscriptionId")] AthleteViewModel model)
        {
            Athlete athlete = new Athlete
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                Gender = model.Gender,
                BirthDate = model.BirthDate,
                Subscription = _db.Subscriptions.Single(x => x.Id.Equals(model.SubscriptionId)),
                SubscriptionLength = model.SubscriptionLength,
            };

            athlete.Reminders = new ClassReminders
            {
                Athlete = athlete,
                AthleteId = athlete.Id,
                Reminders = new List<Class>()
            };

            if (athlete.EmailAddress == "alexmorask@gmail.com")
            {
                List<Class> pastClasses = _db.Classes.Where(c => c.Time <= DateTime.Now).ToList();
                athlete.Classes = pastClasses;
            }

            if (ModelState.IsValid)
            {
                _db.Athletes.Add(athlete);
                await _db.SaveChangesAsync();
                await BeginAthleteSetup(athlete);
                return RedirectToAction("Index");
            }

            return View(athlete);
        }

        // POST: Athletes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Age,Gender,SubscriptionLength")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(athlete).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(athlete);
        }

        // POST: Athletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Athlete athlete = await _db.Athletes.FindAsync(id);
            ClassReminders reminders = await _db.ClassReminders.FindAsync(athlete.Id);

            List<int> reminderClassIds = reminders.Reminders.Select(r => r.Id).ToList();
            List<Class> classes = _db.Classes.Where(c => reminderClassIds.Contains(c.Id)).ToList();
            foreach(var cls in classes)
            {
                cls.AttachedReminders.Remove(reminders);
            }

            _db.ClassReminders.Remove(reminders);
            _db.Athletes.Remove(athlete);

            var user = await _userManager.FindByIdAsync(athlete.ApplicationUserId);
            await _userManager.DeleteAsync(user);

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task BeginAthleteSetup(Athlete athlete)
        {
            Guid token = Guid.NewGuid();
            string emailBody = Utilities.Constants.GetEmailBody(athlete.FirstName, "CrossFit Example", token);
            await _emailSender.SendEmailAsync(athlete.EmailAddress, "Welcome to CrossFit Example!", emailBody);

            var keys = new AthleteSignupKey
            {
                Token = token.ToString(),
                Athlete = athlete
            };

            _db.AthleteSignupKeys.Add(keys);
            await _db.SaveChangesAsync();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
