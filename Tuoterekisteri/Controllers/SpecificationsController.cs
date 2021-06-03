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
    public class SpecificationsController : Controller
    {
        private LaitehallintaEntities db = new LaitehallintaEntities();

        // GET: Specifications
        public ActionResult Index()
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {

                return View(db.Specifications.ToList());

            }
            else return RedirectToAction("Index", "Home");

        }

        // GET: Specifications/Details/5
        public ActionResult Details(int? id)
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specification specification = db.Specifications.Find(id);
            if (specification == null)
            {
                return HttpNotFound();
            }
            return View(specification);

            }
            else return RedirectToAction("Index", "Home");

        }

        // GET: Specifications/Create
        public ActionResult Create()
        {
            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                return View();
            }
            else return RedirectToAction("Index", "Home");

        }

        // POST: Specifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "spec_id,loan_spec")] Specification specification)
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                if (ModelState.IsValid)
            {
                db.Specifications.Add(specification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(specification);

            }
            else return RedirectToAction("Index", "Home");

        }

        // GET: Specifications/Edit/5
        public ActionResult Edit(int? id)
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specification specification = db.Specifications.Find(id);
            if (specification == null)
            {
                return HttpNotFound();
            }
            return View(specification);

            }
            else return RedirectToAction("Index", "Home");

        }

        // POST: Specifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "spec_id,loan_spec")] Specification specification)
        {
            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                if (ModelState.IsValid)
            {
                db.Entry(specification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(specification);

            }
            else return RedirectToAction("Index", "Home");

        }

        // GET: Specifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specification specification = db.Specifications.Find(id);
            if (specification == null)
            {
                return HttpNotFound();
            }
            return View(specification);

            }
            else return RedirectToAction("Index", "Home");

        }

        // POST: Specifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                Specification specification = db.Specifications.Find(id);
            db.Specifications.Remove(specification);
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
