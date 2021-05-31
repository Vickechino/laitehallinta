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
    public class ProductgroupsController : Controller
    {
        private LaitehallintaEntities db = new LaitehallintaEntities();

        // GET: Productgroups
        public ActionResult Index()
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                return View(db.Productgroups.ToList());

            }
            else return RedirectToAction("Index", "Home");
        }

        // GET: Productgroups/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {

                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productgroup productgroup = db.Productgroups.Find(id);
            if (productgroup == null)
            {
                return HttpNotFound();
            }
            return View(productgroup);

            }
            else return RedirectToAction("Index", "Home");
        }

        // GET: Productgroups/Create
        public ActionResult Create()
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                return View();

            }
            else return RedirectToAction("Index", "Home");
        }

        // POST: Productgroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_group_id,product_group_name")] Productgroup productgroup)
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                if (ModelState.IsValid)
            {
                db.Productgroups.Add(productgroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productgroup);

            }
            else return RedirectToAction("Index", "Home");
        }

        // GET: Productgroups/Edit/5
        public ActionResult Edit(int? id)
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productgroup productgroup = db.Productgroups.Find(id);
            if (productgroup == null)
            {
                return HttpNotFound();
            }
            return View(productgroup);

            }
            else return RedirectToAction("Index", "Home");
        }

        // POST: Productgroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_group_id,product_group_name")] Productgroup productgroup)
        {
            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                if (ModelState.IsValid)
            {
                db.Entry(productgroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productgroup);
            }
            else return RedirectToAction("Index", "Home");
        }

        // GET: Productgroups/Delete/5
        public ActionResult Delete(int? id)
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productgroup productgroup = db.Productgroups.Find(id);
            if (productgroup == null)
            {
                return HttpNotFound();
            }
            return View(productgroup);

            }
            else return RedirectToAction("Index", "Home");

        }

        // POST: Productgroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {

                Productgroup productgroup = db.Productgroups.Find(id);
            db.Productgroups.Remove(productgroup);
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
