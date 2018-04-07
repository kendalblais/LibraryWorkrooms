using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace LibraryWorkroomSystem.Models.Database
{
    public partial class LibraryDatabase
    {

        public void addBook(string title, string author, string publish_date, string series, int floor_no) {


            string query = @"INSERT INTO " + dbname + ".book (title,author,publish_date,series,belonging_floor) " +
                "VALUES('" + title + "','" + author + "','" + publish_date + "','" + series + "'," + floor_no + ");";

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();

                }
                catch (MySqlException e)
                {
                    
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
        }

        //Book methods for the database here
    }
}