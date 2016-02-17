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
using HERO.Models.Objects;
using Microsoft.AspNet.Identity;
using System.Globalization;

namespace HERO.Controllers
{
    public class PerformancesController : Controller
    {
        private GymContext db = new GymContext();

        // GET: Performances
        public async Task<ActionResult> Index()
        {
            return View(await db.Performances.ToListAsync());
        }

        // GET: Performances/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = await db.Performances.FindAsync(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // GET: Performances/Create
        public ActionResult Create(int? classId)
        {
            Class cls = db.Classes.Find(classId);
            ViewData["Class"] = db.Classes.Find(classId);
            ViewData["WOD"] = cls.WOD;
            return View();
        }

        // POST: Performances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ScoreInput,Description,Prescribed")] Performance performance, int classId)
        {
            Task<Class> clsTask = db.Classes.FindAsync(classId);
            string userId = HttpContext.User.Identity.GetUserId();
            Athlete athlete = db.Athletes.Single(a => a.ApplicationUserId.Equals(userId));
            Class cls = await clsTask;

            switch(cls.WOD.Scoring)
            {
                case WODScoring.TotalReps:
                    performance.ScoreActual = Convert.ToDouble(performance.ScoreInput);
                    break;
                case WODScoring.TotalRounds:
                    performance.ScoreActual = Convert.ToDouble(performance.ScoreInput);
                    break;
                case WODScoring.TotalTime:
                    TimeSpan time;
                    if (!TimeSpan.TryParseExact(performance.ScoreInput, @"mm\:ss", CultureInfo.CurrentCulture, out time))
                    {
                        throw new Exception("TimeSpan parse failed.");
                    }
                    performance.ScoreActual = Convert.ToDouble(time.TotalSeconds);
                    break;
            }

            performance.Class = cls;
            performance.Athlete = athlete;
            performance.WOD = cls.WOD;

            ModelState.Remove("Description");
            ModelState.Remove("Prescribed");
            if (ModelState.IsValid)
            {
                db.Performances.Add(performance);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", new { controller = "Performances" } );
            } else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
                throw new Exception(String.Join(", ", errorMessages));
            }
        }

        // GET: Performances/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = await db.Performances.FindAsync(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // POST: Performances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Score")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(performance).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(performance);
        }

        // GET: Performances/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Performance performance = await db.Performances.FindAsync(id);
            if (performance == null)
            {
                return HttpNotFound();
            }
            return View(performance);
        }

        // POST: Performances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Performance performance = await db.Performances.FindAsync(id);
            db.Performances.Remove(performance);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
