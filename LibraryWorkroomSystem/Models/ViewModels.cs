using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryWorkroomSystem.Models.Database
{
    /// <summary>
    /// Model to access a Book from the html view
    /// </summary>
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

    /// <summary>
    /// Model to access a Program from the html view
    /// </summary>
    public class Program
    {
        public String name { get; set; }

        public String description { get; set; }

        public String date { get; set; }

        public String startTime { get; set; }

        public String endTime { get; set; }

        public int teacherID { get; set; }

        public bool inProgram { get; set; } = false;
    }

    /// <summary>
    /// Model to access the Account info of a logged in user from the html view
    /// </summary>
    public class Account
    {
        public AccountType accType { get; set; }

        public String actualName { get; set; } = null;

        public String usrname { get; set; } = null;

        public Boolean premium_or_not { get; set; }

    }

    /// <summary>
    /// List of Floors
    /// </summary>
    public class Floors
    {
        public List<Floor> list = new List<Floor>();
    }

    /// <summary>
    /// Generic model for drop down lists
    /// </summary>
    public class MySelectModel {
        public IEnumerable<SelectListItem> list { get; set; }
    }

    /// <summary>
    /// Definition of Floor to be accessed in the html view
    /// </summary>
    public class Floor {

        public int floor_no { get; set; }

        public int no_of_workrooms { get; set; }
    }


}