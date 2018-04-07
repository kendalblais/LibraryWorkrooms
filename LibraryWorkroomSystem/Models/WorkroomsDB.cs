using MySql.Data.MySqlClient;

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
       public int getNumberOfFloors()
        {
            //This if statement prevents unnecessary database queries.
            if(numberOfFloors != -1)
            {
                return numberOfFloors;
            }

            string query = @"SELECT COUNT(*) FROM " + dbname + @" .FLOOR;";

            if(openConnection())
            {
                MySqlCommand com = new MySqlCommand(query, connection);
                MySqlDataReader red = com.ExecuteReader();
                red.Read();
                numberOfFloors = red.GetInt16(0);
                closeConnection();
                return numberOfFloors;
            }
            else
            {
                throw new Exception("Issue connecting to database. Could not retrieve number of floors.");
            }  
        }
    }

    /// <summary>
    /// This part of the class contains potentially useful attributes pertaining to workrooms.
    /// </summary>
    public partial class LibraryDatabase
    {
        /// <summary>
        /// This attribute represents the number of floors that the library contains.
        /// The default value is negative 1, indicating that the value hasn't been set.
        /// </summary>
        private int numberOfFloors = -1;
    }
}