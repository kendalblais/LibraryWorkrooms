﻿using System;
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
    }
}