using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DreamPlanner_Main.Models;
using DreamPlanner_Main.Models.UserDefinedModels;

namespace DreamPlanner_Main.Controllers.UserDefinedControllers
{
    public class UserExperiencesController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: UserExperiences
        public ActionResult Index()
        {
            var userExperiences = db.UserExperiences.Include(u => u.Rating).Include(u => u.User);
            return View(userExperiences.ToList());
        }

        // GET: UserExperiences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserExperience userExperience = db.UserExperiences.Find(id);
            if (userExperience == null)
            {
                return HttpNotFound();
            }
            return View(userExperience);
        }

        // GET: UserExperiences/Create
        public ActionResult Create()
        {
            ViewBag.Message = "";
            if (Authentication.IsAuthenticated)
            {
                var userExperience = db.UserExperiences.Find(Authentication.UserId);
                if(userExperience == null)
                {
                    ViewBag.RatingId = new SelectList(db.Ratings, "RatingId", "RatingName");
                    ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
                    return View();
                }
                else
                {
                    ViewBag.Message = "You have already shared your experience once. Thank you.";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("LogIn", "Authentication");
            }
        }

        // POST: UserExperiences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserExperienceId,RatingId,ExperienceDescription")] UserExperience userExperience)
        {
            if (ModelState.IsValid)
            {                
                userExperience.UserId = Authentication.UserId;
                db.UserExperiences.Add(userExperience);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.RatingId = new SelectList(db.Ratings, "RatingId", "RatingName", userExperience.RatingId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", userExperience.UserId);
            return View(userExperience);
        }

        // GET: UserExperiences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserExperience userExperience = db.UserExperiences.Find(id);
            if (userExperience == null)
            {
                return HttpNotFound();
            }
            ViewBag.RatingId = new SelectList(db.Ratings, "RatingId", "RatingName", userExperience.RatingId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", userExperience.UserId);
            return View(userExperience);
        }

        // POST: UserExperiences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserExperienceId,RatingId,ExperienceDescription,UserId")] UserExperience userExperience)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userExperience).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RatingId = new SelectList(db.Ratings, "RatingId", "RatingName", userExperience.RatingId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", userExperience.UserId);
            return View(userExperience);
        }

        // GET: UserExperiences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserExperience userExperience = db.UserExperiences.Find(id);
            if (userExperience == null)
            {
                return HttpNotFound();
            }
            return View(userExperience);
        }

        // POST: UserExperiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserExperience userExperience = db.UserExperiences.Find(id);
            db.UserExperiences.Remove(userExperience);
            db.SaveChanges();
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
