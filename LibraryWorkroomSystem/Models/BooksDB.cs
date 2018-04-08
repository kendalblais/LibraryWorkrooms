using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data.SqlTypes;

namespace LibraryWorkroomSystem.Models.Database
{
    public partial class LibraryDatabase
    {

        public void updateBookAvailability(string title, string author)
        {
            string query = @"UPDATE " + dbname + @".book SET renter_username = NULL, take_out_date = NULL, return_date = NULL " +
                @"WHERE title = '" + title + @"' AND author = '" + author + @"';";
            if (openConnection() == true)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteReader();

                closeConnection();

            }
            else
            {

            }
        }

        public void deleteBook(string title, string author)
        {
            string query = @"DELETE FROM " + dbname + @".book WHERE title='" + title +
                @"' AND author='" + author + @"';";

            if (openConnection() == true)
            {
               
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteReader();
               
                closeConnection();
                
            }
            else
            {

            }
        }

        public void takeOutBook(string title, string author, DateTime takeout, DateTime returndate)
        {
            
            string query = @"UPDATE " + dbname + @".book SET renter_username = '" + Sessions.getUser() + @"', take_out_date = '" + takeout.ToString("yyyy-MM-dd HH:mm:ss")
                + @"', return_date = '" + returndate.ToString("yyyy-MM-dd hh:mm:ss") + @"' WHERE title='" + title + @"' AND author='" +
                author + @"';";

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteReader();
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

            }
            
        }

        public string[] searchBooks(string request, string type)
        {
            string[] books = null;

            string query;
            if (type == "series")
            {
                query = @"SELECT * FROM " + dbname + @".book WHERE series = '" + request + @"';";
            }
            else if (type == "rented")
            {
                query = @"SELECT * FROM " + dbname + @".book WHERE renter_username = '" + Sessions.getUser() + @"';";
            }
            else
            {
                query = @"SELECT * FROM " + dbname + @".book WHERE title LIKE '%" +
                request + @"%' OR author LIKE '%" + request + @"%';";
            }

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = command.ExecuteReader();

                    List<string> temp = new List<string>();
                    while (dataReader.Read())
                    {
                        temp.Add(dataReader.GetString("title") + "(" + dataReader.GetString("author") + ")");
                    }
                    dataReader.Close();
                    books = temp.ToArray();
                    
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
                
            }
            return books;
        }

        public Book getBook(string title, string author)
        {

            Book book = new Book();

            string query = @"SELECT * FROM " + dbname + @".book WHERE title='" +
                title + @"' AND author='" + author + "';";

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = command.ExecuteReader();


                    if (dataReader.Read())
                    {

                        book.title = dataReader.GetString("title");
                        book.author = dataReader.GetString("author");
                        book.floorNumber = dataReader.GetInt32("belonging_floor");
                        book.publish_date = dataReader.GetString("publish_date");
                        book.renter = dataReader.GetString("renter_username");
                        book.returnDate = dataReader.GetDateTime("return_date");
                        book.series = dataReader.GetString("series");
                        book.take_out_date = dataReader.GetDateTime("take_out_date");
                        
                    }
                    else
                    {
                        throw new Exception("Reader cannot read. DirectoryServiceDatabase Error.");
                    }

                    dataReader.Close();
                    
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
                
            }
            return book;
        }

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