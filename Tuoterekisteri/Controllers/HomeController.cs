using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tuoterekisteri.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ENG()
        {

            L.nr = 0;
            return View();
        }


        public ActionResult FIN()
        {

            L.nr = 1;
            return View();
        }


        public ActionResult SWE()
        {

            L.nr = 2;
            return View();
        }


    }
}
