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
        public ActionResult Create()
        {
            return View();
        }

        // POST: WOD/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Scoring,Description")] WOD wod)
        {
            if (ModelState.IsValid)
            {
                db.WODs.Add(wod);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(wod);
        }

        // GET: WOD/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

        // POST: WOD/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Scoring,Description")] WOD wOD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wOD).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(wOD);
        }

        // GET: WOD/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: WOD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WOD wOD = await db.WODs.FindAsync(id);
            db.WODs.Remove(wOD);
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
