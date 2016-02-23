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
    [Authorize(Roles = "Admin")]
    public class WODController : Controller
    {
        private GymContext db;

        public WODController(GymContext context)
        {
            db = context;
        }

        // GET: WOD
        public async Task<ActionResult> Index()
        {
            return View(await db.WODs.ToListAsync());
        }

        // GET: WOD/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WOD wOD = await db.WODs.FindAsync(id);
            if (wOD == null)
            {
                return HttpNotFound();
            }
            return View(wOD);
        }

        // GET: WOD/Create
        public ActionResult Create(int? classId)
        {
            if (classId == null)
            {
                AddWodViewModel model = new AddWodViewModel { WOD = null, ClassId = null };
                return View(model);
            } else
            {
                AddWodViewModel model = new AddWodViewModel { WOD = new WOD(), ClassId = classId };
                return View(model);
            }
        }

        // GET: WOD/AddToClass
        public ActionResult AddToClass(int classId)
        {
            string classType = db.Classes.Find(classId).Type;
            var model = new AddWODToClassViewModel { ClassId = classId, ClassType = classType };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddToClass(AddWODToClassViewModel model)
        {
            Class cls = db.Classes.Find(model.ClassId);
            try
            {
                WOD wod = db.WODs.Single(w => w.Name == model.WODName);
                cls.WOD = wod;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", new { controller = "Classes", id = model.ClassId });
            } catch
            {
                model.ClassType = cls.Type;
                ViewBag.Error = "No WOD was found by that name.";
                return View(model);
            }
        }

        public ActionResult AddToDates(string start, string end)
        {
            DateTime startDate = Utilities.Constants.GetDateTimeFromFullCalendar(start);
            DateTime endDate = Utilities.Constants.GetDateTimeFromFullCalendar(end);

            var model = new AddWODToDatesViewModel { StartDate = startDate, EndDate = endDate };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddToDates(AddWODToDatesViewModel model)
        {
            List<Class> classes = db.Classes.ToList().Where(c => c.Time.Value.Date >= model.StartDate.Date && c.Time.Value.Date <= model.EndDate.Date).ToList();
            WOD wod = await db.WODs.SingleAsync(w => w.Name.Equals(model.WODName));
            foreach(var cls in classes)
            {
                cls.WOD = wod;
            }
            await db.SaveChangesAsync();
            ViewBag.Success = true;
            return RedirectToAction("Index", new { controller = "Classes" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveWodFromClass(int? classId)
        {
            Class cls = await db.Classes.FindAsync(classId);
            cls.WOD = null;
            await db.SaveChangesAsync();
            return RedirectToAction("Details", new { controller = "Classes", id = classId } );
        }

        public JsonResult Autocomplete(string term)
        {
            List<string> items = db.WODs.Select(w => w.Name).ToList();
            IEnumerable<string> filteredItems = items.Where(item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0);
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        // POST: WOD/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [Bind(Include = "Id,Name,Scoring,Description")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create([Bind(Include = "WOD,ClassId")] AddWodViewModel model)
        {
            Class cls = null;

            if (model.ClassId == null)
            {
                if (ModelState.IsValid)
                {
                    db.WODs.Add(model.WOD);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", new { controller = "Classes" });
                } else
                {
                    return View(model);
                }
            } else
            {
                try
                {
                    cls = await db.Classes.FindAsync(model.ClassId);
                    if (ModelState.IsValid)
                    {
                        cls.WOD = model.WOD;
                        db.WODs.Add(model.WOD);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index", new { controller = "WOD" });
                    }
                    else
                    {
                        return View(model);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message + ": " + e.InnerException);
                }
            }
        }

        // POST: WOD/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Scoring,Description")] WOD wod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wod).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(wod);
        }

        // POST: WOD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WOD wod = await db.WODs.FindAsync(id);
            List<Class> classes = db.Classes.Where(c => c.WOD.Id.Equals(wod.Id)).ToList();

            foreach(var cls in classes)
            {
                cls.WOD = null;
            }

            db.WODs.Remove(wod);
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
