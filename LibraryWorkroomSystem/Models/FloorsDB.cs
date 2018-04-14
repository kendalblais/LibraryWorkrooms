using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWorkroomSystem.Models.Database
{
    public partial class LibraryDatabase
    {
        /// <summary>
        /// Adds a floor to the library system
        /// </summary>
        /// <param name="floor_no"> floor number to be added to the system</param>
        /// <returns> response message about status </returns>
        public string addFloor(int floor_no) {
            string response = "";

       
            string query = @"INSERT INTO " + dbname + ".floor (floor_no) " +
                "VALUES('" + floor_no + "');";

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

        /// <summary>
        /// Retrieves all Floors in the system 
        /// </summary>
        /// <returns> Floors which contains a list of each Floor </returns>
        public Floors getFloors()
        {

            Floors floorList = new Floors();

            string query = @"SELECT * FROM " + dbname + @".floor;";

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Floor floor = new Floor();
                        floor.floor_no = dataReader.GetInt32("floor_no");
                        floor.no_of_workrooms = dataReader.GetInt32("number_of_workrooms");

                        floorList.list.Add(floor);

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
            return floorList;

        }
    }
}