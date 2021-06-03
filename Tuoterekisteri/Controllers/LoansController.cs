using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tuoterekisteri.Models;

namespace Tuoterekisteri.Controllers
{
    public class LoansController : Controller
    {
        LaitehallintaEntities db = new LaitehallintaEntities();
        // GET: Loans
        public ActionResult Index()
        {
            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                try
                {
                    List<Loan> model = db.Loans.ToList();
                    db.Dispose();
                    return View(model);
                }
                catch (Exception)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    throw;
                }
                finally { db.Dispose(); }
            }
            else return RedirectToAction("login", "Users");
        }
        public ActionResult Create()
        {
            if (Session["UserName"] != null && Session["Permission"].ToString() == "1")
            {
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "user_id, location_id, product_id, loaned_date, spec_id, status")] Loan newLoan)
        {
            if (ModelState.IsValid && Session["UserName"] != null && Session["Permission"].ToString() == "1")
            {
                try
                {
                    db.Loans.Add(newLoan);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.CreateLoanError = T.txt[26, L.nr];
                    return View();
                }

            }
            return View(User);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Loan loan = db.Loans.Find(id);
            if (loan == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (Session["UserName"] != null && Session["Permission"].ToString() == "1")
            {
                return View(loan);
            }
            else return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserName"] != null && Session["Permission"].ToString() == "1")
            {
                try
                {
                    Loan loan = db.Loans.Find(id);

                    db.Loans.Remove(loan);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.DeleteUserError = T.txt[27, L.nr];
                    return View();
                }

            }
            else return RedirectToAction("Index");
        }
    }
    }
