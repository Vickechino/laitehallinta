using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tuoterekisteri.Models;

namespace Tuoterekisteri.Controllers
{
    public class LocationsController : Controller
    {
        private LaitehallintaEntities db = new LaitehallintaEntities();


        // GET: Locations
        public ActionResult Index()
        {
            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                return View(db.Locations.ToList());
            }
            else return RedirectToAction("Index", "Home");
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
            }
            else return RedirectToAction("Index", "Home");
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "location_id,location_name,location_row2")] Location location)
        {
            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(location);
            }
            else return RedirectToAction("Index", "Home");
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
            }
            else return RedirectToAction("Index", "Home");
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "location_id,location_name,location_row2")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);

            }
            else return RedirectToAction("Index", "Home");
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
            db.SaveChanges();
            return RedirectToAction("Index");

            }
            else return RedirectToAction("Index", "Home");
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
