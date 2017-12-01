using DreamPlanner_Main.Models;
using DreamPlanner_Main.Models.UserDefinedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DreamPlanner_Main.Controllers.UserDefinedControllers
{
    public class AuthenticationController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Authentication
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LogInModel logInModel)
        {
            var user = db.Users.ToList().FirstOrDefault(u => u.UserName == logInModel.UserName && u.UserPassword == logInModel.Password);
            if(user != null)
            {
                Authentication.UserId = user.UserId;
                Authentication.UserName = user.UserName;
                Authentication.UserStatus = true;
                Authentication.IsAuthenticated = true;

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("LogIn", "Authentication");
        }

        public ActionResult LogOff()
        {
            Authentication.UserId = 0;
            Authentication.UserName = "public";
            Authentication.UserStatus = false;
            Authentication.IsAuthenticated = false;

            return RedirectToAction("Index", "Home");
        }

    }
}