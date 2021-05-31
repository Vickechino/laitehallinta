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
    public class ProductsController : Controller
    {
        private LaitehallintaEntities db = new LaitehallintaEntities();

        // GET: Products
        public ActionResult Index()
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {

                var products = db.Products.Include(p => p.Productgroup);
            return View(products.ToList());


            }
            else return RedirectToAction("Index", "Home");

        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {


                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);

            }
            else return RedirectToAction("Index", "Home");

        }

        // GET: Products/Create
        public ActionResult Create()
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                ViewBag.product_group_id = new SelectList(db.Productgroups, "product_group_id", "product_group_name");
            return View();


            }
            else return RedirectToAction("Index", "Home");

        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,barcode,product_name,product_row2,product_group_id")] Product product)
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {



                if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_group_id = new SelectList(db.Productgroups, "product_group_id", "product_group_name", product.product_group_id);
            return View(product);

            }
            else return RedirectToAction("Index", "Home");

        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {

                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_group_id = new SelectList(db.Productgroups, "product_group_id", "product_group_name", product.product_group_id);
            return View(product);


            }
            else return RedirectToAction("Index", "Home");

        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,barcode,product_name,product_row2,product_group_id")] Product product)
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_group_id = new SelectList(db.Productgroups, "product_group_id", "product_group_name", product.product_group_id);
            return View(product);


            }
            else return RedirectToAction("Index", "Home");

        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {

                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);



            }
            else return RedirectToAction("Index", "Home");

        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {

                Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
