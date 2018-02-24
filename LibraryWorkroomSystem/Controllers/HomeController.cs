using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinkShortener.Models.Database;

namespace LibraryWorkroomSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //this is just to test the database creation
            LinkDatabase db = LinkDatabase.getInstance();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String Username, String Password)
        {
            ViewBag.MyGreeting = "Hello " + Username + "!\n Welcome to LIBROOM...";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}