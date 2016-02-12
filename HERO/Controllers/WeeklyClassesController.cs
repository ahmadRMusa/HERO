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

namespace HERO.Controllers
{
    public class WeeklyClassesController : Controller
    {
        private GymContext db;

        public WeeklyClassesController(GymContext context)
        {
            db = context;
        }

        // GET: WeeklyClasses
        public async Task<ActionResult> Index()
        {
            return View(await db.WeeklyClasses.ToListAsync());
        }

        // GET: WeeklyClasses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeeklyClass weeklyClass = await db.WeeklyClasses.FindAsync(id);
            if (weeklyClass == null)
            {
                return HttpNotFound();
            }
            return View(weeklyClass);
        }

        // GET: WeeklyClasses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WeeklyClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Duration,MaxAttendance,StartDate,EndDate,SchedulingRange,TimeOfDay,Name")] WeeklyClass weeklyClass)
        {
            if (ModelState.IsValid)
            {
                db.WeeklyClasses.Add(weeklyClass);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(weeklyClass);
        }

        // GET: WeeklyClasses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeeklyClass weeklyClass = await db.WeeklyClasses.FindAsync(id);
            if (weeklyClass == null)
            {
                return HttpNotFound();
            }
            return View(weeklyClass);
        }

        // POST: WeeklyClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Duration,MaxAttendance,StartDate,EndDate,SchedulingRange,TimeOfDay,Name")] WeeklyClass weeklyClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(weeklyClass).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(weeklyClass);
        }

        // GET: WeeklyClasses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WeeklyClass weeklyClass = await db.WeeklyClasses.FindAsync(id);
            if (weeklyClass == null)
            {
                return HttpNotFound();
            }
            return View(weeklyClass);
        }

        // POST: WeeklyClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WeeklyClass weeklyClass = await db.WeeklyClasses.FindAsync(id);
            db.WeeklyClasses.Remove(weeklyClass);
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
