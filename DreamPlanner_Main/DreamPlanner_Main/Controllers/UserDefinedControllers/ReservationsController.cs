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
using System.Net.Mail;

namespace DreamPlanner_Main.Controllers.UserDefinedControllers
{
    public class ReservationsController : Controller
    {
        private ProjectDbContext db = new ProjectDbContext();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.Hall).Include(r => r.Theme).Include(r => r.User);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            if (Authentication.IsAuthenticated)
            {
                ViewBag.HallId = new SelectList(db.Halls, "HallId", "HallName");
                ViewBag.PartyTypeId = new SelectList(db.PartyTypes, "PartyTypeId", "PartyTypeName");
                ViewBag.ThemeId = new SelectList(db.Themes.Where(t => t.PartyTypeId == 1).ToList(), "ThemeId", "ThemeName");
                ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
                return View();
            }
            else
            {
                return RedirectToAction("LogIn", "Authentication");
            }
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationId,ThemeId,HallId,ReservationDate,UserId,ReservationCode,TotalRent")] Reservation reservation)
        {

            if (ModelState.IsValid)
            {
                if(IsReserved(reservation.HallId, reservation.ReservationDate))
                {
                    ViewBag.ReservationMesg = "The selected hall is already reserved in this date.";
                    ViewBag.HallId = new SelectList(db.Halls, "HallId", "HallName", reservation.HallId);
                    ViewBag.PartyTypeId = new SelectList(db.PartyTypes, "PartyTypeId", "PartyTypeName");
                    ViewBag.ThemeId = new SelectList(db.Themes, "ThemeId", "ThemeName", reservation.ThemeId);
                    ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", reservation.UserId);
                    return View(reservation);
                }
                else
                {
                    reservation.UserId = Authentication.UserId;
                    reservation.ReservationCode = GetReservationCode(reservation);
                    reservation.AdvancePayment = false;
                    reservation.IsPaid = false;

                    db.Reservations.Add(reservation);
                    db.SaveChanges();

                    SendEmail(reservation.ReservationCode);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.HallId = new SelectList(db.Halls, "HallId", "HallName", reservation.HallId);
            ViewBag.PartyTypeId = new SelectList(db.PartyTypes, "PartyTypeId", "PartyTypeName");
            ViewBag.ThemeId = new SelectList(db.Themes, "ThemeId", "ThemeName", reservation.ThemeId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", reservation.UserId);
            ViewBag.ReservationMesg = "";
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.HallId = new SelectList(db.Halls, "HallId", "HallName", reservation.HallId);
            ViewBag.ThemeId = new SelectList(db.Themes, "ThemeId", "ThemeName", reservation.ThemeId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", reservation.UserId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationId,ThemeId,HallId,ReservationDate,UserId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HallId = new SelectList(db.Halls, "HallId", "HallName", reservation.HallId);
            ViewBag.ThemeId = new SelectList(db.Themes, "ThemeId", "ThemeName", reservation.ThemeId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", reservation.UserId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetThemeByPartyType(int partyTypeId)
        {
            var themeList = db.Themes.Where(t => t.PartyTypeId == partyTypeId).ToList();
            return Json(themeList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserInformation()
        {
            var user = db.Users.Find(Authentication.UserId);
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalculateTotalRent(int partyTypeId, int themeId, int hallId)
        {
            int totalRent = 0;

            switch(partyTypeId)
            {
                case 1: totalRent = totalRent + 10000;
                    break;
                case 2: totalRent = totalRent + 8000;
                    break;
                case 3: totalRent = totalRent + 5000;
                    break;     
            }

            switch(hallId)
            {
                case 1: totalRent = totalRent + 25000;
                    break;
                case 2: totalRent = totalRent + 40000;
                    break;
                case 3: totalRent = totalRent + 50000;
                    break;
            }

            totalRent = totalRent + 5000;

            return Json(totalRent, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReservedDates(int hallId)
        {
            var dateList = db.Reservations.Where(r => r.HallId == hallId).ToList();
            var dateArray = dateList.Select(d => d.ReservationDate.ToShortDateString()).ToArray();
            return Json(dateArray, JsonRequestBehavior.AllowGet);
        }


        public bool IsReserved(int hallId, DateTime date)
        {
            var reservation = db.Reservations.FirstOrDefault(r => r.HallId == hallId && r.ReservationDate == date);

            if(reservation != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GetReservationCode(Reservation reservation)
        {
            Random rnd = new Random();
            string reservationCode = rnd.Next(1, 999) + reservation.UserId + "" + reservation.ReservationDate.Day + "" + reservation.ReservationDate.Year + "" + reservation.ReservationDate.Month + "" + DateTime.Now.Second + "" + DateTime.Now.Minute + "" + DateTime.Now.Hour + "" + reservation.ThemeId + "" + reservation.HallId ;
            return reservationCode;
        }

        private void SendEmail(string code)
        {
            var user = db.Users.Find(Authentication.UserId);
            MailMessage mailMessage = new MailMessage("dreamplanner.bth@gmail.com", user.UserEmail);
            try
            {
                mailMessage.Subject = "Reservation Code!!!!";
                mailMessage.Body = "Congratulation!!!,\n\nYour reservation is successful!!! Please pay 50% advance within a week. Otherwise, your happiness won't last. Please bring this reservation code: '" + code + "' while payment.";
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new NetworkCredential()
                {
                    UserName = "dreamplanner.bth@gmail.com",
                    Password = "dream12345"
                };
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                Response.Write("E-mail sent!");
            }
            catch(Exception ex)
            {
                Response.Write("Could not send the e-mail - error: " + ex.Message);
            }
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
