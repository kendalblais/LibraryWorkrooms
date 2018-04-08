using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryWorkroomSystem.Models.Database;

namespace LibraryWorkroomSystem.Controllers
{
    public class WorkroomsController : Controller
    {
        // GET: Workrooms
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WorkroomSelection(int floornum, int day, int month, int year)
        {
            ViewBag.floornum = floornum;
            ViewBag.day = day;
            ViewBag.month = month;
            ViewBag.year = year;
            return View();
        }

        public ActionResult DisplayWorkroom(int floorNum, int roomNum, int day, int month, int year, int time)
        {
            ViewBag.time = time;
            return View();
        }
    }
}