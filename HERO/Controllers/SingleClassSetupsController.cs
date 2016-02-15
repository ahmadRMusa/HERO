using HERO.Constants;
using HERO.Models;
using HERO.Models.Objects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HERO.Controllers
{
    public class SingleClassSetupsController : Controller
    {
        private GymContext db;

        public SingleClassSetupsController(GymContext context)
        {
            db = context;
        }

        // GET: SingleClassSetups
        public async Task<ActionResult> Index()
        {
            return View(await db.SingleClasses.ToListAsync());
        }

        // GET: SingleClassSetups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SingleClassSetup singleClassSetup = await db.SingleClasses.FindAsync(id);
            if (singleClassSetup == null)
            {
                return HttpNotFound();
            }
            return View(singleClassSetup);
        }

        // GET: SingleClassSetups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SingleClassSetups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Date,Time,Duration,Type,MaxAttendance")] SingleClassSetup singleClassSetup)
        {
            Class c = new Class();
            c.MaxAttendance = singleClassSetup.MaxAttendance;
            c.Type = singleClassSetup.Type;
            c.Duration = singleClassSetup.Duration;
            c.DaysOfWeek = new List<DayOfWeekModel>() { db.DaysOfWeek.ToList().Single(d => d.Day == singleClassSetup.Date.Value.DayOfWeek) };
            c.Time = ((DateTime)singleClassSetup.Date).Add(singleClassSetup.Time);
            c.Attendance = new List<Athlete>();

            singleClassSetup.GeneratedClass = c;

            ModelState.Remove("GeneratedClass");
            if (ModelState.IsValid)
            {
                db.SingleClasses.Add(singleClassSetup);

                try
                {
                    await db.SaveChangesAsync();
                } catch(DbEntityValidationException e)
                {
                    ConstantValues.ThrowDetailedEntityValidationErrors(e);
                }

                return RedirectToAction("Index");
            }

            return View(singleClassSetup);
        }
        
        // POST: SingleClassSetups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ClassId,Date,Time,Duration,Type,MaxAttendance,GeneratedClass")] SingleClassSetup singleClassSetup)
        {
            Class cls = await db.Classes.SingleAsync(c => c.Id.Equals(singleClassSetup.ClassId));
            cls.MaxAttendance = singleClassSetup.MaxAttendance;
            cls.Type = singleClassSetup.Type;
            cls.Duration = singleClassSetup.Duration;
            cls.DaysOfWeek[0] = db.DaysOfWeek.ToList().Single(d => d.Day == singleClassSetup.Date.Value.DayOfWeek);
            cls.Time = singleClassSetup.Date;
            cls.Attendance = cls.Attendance;

            singleClassSetup.GeneratedClass = cls;

            ModelState.Remove("GeneratedClass");
            if (ModelState.IsValid)
            {
                db.Entry(singleClassSetup).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                } catch(DbEntityValidationException e)
                {
                    ConstantValues.ThrowDetailedEntityValidationErrors(e);
                }

                return RedirectToAction("Index");
            }
            return View(singleClassSetup);
        }

        // POST: SingleClassSetups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SingleClassSetup singleClassSetup = await db.SingleClasses.FindAsync(id);
            db.SingleClasses.Remove(singleClassSetup);
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
