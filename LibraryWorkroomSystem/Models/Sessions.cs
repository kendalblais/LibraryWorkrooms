using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWorkroomSystem.Models.Database
{
    public static class Sessions
    {
 
        /// <summary>
        /// Checks if the user is currently logged in or not
        /// </summary>
        /// <returns> true or false </returns>
        public static bool isLoggedIn()
        {
            if (getUser() == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Logs the current user out of the sytsem by logging them out of the http session
        /// </summary>
        public static void logout()
        {
            if(getUser() != null)
            {
                HttpContext.Current.Session["user"] = null;
                HttpContext.Current.Session["type"] = null;
            }
        }

        /// <summary>
        /// Logs in the user with the following username and account type 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="type"></param>
        public static void setUser(string user, AccountType type)
        {
            
            HttpContext.Current.Session["user"] = user;
            HttpContext.Current.Session["type"] = type.ToString();
            
        }

        /// <summary>
        /// sets the premium status of the currently logged in user to the value passed in
        /// </summary>
        /// <param name="premium"></param>
        public static void setPremium(bool premium) {

            HttpContext.Current.Session["premium"] = premium;
        }

        /// <summary>
        /// Retrieves whether or not the current user is premium or not
        /// </summary>
        /// <returns></returns>
        public static bool getPremium() {
            return (bool)HttpContext.Current.Session["premium"];
        }

        /// <summary>
        /// sets the nav-bar colour for the currently logged in user type
        /// </summary>
        /// <returns></returns>
        public static string setColour()
        {
            string response = "background-color:default";
           
            if((string)HttpContext.Current.Session["type"] == "admin")
                response = "background-color:darkred";
            else if((string)HttpContext.Current.Session["type"] == "employee")
                response = "background-color:darkblue";
            return response;
        }

        /// <summary>
        /// retrieves the account type of the user currently logged in
        /// </summary>
        /// <returns></returns>
        public static string getType()
        {
            return (string)HttpContext.Current.Session["type"];
        }

        /// <summary>
        /// retrieves the username of the currently logged in user
        /// </summary>
        /// <returns></returns>
        public static string getUser()
        {
            return (string)HttpContext.Current.Session["user"];
        }
    }

    public enum AccountType { user, admin, employee};
}