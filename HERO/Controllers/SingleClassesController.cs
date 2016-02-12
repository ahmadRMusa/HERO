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
    public class SingleClassesController : Controller
    {
        private GymContext db;

        public SingleClassesController(GymContext context)
        {
            db = context;
        }

        // GET: SingleClasses
        public async Task<ActionResult> Index()
        {
            return View(await db.SingleClasses.ToListAsync());
        }

        // GET: SingleClasses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SingleClass singleClass = await db.SingleClasses.FindAsync(id);
            if (singleClass == null)
            {
                return HttpNotFound();
            }
            return View(singleClass);
        }

        // GET: SingleClasses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SingleClasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Duration,MaxAttendance,Date")] SingleClass singleClass)
        {
            if (ModelState.IsValid)
            {
                db.SingleClasses.Add(singleClass);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(singleClass);
        }

        // GET: SingleClasses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SingleClass singleClass = await db.SingleClasses.FindAsync(id);
            if (singleClass == null)
            {
                return HttpNotFound();
            }
            return View(singleClass);
        }

        // POST: SingleClasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Duration,MaxAttendance,Date")] SingleClass singleClass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(singleClass).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(singleClass);
        }

        // GET: SingleClasses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SingleClass singleClass = await db.SingleClasses.FindAsync(id);
            if (singleClass == null)
            {
                return HttpNotFound();
            }
            return View(singleClass);
        }

        // POST: SingleClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SingleClass singleClass = await db.SingleClasses.FindAsync(id);
            db.SingleClasses.Remove(singleClass);
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
