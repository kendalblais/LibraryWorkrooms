using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryWorkroomSystem.Models.Database;

namespace LibraryWorkroomSystem.Controllers
{
    public class ProgramsController : Controller
    {
        // GET: Programs
        public ActionResult Index()
        {
            if (Sessions.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult ViewRegisteredPrograms()
        {
            if (Sessions.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult ViewAllPrograms()
        {
            if (Sessions.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Home");
            }

            string[] programs = LibraryDatabase.getInstance().getPrograms();

            ViewBag.progList = programs;
            return View();
        }

        public ActionResult AddProgram()
        {
            if (Sessions.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Home");
            }

            List<string> employees = LibraryDatabase.getInstance().getEmployees();

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (string emp in employees)
            {
                items.Add(new SelectListItem { Text = emp, Value = emp });
            }

            var model = new MySelectModel { list = items };
            return View(model);
        }

        public ActionResult AddNewProgram(String name, String description, String date, String startTime, String endTime, String teacherID)
        {
            LibraryDatabase.getInstance().addProgram(name, description, date, startTime, endTime, teacherID);
            return Redirect("Index");
        }

        public ActionResult DisplayProgram(string id)
        {
            if (Sessions.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Home");
            }
            
            
            Program program = LibraryDatabase.getInstance().getProgram(id);

            return View(program);
        }

        public ActionResult RegisterProgram(String progName)
        {
            if (Sessions.isLoggedIn() == false)
            {
                return RedirectToAction("Index", "Home");
            }
           
            LibraryDatabase.getInstance().registerForProgram(progName);
            return Redirect("Index");
        }
    }
}