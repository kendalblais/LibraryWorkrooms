using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryWorkroomSystem.Models.Database
{
    public class Account
    {
        public AccountType accType { get; set; }

        public String actualName { get; set; } = null;

        public String usrname { get; set; } = null;

        public Boolean premium_or_not { get; set; }

    }

    public class Floors
    {
        public List<Floor> list = new List<Floor>();
    }

    public class FloorSelectModel {
        public IEnumerable<SelectListItem> list { get; set; }
    }

    public class Floor {

        public int floor_no { get; set; }

        public int no_of_workrooms { get; set; }
    }


}