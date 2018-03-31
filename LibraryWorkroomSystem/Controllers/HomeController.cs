using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryWorkroomSystem.Models.Database;



namespace LibraryWorkroomSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //this is just to test the database creation
            LibraryDatabase db = LibraryDatabase.getInstance();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout() {
            Sessions.logout();
            return View("Index");
        }

        public ActionResult MyAccount() {

            Account account = LibraryDatabase.getInstance().getAccountData();

            return View(account);
        }

        
        public ActionResult AttemptLogin(String Username, String Password)
        {
            bool result = LibraryDatabase.getInstance().attemptLogin(Username, Password);
            if (!result)
            {
                ViewBag.MyGreeting = "Invalid login or account doesnt exist";
                return View("Login");
            }

            AccountType type = LibraryDatabase.getInstance().getAccountType(Username);
            Sessions.setUser(Username, type);

            return View("Index");
        }

        public ActionResult CreateAccount()
        {
            return View();
        }

        
        public ActionResult CreateAccountRequest(String name, String username, String password, AccountType accountType)
        {
            bool result = LibraryDatabase.getInstance().createNewAccount(username, password, name);
           
            if (!result)
            {
                ViewBag.Greeting = "username already exists";
                return View("CreateAccount");
            }
            if (accountType == AccountType.admin || accountType == AccountType.employee)
            {
                string response = LibraryDatabase.getInstance().addEmployee(username, accountType);
            }
            return View("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}