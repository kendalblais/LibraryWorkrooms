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
            if (Sessions.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult SearchBook(String SearchBox)
        {
            if (Sessions.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Home");
            }
            string[] bookList = LibraryDatabase.getInstance().searchBooks(SearchBox, "search");

            ViewBag.bookList = bookList;

            return View();
        }

        public ActionResult DisplaySeries(String id)
        {
            if (Sessions.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Home");
            }
            string[] seriesList = LibraryDatabase.getInstance().searchBooks(id, "series");
            ViewBag.seriesList = seriesList;
            ViewBag.series = id;
            return View();
        }

        public ActionResult DisplayBook(string id)
        {
            if (Sessions.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Home");
            }
            string title = id.Substring(0, id.IndexOf("("));
            string author = id.Substring(id.IndexOf("(") + 1, id.IndexOf(")") - 1 - id.IndexOf("("));
            Book book = LibraryDatabase.getInstance().getBook(title, author);
            
            return View(book);
        }

        public ActionResult TakeOutBook(String title, String author)
        {
            if (Sessions.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Home");
            }
            DateTime takeout = DateTime.Now;
            DateTime returnDate = takeout.AddDays(7);

            LibraryDatabase.getInstance().takeOutBook(title, author, takeout, returnDate);
            return Redirect("Index");
        }

        public ActionResult AddBook()
        {
            if (Sessions.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Home");
            }
            

            Floors allFloors = LibraryDatabase.getInstance().getFloors();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (Floor flr in allFloors.list)
            {
                items.Add(new SelectListItem { Text = flr.floor_no.ToString(), Value = flr.floor_no.ToString() });
            }

            var model = new MySelectModel { list = items };
            return View(model);
        }

        public ActionResult AddNewBook(String title, String author, String publish_date, String series, String floor)
        {
            if (Sessions.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Home");
            }
            LibraryDatabase.getInstance().addBook(title, author, publish_date, series, Int32.Parse(floor));
            ViewBag.confirmMessage = "Book Added!";
            return Redirect("AddBook");
            
        }

        public ActionResult ViewBooks()
        {
            if (Sessions.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Home");
            }
            string[] rentedList = LibraryDatabase.getInstance().searchBooks(null, "rented");
            ViewBag.rentedList = rentedList;
            return View();
        }

        public ActionResult DeleteBook(String title, String author)
        {
            if (Sessions.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Home");
            }
            LibraryDatabase.getInstance().deleteBook(title, author);
            return Redirect("Index");
        }

        public ActionResult UpdateAvailable(String title, String author)
        {
            if (Sessions.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Home");
            }
            LibraryDatabase.getInstance().updateBookAvailability(title, author);
            return Redirect("Index");
        }
    }
}