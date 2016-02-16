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
                return View();
            } else
            {
                AddWodViewModel model = new AddWodViewModel { WOD = new WOD(), ClassId = classId };
                return View(model);
            }
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
                        return RedirectToAction("Index", new { controller = "Classes" });
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
