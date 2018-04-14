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
        /// Retrieves all employees from the system
        /// </summary>
        /// <returns></returns>
        public List<string> getEmployees()
        {
            string query = @"SELECT * FROM " + dbname + @".employee WHERE position = 'employee';";

            List<string> list = new List<string>();
            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = command.ExecuteReader();


                    while (dataReader.Read())
                    {
                        list.Add(dataReader.GetString("employee_ID"));
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
            return list;
        }

        /// <summary>
        /// Adds a program to the system with the required information
        /// </summary>
        /// <param name="name"> name of program </param>
        /// <param name="description"> description of the program </param>
        /// <param name="date"> date the program takes place </param>
        /// <param name="startTime"> start time </param>
        /// <param name="endTime"> end time </param>
        /// <param name="teacherID"> the employee id of the teacher teaching the program </param>
        public void addProgram(string name, string description, string date, string startTime, string endTime, string teacherID)
        {
            string query = @"INSERT INTO " + dbname + ".program (name,description,date,start_time,end_time,teacher_ID) " +
                "VALUES('" + name + "','" + description + "','" + date + "','" + startTime + "','" + endTime + "'," + Int32.Parse(teacherID) + ");";

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

        /// <summary>
        /// retrieves a list of the names of all programs in the system
        /// </summary>
        /// <returns></returns>
        public string[] getPrograms()
        {
            string query = @"SELECT * FROM " + dbname + @".program;";

            string[] programs = null;
            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = command.ExecuteReader();

                    List<string> temp = new List<string>();
                    while (dataReader.Read())
                    {
                        temp.Add(dataReader.GetString("name"));
                    }
                    dataReader.Close();
                    programs = temp.ToArray();

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
            return programs;
        }

        /// <summary>
        /// Retrieves all the information about the desired program
        /// </summary>
        /// <param name="name"></param>
        /// <returns> instance of Program that contains all information for that program </returns>
        public Program getProgram(string name)
        {

            Program program = new Program();

            string query = @"SELECT * FROM " + dbname + @".program WHERE name='" +
                name + @"';";

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = command.ExecuteReader();


                    if (dataReader.Read())
                    {

                        program.name = dataReader.GetString("name");
                        program.description = dataReader.GetString("description");
                        program.date = dataReader.GetString("date").Substring(0,10);
                        program.startTime = dataReader.GetString("start_time");
                        program.endTime = dataReader.GetString("end_time");
                        program.teacherID = dataReader.GetInt32("teacher_ID");


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
            program.inProgram = isInProgram(name);
            return program;
        }

        /// <summary>
        /// Checks whether the currently logged in user is in the desired program yet 
        /// </summary>
        /// <param name="progName"></param>
        /// <returns> true or false </returns>
        public bool isInProgram(string progName)
        {
            string query = @"SELECT * FROM " + dbname + @".users_in_programs WHERE program_name='" +
                progName + @"' AND username = '" + Sessions.getUser() + "';";

            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                    {
                        return true;
                    }
                    else
                        return false;
                }
                catch (MySqlException e)
                {
                    return false;
                }
                catch (Exception e)
                {
                    return false;
                }
                finally
                {
                    closeConnection();
                }
            }

            return false;

        }

        /// <summary>
        /// registers the currently logged in user to the desired program
        /// </summary>
        /// <param name="programName"></param>
        public void registerForProgram(String programName)
        {
            string query = "INSERT INTO " + dbname + ".users_in_programs (program_name,username) " +
                "VALUES('" + programName + "','" + Sessions.getUser() + "');";

            if (openConnection() == true)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteReader();

                closeConnection();
            }
        }

        /// <summary>
        /// removes a program from the system
        /// </summary>
        /// <param name="progName"></param>
        public void deleteProgram(string progName)
        {
            string query = "DELETE FROM " + dbname + ".program WHERE name = '" +
                progName + "';";

            if (openConnection() == true)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteReader();

                closeConnection();
            }
        }

        /// <summary>
        /// Retrieves a list of the names of all programs the currently logged in user is registered for
        /// </summary>
        /// <returns></returns>
        public string[] retrieveRegisteredPrograms()
        {
            string query = @"SELECT * FROM " + dbname + @".users_in_programs WHERE username = '" +
                Sessions.getUser() + @"';";

            string[] programs = null;
            if (openConnection() == true)
            {
                try
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = command.ExecuteReader();

                    List<string> temp = new List<string>();
                    while (dataReader.Read())
                    {
                        temp.Add(dataReader.GetString("program_name"));
                    }
                    dataReader.Close();
                    programs = temp.ToArray();

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
            return programs;

        }

        /// <summary>
        /// Cancels the the currently logged in user's registration for the desired program 
        /// </summary>
        /// <param name="progName"> program to be cancelled </param>
        public void cancelRegistration(string progName)
        {
            string query = "DELETE FROM " + dbname + ".users_in_programs WHERE program_name = '" +
               progName + "' AND username = '" + Sessions.getUser() + @"';";

            if (openConnection() == true)
            {

                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteReader();

                closeConnection();
            }
        }


                
    }
}