using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryWorkroomSystem.Models.Database
{

    public class Book
    {
        public String title { get; set; }

        public String author { get; set; }

        public String publish_date { get; set; }

        public String series { get; set; }

        public String renter { get; set; }

        public DateTime take_out_date { get; set; }

        public DateTime returnDate { get; set; } 

        public int floorNumber { get; set; }
    }
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