using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryWorkroomSystem.Models.Database
{
    public partial class LibraryDatabase
    {

        //program methods for the database here

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