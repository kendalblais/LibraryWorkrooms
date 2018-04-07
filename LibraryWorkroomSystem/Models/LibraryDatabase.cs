﻿using MySql.Data.MySqlClient;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LibraryWorkroomSystem.Models.Database
{
    public partial class LibraryDatabase : AbstractDatabase
    {
        private LibraryDatabase() { }

        public static LibraryDatabase getInstance()
        {
            if(instance == null)
            {
                instance = new LibraryDatabase();
                
            }
            return instance;
        }

       

        public Account getAccountData()
        {
            Account acct = new Account();
            string user = Sessions.getUser();
            acct.accType = getAccountType(user);
            acct.usrname = user;
            acct.cardNo = null;

            string query = @"SELECT * FROM " + dbname + @".user " +
                @"WHERE username='" + user + @"';";

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    dataReader.Read();
                    acct.actualName = dataReader.GetString("name");

                    dataReader.Close();


                }
                catch (Exception e)
                {
                    
                }
                finally
                {
                    closeConnection();
                }
            }
            else
            {
                

            }
            return acct;
        }

        public AccountType getAccountType(string username) {
            string query = @"SELECT * FROM " + dbname + @".employee " +
                @"WHERE emp_username='" + username + @"';";



            bool result = false;
            string position = "";

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    result = dataReader.Read();
                    position = dataReader.GetString("position");
               
                    dataReader.Close();

                  
                }
                catch (Exception e)
                {
                    result = false;
                }
                finally
                {
                    closeConnection();
                }
            }
            else
            {
                result = false;

            }
            if (!result)
                return AccountType.user;
            else {
                if (position == "admin")
                    return AccountType.admin;
                else
                    return AccountType.employee;
            }


        }

        public string addEmployee(string username, AccountType position) {

            
            string response = "";

            string type;
            if (position == AccountType.admin)
                type = "admin";
            else
                type = "employee";


            string query = @"INSERT INTO " + dbname + ".employee (emp_username,position) " +
                "VALUES('" + username + "','" + type + "');";

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();

                }
                catch (MySqlException e)
                {
                    response = e.Message;
                }
                catch (Exception e)
                {

                }
                finally
                {
                    closeConnection();
                }
            }
            else
            {
                throw new Exception("Could not connect to database.");
            }
            
            return response;
        }

        public bool attemptLogin(string username, string password)
        {
            string query = @"SELECT * FROM " + dbname + @".user " +
                @"WHERE username='" + username + @"' " +
                @"AND password='" + password + @"';";

            bool result = false;
            

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    result = dataReader.Read();
                    dataReader.Close();
                }
                catch (Exception e)
                {
                    result = false;
                }
                finally
                {
                    closeConnection();
                }
            }
            else
            {
                result = false;
                
            }
            return result;

        }

        public bool createNewAccount(string username, string password, string name)
        {
            string query = @"INSERT INTO " + dbname + ".user (username,name,password) " +
                "VALUES('" + username + "','" + name + "','" + password + "');";
            bool result = true;
            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    
                }
                catch (MySqlException e)
                {
                    result = false;
                }
                catch (Exception e)
                {
   
                }
                finally
                {
                    closeConnection();
                }
            }
            else
            {
                throw new Exception("Could not connect to database.");
            }

            return result;
        }
        /// <summary>
        /// Gets a long URL based on the id of the short url
        /// </summary>
        /// <param name="id">The id of the short url</param>
        /// <throws type="ArgumentException">Throws an argument exception if the short url id does not refer to anything in the database</throws>
        /// <returns>The long url the given short url refers to</returns>
        public string getLongUrl(string id)
        {
            string query = @"SELECT * FROM " + dbname + ".shortenedLinks "
                + "WHERE id=" + id + ";";

            if(openConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();

                if(reader.Read() == true)
                {
                    return reader.GetString("original");
                }
                else
                {
                    //Throw an exception indicating no result was found
                    throw new ArgumentException("No url in the database matches that id.");
                }
            }
            else
            {
                throw new Exception("Could not connect to database.");
            }
        }

        /// <summary>
        /// Saves the longURL to the database to be accessed later via the id that is returned.
        /// </summary>
        /// <param name="longURL">The longURL to be saved</param>
        /// <returns>The id of the url</returns>
        public string saveLongURL(string longURL)
        {
            string query = @"INSERT INTO " + dbname + ".shortenedLinks(original) "
                + @"VALUES('" + longURL + @"');";

            if(openConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();

                command.CommandText = "SELECT * FROM " + dbname + ".shortenedLinks WHERE id = LAST_INSERT_ID();";

                MySqlDataReader reader = command.ExecuteReader();

                if(reader.Read() == true)
                {
                    string result = reader.GetInt64("id").ToString();
                    closeConnection();
                    return result.ToString();
                }
                else
                {
                    closeConnection();
                    throw new Exception("Error: LAST_INSERT_ID() did not work as intended.");
                }
            }
            else
            {
                throw new Exception("Could not connect to database");
            }
        }

    }

    public partial class LibraryDatabase : AbstractDatabase
    {
       
        private static LibraryDatabase instance = null;

        private const String dbname = "LibraryWorkroom";
        public override String databaseName { get; } = dbname;

        // This represents the database schema
        //TODO: ADD FOREIGN KEYS 
        protected override Table[] tables { get; } =
        {

            // This represents the BOOK table that keeps track of all the books in the system
            new Table ( dbname, "book", new Column[]
            {
                new Column( "title", "VARCHAR(300)", new string[] { "NOT NULL" }, true),  // primary key
                new Column( "author", "VARCHAR(300)", new string[] { "NOT NULL" }, true), // primary key
                new Column( "publish_date", "DATE", new string[] { "NOT NULL" }, false),
                new Column( "series", "VARCHAR(300)", null, false),
                new Column( "renter_username", "VARCHAR(300)", null, false),              // null means no mods to that attribute
                new Column( "take_out_date", "DATE", null, false),
                new Column( "return_date", "DATE", null, false),
                new Column( "belonging_floor", "INTEGER", null, false),
                new Column( "bookkeeper_ID", "INTEGER", null, false)
            }),
        
            // This represents the USER table that keeps track of all the users in the system 
            new Table ( dbname, "user", new Column[]
            {
                new Column( "username", "VARCHAR(300)", new string[] { "NOT NULL", "UNIQUE" }, true),
                new Column( "name", "VARCHAR(300)", new string[] { "NOT NULL" }, false),
                new Column( "password", "VARCHAR(300)", new string[] { "NOT NULL" }, false),
                new Column( "admin_ID", "INTEGER", null, false)
            }),

            // This represents the FLOOR table that keeps track of all the floors in the building
            new Table ( dbname, "floor", new Column[]
            {
                new Column( "floor_no", "INTEGER", new string[] { "NOT NULL", "UNIQUE" }, true),
                new Column( "number_of_workrooms", "INTEGER", new string[] { "NOT NULL", "DEFAULT 0" }, false)
            }),

            // This represents the EMPLOYEE table that keeps track of all the employees in the system 
            new Table ( dbname, "employee", new Column[]
            {
                new Column( "employee_ID", "INTEGER", new string[] { "NOT NULL", "UNIQUE", "AUTO_INCREMENT"}, true),
                new Column( "emp_username", "VARCHAR(300)", new string[] { "NOT NULL", "UNIQUE" }, false),
                new Column( "position", "VARCHAR(300)", new string[] { "NOT NULL" }, false)
            }),

            // This is the WORKROOM table that keeps track of all the workrooms in the building
            new Table ( dbname, "workroom", new Column[]
            {
                new Column( "workroom_no", "INTEGER", new string[] { "NOT NULL" }, true),
                new Column( "belonging_floor", "INTEGER", new string[] { "NOT NULL" }, true),
                new Column( "room_size", "INTEGER", new string[] { "NOT NULL" }, false),
                new Column( "admin_ID", "INTEGER", null, false)
            }),

            // This represents the PROGRAM table that keeps track of all the different programs offered by the library
            new Table ( dbname, "program", new Column[]
            {
                new Column( "name", "VARCHAR(300)", new string[] { "NOT NULL", "UNIQUE" }, true),
                new Column( "date", "DATE", new string[] { "NOT NULL "}, false),
                new Column( "start_time", "TIME", new string[] { "NOT NULL" }, false),
                new Column( "end_time", "TIME", new string[] { "NOT NULL" }, false),
                new Column( "teacher_ID", "INTEGER", new string[] { "NOT NULL" }, false)
            }),

            // This represents the USERS_IN_PROGRAMS table that holds the relationship between each program and its signed up users 
            new Table ( dbname, "users_in_programs", new Column[]
            {
                new Column( "program_name", "VARCHAR(300)", new string[] { "NOT NULL" }, true),
                new Column( "username", "VARCHAR(300)", new string[] { "NOT NULL" }, true)
            }),

            new Table ( dbname, "workroomBookings", new Column[]
            {
                new Column( "floor_no", "INTEGER", new string[] { "NOT NULL" }, true),
                new Column( "workroom_no", "INTEGER", new string[] { "NOT NULL" }, true),
                new Column( "reserver_username", "VARCHAR(300)", null, false),
                new Column( "time_of_reservation", "DATETIME", null, false)      //each reservation will be an hour length and only the start time is recorded          

            })


        };
    }
}