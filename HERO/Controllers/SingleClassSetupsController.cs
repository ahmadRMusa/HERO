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
using System.Data.Entity.Validation;

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
            singleClassSetup.GeneratedClass = new Class();
            ModelState.Remove("GeneratedClass");
            if (ModelState.IsValid)
            {
                db.SingleClasses.Add(singleClassSetup);

                try
                {
                    await db.SaveChangesAsync();
                } catch(DbEntityValidationException e)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = e.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,Time,Duration,Type,MaxAttendance")] SingleClassSetup singleClassSetup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(singleClassSetup).State = EntityState.Modified;
                await db.SaveChangesAsync();
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
