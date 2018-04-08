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

        public ActionResult SearchBook(String SearchBox)
        {
            string[] bookList = LibraryDatabase.getInstance().searchBooks(SearchBox, "search");

            ViewBag.bookList = bookList;

            return View();
        }

        public ActionResult DisplaySeries(String id)
        {
            string[] seriesList = LibraryDatabase.getInstance().searchBooks(id, "series");
            ViewBag.seriesList = seriesList;
            ViewBag.series = id;
            return View();
        }

        public ActionResult DisplayBook(string id)
        {
            string title = id.Substring(0, id.IndexOf("("));
            string author = id.Substring(id.IndexOf("(") + 1, id.IndexOf(")") - 1 - id.IndexOf("("));
            Book book = LibraryDatabase.getInstance().getBook(title, author);
            
            return View(book);
        }

        public ActionResult TakeOutBook(String title, String author)
        {
            DateTime takeout = DateTime.Now;
            DateTime returnDate = takeout.AddDays(7);

            LibraryDatabase.getInstance().takeOutBook(title, author, takeout, returnDate);
            return Redirect("Index");
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
            LibraryDatabase.getInstance().addBook(title, author, publish_date, series, Int32.Parse(floor));
            ViewBag.confirmMessage = "Book Added!";
            return Redirect("AddBook");
            
        }

        public ActionResult ViewBooks()
        {
            string[] rentedList = LibraryDatabase.getInstance().searchBooks(null, "rented");
            ViewBag.rentedList = rentedList;
            return View();
        }

        public ActionResult DeleteBook(String title, String author)
        {
            LibraryDatabase.getInstance().deleteBook(title, author);
            return Redirect("Index");
        }

        public ActionResult UpdateAvailable(String title, String author)
        {
            LibraryDatabase.getInstance().updateBookAvailability(title, author);
            return Redirect("Index");
        }
    }
}