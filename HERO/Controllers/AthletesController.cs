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

namespace HERO.Controllers
{
    public class AthletesController : Controller
    {
        // GET: Athletes
        public async Task<ActionResult> Index(GymContext db)
        {
            return View(await db.Athletes.ToListAsync());
        }

        // GET: Athletes/Details/5
        public async Task<ActionResult> Details(GymContext db, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlete = await db.Athletes.FindAsync(id);
            if (athlete == null)
            {
                return HttpNotFound();
            }
            return View(athlete);
        }

        // GET: Athletes/Create
        public ActionResult Create(GymContext db)
        {
            List<Subscription> subscriptions = db.Subscriptions.ToList();

            ViewBag.SubscriptionLength = new SelectList(
                    SubscriptionLenghts.SubscriptionLengthOptions.Select(x => new { text = x.Key, value = x.Value } ),
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
        public async Task<ActionResult> Create(GymContext db, [Bind(Include = "FirstName,LastName,BirthDate,Gender,SubscriptionLength,SubscriptionId")] AthleteViewModel model)
        {
            Athlete athlete = new Athlete
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                BirthDate = model.BirthDate,
                Subscription = db.Subscriptions.Single(x => x.Id.Equals(model.SubscriptionId)),
                SubscriptionLength = model.SubscriptionLength
            };

            if (ModelState.IsValid)
            {
                db.Athletes.Add(athlete);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(athlete);
        }

        // GET: Athletes/Edit/5
        public async Task<ActionResult> Edit(GymContext db, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlete = await db.Athletes.FindAsync(id);
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
        public async Task<ActionResult> Edit(GymContext db, [Bind(Include = "Id,FirstName,LastName,Age,Gender,SubscriptionLength")] Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                db.Entry(athlete).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(athlete);
        }

        // GET: Athletes/Delete/5
        public async Task<ActionResult> Delete(GymContext db, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Athlete athlete = await db.Athletes.FindAsync(id);
            if (athlete == null)
            {
                return HttpNotFound();
            }
            return View(athlete);
        }

        // POST: Athletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(GymContext db, int id)
        {
            Athlete athlete = await db.Athletes.FindAsync(id);
            db.Athletes.Remove(athlete);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            GymContext db = new GymContext();
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
