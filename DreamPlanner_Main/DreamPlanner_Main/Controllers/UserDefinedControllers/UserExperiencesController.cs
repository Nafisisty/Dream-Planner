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
	}
}