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
            Floors allFloors = LibraryDatabase.getInstance().getFloors();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (Floor flr in allFloors.list)
            {
                items.Add(new SelectListItem { Text = flr.floor_no.ToString(), Value = flr.floor_no.ToString() });
            }

            var model = new FloorSelectModel { list = items };
            return View(model);
        }

        public ActionResult AddNewBook(String title, String author, String publish_date, String series, String floor)
        {
            return Redirect("Index");
        }

        public ActionResult ViewBooks()
        {
            return View();
        }
    }
}