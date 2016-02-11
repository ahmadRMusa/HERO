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
using HERO.Constants;
using HERO.Models.Objects;
using HERO.Services;

namespace HERO.Controllers
{
    public class AthletesController : Controller
    {
        private GymContext _db;
        private IEmailSender _emailSender;

        public AthletesController(GymContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
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
            return View(athlete);
        }

        // GET: Athletes/Create
        public ActionResult Create()
        {
            List<Subscription> subscriptions = _db.Subscriptions.ToList();

            ViewBag.SubscriptionLength = new SelectList(
                    ConstantValues.SubscriptionLengthOptions.Select(x => new { text = x.Key, value = x.Value } ),
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
                SubscriptionLength = model.SubscriptionLength
            };

            if (ModelState.IsValid)
            {
                _db.Athletes.Add(athlete);
                await _db.SaveChangesAsync();
                await BeginAthleteSetup(athlete);
                return RedirectToAction("Index");
            }

            return View(athlete);
        }

        public ActionResult Signup(string token)
        {
            AthleteSignupKey key = _db.AthleteSignupKeys.Include(m => m.Athlete).Single(t => t.Token.Equals(token));
            ViewBag.Email = key.Athlete.EmailAddress;
            ViewBag.Token = token;
            return View();
        }

        // GET: Athletes/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

        // GET: Athletes/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
            return View(athlete);
        }

        // POST: Athletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Athlete athlete = await _db.Athletes.FindAsync(id);
            _db.Athletes.Remove(athlete);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task BeginAthleteSetup(Athlete athlete)
        {
            Guid token = Guid.NewGuid();
            string emailBody = ConstantValues.GetEmailBody(athlete.FirstName, "CrossFit Example", token);
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
