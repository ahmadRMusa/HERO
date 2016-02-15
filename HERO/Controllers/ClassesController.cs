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
    [Authorize]
    public class ClassesController : Controller
    {
        private GymContext db;

        public ClassesController(GymContext context)
        {
            db = context;
        }

        // GET: Classes
        public ActionResult Index()
        {
            return View();
        }

        // GET: Classes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = await db.Classes.FindAsync(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // GET: Classes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Time,Duration,Type,MaxAttendance")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Classes.Add(@class);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Time,Duration,Type,MaxAttendance")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@class).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Class @class = await db.Classes.FindAsync(id);
            db.Classes.Remove(@class);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<JsonResult> GetScheduledClasses(string start, string end)
        {
            DateTime startDate = Constants.ConstantValues.UnixTimestampToDateTime(Convert.ToDouble(start));
            DateTime endDate = Constants.ConstantValues.UnixTimestampToDateTime(Convert.ToDouble(end));

            List<Class> classes = await db.Classes.Where(x => x.Time >= startDate && x.Time <= endDate).ToListAsync();
            List<ClassJsonModel> jsonModel = classes.Select(x => new ClassJsonModel {
                id = x.Id,
                title = x.Type,
                editable = false,
                allDay = false,
                start = ((DateTime)x.Time).ToString("s"),
                end = ((DateTime)x.Time).AddHours(x.Duration).ToString("s"),
                url = Url.Action("Details", new { id = x.Id } )
            }).ToList();

            return Json(jsonModel, JsonRequestBehavior.AllowGet);
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
