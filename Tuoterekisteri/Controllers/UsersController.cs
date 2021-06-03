using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tuoterekisteri.Models;

namespace Tuoterekisteri.Controllers
{
    public class UsersController : Controller
    {
        LaitehallintaEntities db = new LaitehallintaEntities();
        // GET: Users
        public ActionResult Index()
        {

            if (Session["Permission"] != null && Session["Permission"].ToString() == "1")
            {
                List<User> model = db.Users.ToList();
                db.Dispose();
                return View(model);
            }
            else return RedirectToAction("login", "Users");
        }
        public ActionResult Login()
        {
            if (Session["username"] != null) { return RedirectToAction("Index", "Home"); }
            else return View();
        }
        [HttpPost]
        public ActionResult Authorize(User LoginModel)
        {
            var LoggedUser = db.Users.SingleOrDefault(x => x.username == LoginModel.username && x.password == LoginModel.password);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successful login";
                ViewBag.Loggedstatus = "In";
                Session["UserName"] = LoggedUser.username;
                Session["Permission"] = LoggedUser.admin;
                User user = db.Users.Find(LoggedUser.user_id);
                user.lastSeen = DateTime.Now;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();


                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.Loggedstatus = "Out";
                //LoginModel.LoginErrorMessage = "Väärä käyttäjätunnus/salasana";
                return View("Login", LoginModel);
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("Index", "Home");
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
        public ActionResult Create([Bind(Include = "username, password, email, firstName, lastName, admin")] User newUser)
        {
            if (ModelState.IsValid && Session["UserName"] != null && Session["Permission"].ToString() == "1")
            {
                try
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.CreateUserError = T.txt[26, L.nr];
                    return View();
                }

            }
            return View(User);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = db.Users.Find(id);
            if (user == null) return RedirectToAction("Index", "Home");
            if (Session["UserName"] != null && Session["Permission"].ToString() == "1")
            {
                return View(user);
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
                    User user = db.Users.Find(id);

                    db.Users.Remove(user);
                    db.SaveChanges();
                    if (Session["UserName"].ToString().ToLower() == user.username.ToString().ToLower()) { return RedirectToAction("Logout", "Users"); } //Should consider removing some casts
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
        public ActionResult Edit(int? id)
        {
            if (id == null) { return RedirectToAction("Index"); }
            try
            {
                if (Session["UserName"] == null) return RedirectToAction("Login", "Home");
                User user = db.Users.Find(id);
                if (user == null) RedirectToAction("Index", "Home");
                //ViewBag.RegionID = new SelectList(db.Region, "RegionID", "RegionDescription", X.RegionID);
                if (Session["UserName"] != null && Session["Permission"].ToString() == "1")
                {
                    return View(user);
                }
                else return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            finally { db.Dispose(); }
            

        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Katso https://go.microsoft.com/fwlink/?LinkId=317598
        public ActionResult Edit([Bind(Include = "username, password, email, firstName, lastName, admin, user_id")] User user)
        {
            if (ModelState.IsValid && (Session["UserName"] != null))
            {
                    try
                    {
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        ViewBag.CreateUserError = T.txt[26, L.nr];
                    return View();
                    }

            }
            return View(User);
        }
    }
}
