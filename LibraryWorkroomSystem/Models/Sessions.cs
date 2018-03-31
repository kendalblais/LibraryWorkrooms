using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWorkroomSystem.Models.Database
{
    public static class Sessions
    {
 
        public static bool isLoggedIn()
        {
            if (getUser() == null)
            {
                return false;
            }
            return true;
        }

        public static void logout()
        {
            if(getUser() != null)
            {
                HttpContext.Current.Session["user"] = null;
                HttpContext.Current.Session["type"] = null;
            }
        }

        public static void setUser(string user, AccountType type)
        {
            
            HttpContext.Current.Session["user"] = user;
            HttpContext.Current.Session["type"] = type.ToString();
            
        }

        public static string setColour()
        {
            string response = "background-color:default";
           
            if((string)HttpContext.Current.Session["type"] == "admin")
                response = "background-color:darkred";
            else if((string)HttpContext.Current.Session["type"] == "employee")
                response = "background-color:darkblue";
            return response;
        }

        public static string getType()
        {
            return (string)HttpContext.Current.Session["user"];
        }

        public static string getUser()
        {
            return (string)HttpContext.Current.Session["user"];
        }
    }

    public enum AccountType { user, admin, employee};
}