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
    public class LoanxController : Controller
    {
        private LaitehallintaEntities db = new LaitehallintaEntities();

        // GET: Loanx
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {

                var loans = db.Loans.Include(l => l.Location).Include(l => l.Product).Include(l => l.Specification).Include(l => l.User);
            return View(loans.ToList());

            }
            else return RedirectToAction("Index", "Home");

        }

        // GET: Loanx/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserName"] != null)
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);

            }
            else return RedirectToAction("Index", "Home");

        }

        // GET: Loanx/Create
        public ActionResult Create()
        {
            if (Session["UserName"] != null)
            {
            ViewBag.product_group_id = new SelectList(db.Productgroups, "product_group_id", "product_group_name");
            ViewBag.location_id = new SelectList(db.Locations, "location_id", "location_name");
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_name");
            ViewBag.spec_id = new SelectList(db.Specifications, "spec_id", "loan_spec");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username");
            ViewBag.status = new SelectList(db.Loans, "load_id", "status");
            
            return View();
                 
            }
            else return RedirectToAction("Index", "Home");

        }

        // POST: Loanx/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "loan_id,user_id,location_id,product_id,loaned_date,spec_id,status")] Loan loan)
        {
            if (Session["UserName"] != null)
            {

                if (ModelState.IsValid)
            {
                db.Loans.Add(loan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_group_id = new SelectList(db.Productgroups, "product_group_id", "product_group_name");
            ViewBag.location_id = new SelectList(db.Locations, "location_id", "location_name", loan.location_id);
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_name", loan.product_id);
            ViewBag.spec_id = new SelectList(db.Specifications, "spec_id", "loan_spec", loan.spec_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", loan.user_id);
            ViewBag.status = new SelectList(db.Loans, "load_id", "status");

            return View(loan);

            }
            else return RedirectToAction("Index", "Home");

        }

        // GET: Loanx/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserName"] != null)
            {

                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            ViewBag.location_id = new SelectList(db.Locations, "location_id", "location_name", loan.location_id);
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_name", loan.product_id);
            ViewBag.spec_id = new SelectList(db.Specifications, "spec_id", "loan_spec", loan.spec_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "email", loan.user_id);
            return View(loan);

            }
            else return RedirectToAction("Index", "Home");

        }

        // POST: Loanx/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "loan_id,user_id,location_id,product_id,loaned_date,spec_id,status")] Loan loan)
        {
            if (Session["UserName"] != null)
            {

                if (ModelState.IsValid)
            {
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.location_id = new SelectList(db.Locations, "location_id", "location_name", loan.location_id);
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_name", loan.product_id);
            ViewBag.spec_id = new SelectList(db.Specifications, "spec_id", "loan_spec", loan.spec_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "email", loan.user_id);
            return View(loan);

            }
            else return RedirectToAction("Index", "Home");

        }

        // GET: Loanx/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserName"] != null)
            {

                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);

            }
            else return RedirectToAction("Index", "Home");

        }

        // POST: Loanx/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserName"] != null)
            {

            Loan loan = db.Loans.Find(id);
            db.Loans.Remove(loan);
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
