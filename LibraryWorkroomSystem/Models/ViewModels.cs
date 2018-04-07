using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWorkroomSystem.Models.Database
{
    public class Account
    {
        public AccountType accType { get; set; }

        public String actualName { get; set; } = null;

        public String usrname { get; set; } = null;

        public String cardNo { get; set; } = null;

    }

    public class Floors
    {
        public List<Floor> list = new List<Floor>();
    }

    public class Floor {

        public int floor_no { get; set; }

        public int no_of_workrooms { get; set; }
    }


}