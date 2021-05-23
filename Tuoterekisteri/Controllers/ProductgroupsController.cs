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
            return View(db.Productgroups.ToList());
        }

        // GET: Productgroups/Details/5
        public ActionResult Details(int? id)
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

        // GET: Productgroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productgroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_group_id,product_group_name")] Productgroup productgroup)
        {
            if (ModelState.IsValid)
            {
                db.Productgroups.Add(productgroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productgroup);
        }

        // GET: Productgroups/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Productgroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_group_id,product_group_name")] Productgroup productgroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productgroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productgroup);
        }

        // GET: Productgroups/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Productgroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Productgroup productgroup = db.Productgroups.Find(id);
            db.Productgroups.Remove(productgroup);
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
