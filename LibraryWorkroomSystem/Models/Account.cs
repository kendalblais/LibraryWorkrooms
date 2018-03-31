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

        public string cardNo { get; set; } = null;

    }
}