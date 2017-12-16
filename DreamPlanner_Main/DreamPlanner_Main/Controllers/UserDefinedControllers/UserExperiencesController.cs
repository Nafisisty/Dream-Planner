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
            ViewBag.RatingId = new SelectList(db.Ratings, "RatingId", "RatingName");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            return View();
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
                return RedirectToAction("Index", "Home");
            }

            ViewBag.RatingId = new SelectList(db.Ratings, "RatingId", "RatingName", userExperience.RatingId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", userExperience.UserId);
            return View(userExperience);
        }
	}
}