using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryWorkroomSystem.Models.Database;


namespace LibraryWorkroomSystem.Controllers
{
    public class FloorsController : Controller
    {
        // GET: Floors
        public ActionResult Index()
        {
            Floors floors = LibraryDatabase.getInstance().getFloors();
            return View(floors);
        }

        public ActionResult AddFloor(string floorNumber) {

            string response = LibraryDatabase.getInstance().addFloor(Int32.Parse(floorNumber));

            return Redirect("Index");
        }
    }
}