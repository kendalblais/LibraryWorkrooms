using MySql.Data.MySqlClient;
using LibraryWorkroomSystem.Models.DataTypes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWorkroomSystem.Models.Database
{
    //TODO: Add functionality to this class.

    /// <summary>
    /// This part of the class contains useful methods for processing workrooms information.
    /// </summary>
    public partial class LibraryDatabase
    {
        /// <summary>
        /// A useful function for retrieving the number of floors of the library.
        /// </summary>
        /// <returns>A number representing how many floors there are in the library.</returns>
       public int getNumberOfFloors()
        {
            if(openConnection())
            {
                int toReturn = 0;

                try
                {
                    string query = @"SELECT COUNT(*) FROM " + dbname + @" .FLOOR;";
                    MySqlCommand com = new MySqlCommand(query, connection);
                    MySqlDataReader red = com.ExecuteReader();
                    red.Read();
                    toReturn = red.GetInt16(0);
                }
                catch(Exception a)
                {
                    Console.WriteLine("Issue occurred when retrieving number of floors from database.");
                    Console.WriteLine(a.Message);
                }
                finally
                {
                    closeConnection();
                }

                return toReturn;
            }
            else
            {
                throw new Exception("Issue connecting to database. Could not retrieve number of floors.");
            }  
        }

        /// <summary>
        /// This function searches the database and returns information about reservations.
        /// </summary>
        /// <param name="floorNum">This number represents which floor to select from.</param>
        /// <returns>A list of reservations on the given floor.</returns>
        public List<WorkroomReservation> getReservations(int floorNum)
        {
            if(openConnection())
            {
                List<WorkroomReservation> toReturn = new List<WorkroomReservation>();

                try
                {
                    string query = @"SELECT * FROM " + dbname + @".WORKROOMBOOKINGS WHERE FLOOR_NO = '" + floorNum + @"';";
                    MySqlCommand com = new MySqlCommand(query, connection);
                    MySqlDataReader red = com.ExecuteReader();

                    //Read through database and get reservation information. Add that information to the list.
                    while (red.Read())
                    {
                        WorkroomReservation tempRes = new WorkroomReservation
                        {
                            room = new Workroom
                            {
                                floor = red.GetInt32("FLOOR_NO"),
                                number = red.GetInt32("WORKROOM_NO")
                            },
                            reserver = red.GetString("RESERVER_USERNAME"),
                            timeOfReservation = red.GetDateTime("TIME_OF_RESERVATION")
                        };

                        toReturn.Add(tempRes);
                    }
                }
                catch(Exception a)
                {
                    Console.WriteLine("Issue occurred when retrieving reservation information from database.");
                    Console.WriteLine(a.Message);
                }
                finally
                {
                    closeConnection();
                }

                return toReturn;
            }
            else
            {
                throw new Exception("Issue connecting to database. Could not retrieve reservation information.");
            }
        }

        /// <summary>
        /// Returns reservations based on a given date.
        /// </summary>
        /// <param name="floorNum">This number represents which floor to select from.</param>
        /// <param name="day">Day of which to search for reservations.</param>
        /// <param name="month">Month of which to search for reservations.</param>
        /// <param name="year">Year of which to search for reservations.</param>
        /// <returns>List of reservations on the given date.</returns>
        public List<WorkroomReservation> getReservations(int floorNum, int day, int month, int year)
        {
            List<WorkroomReservation> toReturn = new List<WorkroomReservation>();

            foreach(WorkroomReservation reservation in getReservations(floorNum))
            {
                //Not sure what DateTime.Equals() method will return. Did this to be safe.
                if (reservation.timeOfReservation.Year == year && reservation.timeOfReservation.Month == month && reservation.timeOfReservation.Day == day)
                    toReturn.Add(reservation);
            }

            return toReturn;
        }

        /// <summary>
        /// Returns reservations based on username.
        /// </summary>
        /// <param name="username">The username used to search for reservations in the database.</param>
        /// <returns>A list of reservations booked by the user.</returns>
        public List<WorkroomReservation> getReservations(string username)
        {
            if(openConnection())
            {
                List<WorkroomReservation> toReturn = new List<WorkroomReservation>();

                try
                {
                    string query = @"SELECT * FROM " + dbname + @".WORKROOMBOOKINGS WHERE RESERVER_USERNAME = '" + username + @"';";
                    MySqlCommand com = new MySqlCommand(query, connection);
                    MySqlDataReader red = com.ExecuteReader();

                    //Read through database and get reservation information. Add that information to the list.
                    while (red.Read())
                    {
                        WorkroomReservation tempRes = new WorkroomReservation
                        {
                            room = new Workroom
                            {
                                floor = red.GetInt32("FLOOR_NO"),
                                number = red.GetInt32("WORKROOM_NO")
                            },
                            reserver = red.GetString("RESERVER_USERNAME"),
                            timeOfReservation = red.GetDateTime("TIME_OF_RESERVATION")
                        };

                        toReturn.Add(tempRes);
                    }
                }
                catch(Exception a)
                {
                    Console.WriteLine("Issue occurred when retrieving reservation information from database for user: " + username + ".");
                    Console.WriteLine(a.Message);
                }
                finally
                {
                    closeConnection();
                }

                return toReturn;
            }
            else
            {
                throw new Exception("Issue connecting to database. Could not retrieve reservation information for user: " + username + ".");
            }
        }

        /// <summary>
        /// This function searches the database and returns information about workrooms.
        /// </summary>
        /// <param name="floorNum">This number represents which floor to select from.</param>
        /// <returns>A list of workrooms on the given floor.</returns>
        public List<Workroom> getWorkrooms(int floorNum)
        {
            if (openConnection())
            {
                List<Workroom> toReturn = new List<Workroom>();

                try
                {
                    string query = @"SELECT * FROM " + dbname + @".WORKROOM WHERE BELONGING_FLOOR = '" + floorNum + @"';";
                    MySqlCommand com = new MySqlCommand(query, connection);
                    MySqlDataReader red = com.ExecuteReader();

                    //Read through database and get workroom information. Add that information to the list.
                    while (red.Read())
                    {
                        Workroom tempRoom = new Workroom
                        {
                            floor = red.GetInt32("BELONGING_FLOOR"),
                            number = red.GetInt32("WORKROOM_NO"),
                            size = red.GetInt32("ROOM_SIZE")
                        };

                        toReturn.Add(tempRoom);
                    }
                }
                catch (Exception a)
                {
                    Console.WriteLine("Issue occurred when retrieving workroom information from database.");
                    Console.WriteLine(a.Message);
                }
                finally
                {
                    closeConnection();
                }

                return toReturn;
            }
            else
            {
                throw new Exception("Issue connecting to database. Could not retrieve workroom information.");
            }
        }

        /// <summary>
        /// This function attempts to store a workroom booking in the database.
        /// </summary>
        /// <param name="roomNum">Room number of workroom.</param>
        /// <param name="floorNum">Floor number of workroom.</param>
        /// <param name="username">Username of the user trying to book the workroom.</param>
        /// <param name="date">Date of when to book the workroom.</param>
        /// <returns>true if successful, false otherwise.</returns>
        public bool bookWorkroom(int roomNum, int floorNum, string username, DateTime date)
        {
            if(openConnection())
            {
                int result = 0;
                try
                {
                    string query = @"INSERT INTO " + dbname + @".WORKROOMBOOKINGS VALUES(" + floorNum + @", " + roomNum + @", '" + username + @"', '" + date.ToString(@"yyyy-MM-dd HH:mm:ss") + "');";

                    MySqlCommand com = new MySqlCommand(query, connection);
                    result = com.ExecuteNonQuery();
                }
                catch(Exception a)
                {
                    Console.WriteLine("Issue occurred when saving a booking to the database.");
                    Console.WriteLine(a.Message);
                }
                finally
                {
                    closeConnection();
                }
                
                if (result > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// This part of the class contains potentially useful attributes pertaining to workrooms.
    /// </summary>
    public partial class LibraryDatabase
    {
       
    }
}