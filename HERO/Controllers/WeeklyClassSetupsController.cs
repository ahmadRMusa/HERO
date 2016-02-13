using HERO.Constants;
using HERO.Models;
using HERO.Models.Objects;
using HERO.Scheduler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HERO.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WeeklyClassesController : Controller
    {
        private GymContext db;
        private ICalendarGenerator _calendarGenerator;

        public WeeklyClassesController(GymContext context, ICalendarGenerator calendarGenerator)
        {
            db = context;
            _calendarGenerator = calendarGenerator;
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

            IEnumerable<string> items = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Select(d => d.ToString()).ToList();

            ViewBag.Days = new MultiSelectList(items);

            WeeklyClassSetup weeklyClass = await db.WeeklyClasses.FindAsync(id);
            if (weeklyClass == null)
            {
                return HttpNotFound();
            }
            return View(weeklyClass);
        }

        // GET: WeeklyClasses/Create
        public ActionResult Create()
        {
            IEnumerable<string> items = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Select(d => d.ToString()).ToList();

            ViewBag.Days = new MultiSelectList(items);

            return View();
        }

        // POST: WeeklyClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Time,Duration,Type,MaxAttendance,StartDate,EndDate,SelectedDays")] WeeklyClassSetup weeklyClass)
        {
            List<DayOfWeek> chosenDays = weeklyClass.SelectedDays.Select(x => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), x)).ToList();
            List<DayOfWeekModel> days = db.DaysOfWeek.Where(d => chosenDays.Contains(d.Day)).ToList();
            weeklyClass.Days = days;

            // Create Classes
            WeeklySchedule weeklySchedule = new WeeklySchedule
            {
                TimeOfDay = weeklyClass.Time,
                SchedulingRange = new Period(weeklyClass.StartDate, weeklyClass.EndDate)
            };
            weeklySchedule.SetDays(chosenDays);
            var schedules = new List<Schedule>() { weeklySchedule };
            List<Class> classes = _calendarGenerator.GenerateCalendar(ConstantValues.calendarPeriod, schedules).ToList();

            foreach(var item in classes)
            {
                item.Duration = weeklyClass.Duration;
                item.MaxAttendance = weeklyClass.MaxAttendance;
                item.Type = weeklyClass.Type;
                item.WeeklyClass = weeklyClass;
                item.Attendance = new List<Athlete>();
            }

            weeklyClass.GeneratedClasses = classes;

            ModelState.Remove("Days");
            ModelState.Remove("GeneratedClasses");
            if (ModelState.IsValid)
            {
                db.WeeklyClasses.Add(weeklyClass);
                db.Classes.AddRange(classes);

                foreach (var day in days)
                {
                    day.Classes = classes.Where(x => x.Time.DayOfWeek.Equals(day.Day)).ToList();
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            } else
            {
                var errors = ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList();
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
            WeeklyClassSetup weeklyClass = await db.WeeklyClasses.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,Duration,MaxAttendance,StartDate,EndDate,SchedulingRange,TimeOfDay,Name")] WeeklyClassSetup weeklyClass)
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
            WeeklyClassSetup weeklyClass = await db.WeeklyClasses.FindAsync(id);
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
            WeeklyClassSetup weeklyClass = await db.WeeklyClasses.FindAsync(id);
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
