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

        public async Task<ActionResult> UpcomingClasses()
        {
            string userId = HttpContext.User.Identity.GetUserId();
            try
            {
                Athlete athlete = await db.Athletes.SingleAsync(a => a.ApplicationUserId.Equals(userId));
                List<Class> classes = athlete.Classes.Where(c => c.Time >= DateTime.Now).ToList();
                return View(classes);
            } catch
            {
                ViewBag.Error = "Please log in with your athlete account to view your classes.";
                return View();
            }
        }

        public async Task<ActionResult> PastClasses()
        {
            string userId = HttpContext.User.Identity.GetUserId();
            try
            {
                Athlete athlete = await db.Athletes.SingleAsync(a => a.ApplicationUserId.Equals(userId));
                List<Class> classes = athlete.Classes.Where(c => c.Time < DateTime.Now).OrderByDescending(d => d.Time).ToList();
                PastClassesViewModel model = new PastClassesViewModel { AthleteId = athlete.Id, Classes = classes, Performances = athlete.Performances.ToList() };
                return View(model);
            }
            catch
            {
                ViewBag.Error = "Please log in with your athlete account to view your classes.";
                return View();
            }
        }

        // GET: Classes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string userId = HttpContext.User.Identity.GetUserId();

            try
            {
                Athlete athlete = await db.Athletes.SingleAsync(a => a.ApplicationUserId.Equals(userId));
                Class cls = await db.Classes.FindAsync(id);
                if (cls == null)
                {
                    return HttpNotFound();
                }

                ClassSignupViewModel model = new ClassSignupViewModel { Athlete = athlete, Class = cls };
                return View(model);
            }
            catch
            {
                Class cls = await db.Classes.FindAsync(id);
                if (cls == null)
                {
                    return HttpNotFound();
                }

                ClassSignupViewModel model = new ClassSignupViewModel { Class = cls };
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Signup(string userId, string classId)
        {
            if (userId == null || classId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int ClassId = Convert.ToInt32(classId);

            Athlete athlete = await db.Athletes.SingleAsync(a => a.ApplicationUserId.Equals(userId));
            Class cls = await db.Classes.SingleAsync(c => c.Id.Equals(ClassId));

            if (!cls.Attendance.Contains(athlete))
            {
                ViewData["SuccessHeader"] = String.Format("Success");
                ViewData["SuccessBody"] = String.Format("You were added to {0}.", cls.Type);
                cls.Attendance.Add(athlete);
                athlete.Classes.Add(cls);
                await db.SaveChangesAsync();
            } else
            {
                ViewData["SuccessHeader"] = String.Format("We've got you");
                ViewData["SuccessBody"] = String.Format("You've already been added to {0}.", cls.Type);
            }

            ClassSignupViewModel model = new ClassSignupViewModel { Athlete = athlete, Class = cls };
            return View("Details", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cancel(string userId, string classId)
        {
            if (userId == null || classId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int ClassId = Convert.ToInt32(classId);
            Athlete athlete = await db.Athletes.SingleAsync(a => a.ApplicationUserId.Equals(userId));
            Class cls = await db.Classes.SingleAsync(c => c.Id.Equals(ClassId));

            if (cls.Attendance.Contains(athlete))
            {
                ViewData["SuccessHeader"] = "Success";
                ViewData["SuccessBody"] = String.Format("We've removed you from {0}.", cls.Type);
                cls.Attendance.Remove(athlete);
                athlete.Classes.Remove(cls);
                await db.SaveChangesAsync();
            } else
            {
                ViewData["SuccessHeader"] = String.Format("Woops");
                ViewData["SuccessBody"] = String.Format("Looks like you weren't signed up for {0}.", cls.Type);
            }
            
            ClassSignupViewModel model = new ClassSignupViewModel { Athlete = athlete, Class = cls };
            return View("Details", model);
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
