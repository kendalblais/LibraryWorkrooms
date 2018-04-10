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
            if (!Sessions.isLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult WorkroomSelection(int floornum, int roomSize, int day, int month, int year)
        {
            if (!Sessions.isLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.floornum = floornum;
            ViewBag.roomsize = roomSize;
            ViewBag.day = day;
            ViewBag.month = month;
            ViewBag.year = year;
            return View();
        }

        public ActionResult DisplayWorkroom(int floorNum, int roomNum, int day, int month, int year, int time)
        {
            if (!Sessions.isLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.floornum = floorNum;
            ViewBag.roomnum = roomNum;
            ViewBag.date1 = (new DateTime(year, month, day, time, 0, 0)).ToString();
            ViewBag.date2 = (new DateTime(year, month, day, time + 1, 0, 0)).ToString();
            ViewBag.day = day;
            ViewBag.month = month;
            ViewBag.year = year;
            ViewBag.time = time;
            return View();
        }

        public ActionResult BookWorkroom(int floorNum, int roomNum, int day, int month, int year, int time)
        {
            if (!Sessions.isLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            if (LibraryDatabase.getInstance().bookWorkroom(roomNum, floorNum, Sessions.getUser(), new DateTime(year, month, day, time, 0, 0)))
            {
                return View("Success");
            }
            else
                return View("Failure");
        }

        public ActionResult RemoveBooking(int floorNum, int roomNum, int day, int month, int year, int time)
        {
            if (!Sessions.isLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            if (LibraryDatabase.getInstance().removeBooking(floorNum, roomNum, new DateTime(year, month, day, time, 0, 0)))
            {
                return View("Deletion_Success");
            }
            else
                return View("Deletion_Failure");
        }

        public ActionResult DisplayBooked(int floorNum, int roomNum, int day, int month, int year, int time)
        {
            if (!Sessions.isLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            LibraryWorkroomSystem.Models.DataTypes.WorkroomReservation temp = LibraryDatabase.getInstance().getSingleReservation(floorNum, roomNum, new DateTime(year, month, day, time, 0, 0));
            if (temp == null)
                return View("SearchBooked_Failure");
            else
            {
                ViewBag.reserver = temp.reserver;
                ViewBag.time = temp.timeOfReservation.ToString();
                ViewBag.floornum = floorNum;
                ViewBag.roomnum = roomNum;
                ViewBag.day = day;
                ViewBag.month = month;
                ViewBag.year = year;
                ViewBag.time2 = time;
                return View("SearchBooked_Success");
            }
        }
    }
}