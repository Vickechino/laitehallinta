using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tuoterekisteri.Models;
using System.Security.Cryptography;


namespace Tuoterekisteri.Controllers
{
    public class UsersController : Controller
    {
        LaitehallintaEntities db = new LaitehallintaEntities();
        // GET: Users
        public ActionResult Index() //Käyttäjälistan palautus
        {

            if (Session["UserName"] != null && Session["Permission"].ToString() == "1") //Varmistetaan kirjautuminen & oikeudet
            {
                List<User> model = db.Users.ToList();
                db.Dispose();
                return View(model);
            }
            else return RedirectToAction("login", "Users");
        }
        public ActionResult Login() //Login näkymän palautus
        {
            if (Session["username"] != null) { return RedirectToAction("Index", "Home"); } //Palautetaan login näkymä jos ei olla kirjauduttu
            else return View();
        }
        [HttpPost]
        public ActionResult Authorize([Bind(Include ="username, password")]User LoginModel) //Käyttäjän sisäänkirjautuminen 
        {
            try
            {


            if (LoginModel.username == null)
            {
                LoginModel.LoginErrorMessage = T.txt[45, L.nr];
                return View("Login", LoginModel);
            }
            var bpassword = System.Text.Encoding.UTF8.GetBytes(LoginModel.password);
            var hash = System.Security.Cryptography.MD5.Create().ComputeHash(bpassword);
            LoginModel.password = Convert.ToBase64String(hash);
            var LoggedUser = db.Users.SingleOrDefault(x => x.username == LoginModel.username && x.password == LoginModel.password);
            if (LoggedUser != null)
            {
                Session["UserName"] = LoggedUser.username;
                Session["Permission"] = LoggedUser.admin;
                Session["UserID"] = LoggedUser.user_id;  
                Session["firstName"] = LoggedUser.firstName;
                Session["lastName"] = LoggedUser.lastName; 
                LoggedUser.lastSeen = DateTime.Now;
                db.Entry(LoggedUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                LoginModel.LoginErrorMessage = T.txt[45, L.nr];
                return View("Login", LoginModel);
                }
            }
            catch
            {
                LoginModel.LoginErrorMessage = T.txt[45, L.nr];
                return View("Login", LoginModel);
            }
        }
        public ActionResult LogOut() //Käyttäjän uloskirjautuminen
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Create() //käyttäjän Luonti näkymän palatus
        {
            if (Session["UserName"] != null && Session["Permission"].ToString() == "1")
            {
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }

        [HttpPost]  //Käyttäjän luominen
        public ActionResult Create([Bind(Include = "username, password, email, firstName, lastName, admin")] User newUser)
        {
            if (ModelState.IsValid && Session["UserName"] != null && Session["Permission"].ToString() == "1")
            {
                var userNameAlreadyExists = db.Users.Any(x => x.username == newUser.username); //Katsotaan löytyykö samalla nimellä käyttäjää
                if (userNameAlreadyExists)
                {
                    ViewBag.CreateUserError = T.txt[47, L.nr];
                    return View();
                }
                try
                {
                    var bpassword = System.Text.Encoding.UTF8.GetBytes(newUser.password);
                    var hash = System.Security.Cryptography.MD5.Create().ComputeHash(bpassword);
                    newUser.password = Convert.ToBase64String(hash);
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

        public ActionResult Delete(int? id) //Käyttäjän poistonäkymän palautus
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

        [HttpPost, ActionName("Delete")] //Käyttäjän poisto
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
                    if (Session["UserName"].ToString() == user.username) { return RedirectToAction("Logout", "Users"); }
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
        public ActionResult Edit(int? id) //Käyttäjän muokkausnäkymän palautus
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
                    user.password = "";
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
        [HttpPost] //Käyttäjän muokkaus
        [ValidateAntiForgeryToken] //Katso https://go.microsoft.com/fwlink/?LinkId=317598
        public ActionResult Edit([Bind(Include = "username, password, email, firstName, lastName, admin, user_id, lastSeen")] User editee)
        {
            if (ModelState.IsValid && (Session["UserName"] != null))
            {
                var userNameAlreadyExists = db.Users.Any(x => x.username == editee.username); //Katsotaan löytyykö samalla nimellä käyttäjää
                if (userNameAlreadyExists && db.Users.Find(editee.user_id).username != editee.username)
                {
                ViewBag.CreateUserError = T.txt[26, L.nr];
                return View();
                }
                try
                {
                if (editee.password == null)  
                {
                editee.password = db.Users.Find(editee.user_id).password; //Salasana kenttä on tyhjä, haetaan nykyinen tietokannasta, eikä hashata.
                }
                else 
                    {
                    var bpassword = System.Text.Encoding.UTF8.GetBytes(editee.password);
                    var hash = System.Security.Cryptography.MD5.Create().ComputeHash(bpassword); //Muussa tapauksessa syötetty salasana hashataan ennen tiedon talletusta.
                    editee.password = Convert.ToBase64String(hash); 
                    }
                    var existingEntity = db.Users.Find(editee.user_id);
                    db.Entry(existingEntity).CurrentValues.SetValues(editee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                    }
                    catch
                    {
                    ViewBag.CreateUserError = T.txt[26, L.nr];
                    return View();
                    }
                    finally
                    {
                    db.Dispose();
                    }
                    }
             return View(User);
        }
    }
}
