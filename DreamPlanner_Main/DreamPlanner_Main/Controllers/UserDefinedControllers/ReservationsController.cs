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
            ViewBag.HallId = new SelectList(db.Halls, "HallId", "HallName");
            ViewBag.PartyTypeId = new SelectList(db.PartyTypes, "PartyTypeId", "PartyTypeName");
            ViewBag.ThemeId = new SelectList(db.Themes.Where(t => t.PartyTypeId == 1).ToList(), "ThemeId", "ThemeName");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationId,ThemeId,HallId,ReservationDate,UserId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HallId = new SelectList(db.Halls, "HallId", "HallName", reservation.HallId);
            ViewBag.ThemeId = new SelectList(db.Themes, "ThemeId", "ThemeName", reservation.ThemeId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", reservation.UserId);
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
