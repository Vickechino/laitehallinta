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

        public ActionResult Loaned()
        {
            if (Session["UserName"] != null)
            {
                RedirectToAction("Loanx", "Loaned");

                return View();
            }
            else return RedirectToAction("Index", "Home");
        }

        public ActionResult Returned()
        {
            if (Session["UserName"] != null)
            {
                RedirectToAction("Loanx", "Returned");

                return View();
            }
            else return RedirectToAction("Index", "Home");
        }

       
        
               // GET: Loanx/Create
        public ActionResult Create(int? product_group_id)
        {
            if (Session["UserName"] != null)
            {

                //Query from Loans -table
                var loanQuery = (from l in db.Loans
                             where l.status == 1
                             select l).ToList();

            //Query all products
            var productsQuery = (from p in db.Products
                                 select p).ToList();

            //Comparing two lists and excluding all the loaned items into variable
            var notLoanedList = productsQuery.Where(s => !loanQuery.Any(p => p.product_id == s.product_id)).ToList();

            //If group has not been selected
            if (product_group_id == null)
            {
                ViewBag.product_id = new SelectList(notLoanedList, "product_id", "product_name");
            }
            //Else we filter selected group from the variable
            else
            {
                ViewBag.product_id = new SelectList(notLoanedList.Where(x => x.product_group_id == product_group_id), "product_id", "product_name");
            }

            ViewBag.product_group_id = new SelectList(db.Productgroups, "product_group_id", "product_group_name");
            ViewBag.location_id = new SelectList(db.Locations, "location_id", "location_name");
            ViewBag.spec_id = new SelectList(db.Specifications, "spec_id", "loan_spec");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username");
            ViewBag.status = new SelectList(db.Loans, "loan_id", "status");
                
            return View();

            }
            else return RedirectToAction("Index", "Home");
        }

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
                return RedirectToAction("Loaned");
            }
            ViewBag.product_group_id = new SelectList(db.Productgroups, "product_group_id", "product_group_name");
            ViewBag.location_id = new SelectList(db.Locations, "location_id", "location_name", loan.location_id);
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_name", loan.product_id);
            ViewBag.spec_id = new SelectList(db.Specifications, "spec_id", "loan_spec", loan.spec_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "username", loan.user_id);
            ViewBag.status = new SelectList(db.Loans, "loan_id", "status");

            return View(loan);
        }
            else return RedirectToAction("Index", "Home");
    }




        // GET: Loanx/Return
        public ActionResult Return(int? product_group_id)
        {
            if (Session["UserName"] != null)
            {
                ViewBag.product_group_id = new SelectList(db.Productgroups, "product_group_id", "product_group_name");
                ViewBag.product_id = new SelectList(db.Products, "product_id", "product_name");
                ViewBag.location_id = new SelectList(db.Locations, "location_id", "location_name");
                ViewBag.spec_id = new SelectList(db.Specifications, "spec_id", "loan_spec");
                ViewBag.user_id = new SelectList(db.Users, "user_id", "username");
                ViewBag.status = new SelectList(db.Loans, "loan_id", "status");

                return View();

            }
            else return RedirectToAction("Index", "Home");

        }

        // POST: Loanx/Return
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Return([Bind(Include = "loan_id,user_id,location_id,product_id,loaned_date,spec_id,status")] Loan loan)
        {
            if (Session["UserName"] != null)
            {

                if (ModelState.IsValid)
                {
                    db.Loans.Add(loan);
                    db.SaveChanges();
                    return RedirectToAction("Returned");
                }
                ViewBag.product_group_id = new SelectList(db.Productgroups, "product_group_id", "product_group_name");
                
                ViewBag.product_id = new SelectList(db.Products, "product_id", "product_name", loan.product_id);
                
                ViewBag.location_id = new SelectList(db.Locations, "location_id", "location_name", loan.location_id);
                ViewBag.spec_id = new SelectList(db.Specifications, "spec_id", "loan_spec", loan.spec_id);
                ViewBag.user_id = new SelectList(db.Users, "user_id", "username", loan.user_id);
                ViewBag.status = new SelectList(db.Loans, "load_id", "status");

                return View(loan);

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
