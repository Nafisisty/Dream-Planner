using DreamPlanner_Main.Models;
using DreamPlanner_Main.Models.UserDefinedModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DreamPlanner_Main.Controllers
{
    public class HomeController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        public ActionResult Index()
        {
            var feedbackList = db.UserExperiences.Include(u => u.User).Where(u => u.RatingId == 3 || u.RatingId == 4).ToList();

            return View(feedbackList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Gallery()
        {
            return View();
        }

        public ActionResult Service()
        {
            return View();
        }
        public ActionResult Wedding()
        {
            return View();
        }
        public ActionResult Birthday()
        {
            return View();
        }
        public ActionResult Buddist()
        {
            return View();
        }
        public ActionResult Hindu()
        {
            return View();
        }
        public ActionResult Muslim()
        {
            return View();
        }
        public ActionResult Christian()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Contact(Contact contact)
        {
            ViewBag.ContactResult = " ";
            if (ModelState.IsValid)
            {
                //Insert into database
                db.Contacts.Add(contact);

                if (db.SaveChanges() > 0)
                {
                    Response.Write("<script>alert('Thank your for your message!');</script>");
                    return View();
                    //ViewBag.ContactResult = "Thank your for your message!";
                    //return View();
                }
                else
                {
                    Response.Write("<script>alert('Sorry,something wrong happened, please try it next time...');</script>");
                    //ViewBag.ContactResult = "Sorry,something wrong happened, please try it next time...";
                    return View();
                }
            }
            else
            {
                Response.Write("<script>alert('Sorry, please check your message');</script>");
                //ViewBag.ContactResult = "Sorry, please check your message";
                return View();
            }
        }
    }

}
      