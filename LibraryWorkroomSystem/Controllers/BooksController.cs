using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryWorkroomSystem.Models.Database;

namespace LibraryWorkroomSystem.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchBook()
        {
            return View();
        }

        public ActionResult AddBook()
        {
            return View();
        }

        public ActionResult ViewBooks()
        {
            return View();
        }
    }
}