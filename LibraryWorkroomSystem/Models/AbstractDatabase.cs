using LibraryWorkroomSystem.Models.Debugging;

using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;
using System.Threading;

namespace LibraryWorkroomSystem.Models.Database
{
    /// <summary>
    /// This class is used as a base class for the creation and deletion of a database.
    /// To use this class you will need to implement the databaseName and tables properties.
    /// It is recommended that the inhereting class be a singleton.
    /// </summary>
    public abstract partial class AbstractDatabase
    {
        /// <summary>
        /// Creates the connection object, creates a mutex, and attempts to create the database if it does not exist already
        /// </summary>
        protected AbstractDatabase()
        {
            mutex = new Mutex(false);
            connection = new MySqlConnection("SERVER=localhost;DATABASE=mysql;UID=" + UID + ";AUTO ENLIST=false;PASSWORD=" + Password);
            createDB();
            addForeignKeys();
        }

        /// <summary>
        /// Creates the database, if it does not already exist
        /// </summary>
        public void createDB()
        {
            string commandString;
            MySqlCommand command;
            commandString = "CREATE DATABASE " + databaseName + ";";

            if(connection == null)
            {
                connection = new MySqlConnection("SERVER=localhost;DATABASE=mysql;UID=" + UID + ";AUTO ENLIST=false;PASSWORD=" + Password);
            }

            //connection.
            if (openConnection() == true)
            {
                //First try to create the actual database
                try
                {
                    command = new MySqlCommand(commandString, connection);
                    command.ExecuteNonQuery();
                    Debug.consoleMsg("Successfully created database " + databaseName);
                }
                catch (MySqlException e)
                {
                    if (e.Number == 1007)//Database already exists, no need to continure further
                    {
                        Debug.consoleMsg("Database already exists.");
                        closeConnection();
                        connection = new MySqlConnection("SERVER=localhost;DATABASE=" + databaseName + ";UID=" + UID + ";AUTO ENLIST=false;PASSWORD=" + Password);
                        return;
                    }
                    Debug.consoleMsg("Unable to create database"
                        + databaseName + " Error: " + e.Number + e.Message);
                    closeConnection();
                    return;
                }

                //Then try to create each of the tables in the database
                foreach (Table table in tables)
                {
                    try
                    {
                        commandString = table.getCreateCommand();
                        command = new MySqlCommand(commandString, connection);
                        command.ExecuteNonQuery();
                        Debug.consoleMsg("Successfully created the table "
                            + table.databaseName + "." + table.tableName);

                    }
                    catch (MySqlException e)
                    {
                        Debug.consoleMsg("Unable to create table "
                            + table.databaseName + "." + table.tableName
                            + " Error: " + e.Number + e.Message);
                    }
                }

                closeConnection();
                connection = new MySqlConnection("SERVER=localhost;DATABASE=" + databaseName + ";UID=" + UID + ";AUTO ENLIST=false;PASSWORD=" + Password);
            }
        }

        /// <summary>
        /// Deletes the database if it exists
        /// </summary>
        public void deleteDatabase()
        {
            if (openConnection() == true)
            {
                string commandString;
                MySqlCommand command;
                foreach (Table table in tables)
                {
                    try
                    {
                        commandString = table.getDropCommand();
                        command = new MySqlCommand(commandString, connection);
                        command.ExecuteNonQuery();
                        Debug.consoleMsg("Successfully deleted table "
                            + table.databaseName + "." + table.tableName);
                    }
                    catch (MySqlException e)
                    {
                        Debug.consoleMsg("Unable to delete table "
                            + table.databaseName + "." + table.tableName
                            + " Error: " + e.Number + e.Message);
                    }
                }

                commandString = "DROP DATABASE " + databaseName + ";";
                command = new MySqlCommand(commandString, connection);
                try
                {
                    command.ExecuteNonQuery();
                    Debug.consoleMsg("Successfully deleted database " + databaseName);
                }
                catch (MySqlException e)
                {
                    Debug.consoleMsg("Unable to delete database " + databaseName
                        + " Error: " + e.Number + e.Message);
                }
                finally
                {
                    closeConnection();
                }
            }
        }

        /// <summary>
        /// Attempts to open a connection to the database
        /// This function must be called before using the connection object
        /// Any function that calls this function should also call closeConnection() when it it finished with the connection object.
        /// </summary>
        /// <returns>true if the connection was successful, false otherwise</returns>
        protected bool openConnection()
        {
            try
            {
                mutex.WaitOne();
                connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                mutex.ReleaseMutex();
                switch (e.Number)
                {
                    case 0:
                        Debug.consoleMsg("Cannot connect to database.");
                        break;
                    case 1045:
                        Debug.consoleMsg("Invalid username or password for database.");
                        break;
                    default:
                        Debug.consoleMsg("Cannot connect to database. Error code <" + e.Number + ">");
                        break;
                }
                return false;
            }
            catch (InvalidOperationException e)
            {
                if (e.Message.Equals("The connection is already open."))
                {
                    return true;
                }
                mutex.ReleaseMutex();
                return false;
            }
            catch (Exception e)
            {
                mutex.ReleaseMutex();
                return false;
            }
        }

        /// <summary>
        /// Attempts to close the connection with the database
        /// This function MUST be called when you are finished with the connection object
        /// If you do not, the mutex will prevent any further use of the database.
        /// </summary>
        /// <returns>true if successful, false otherwise</returns>
        protected bool closeConnection()
        {
            try
            {
                connection.Close();
                mutex.ReleaseMutex();
                return true;
            }
            catch (MySqlException e)
            {
                mutex.ReleaseMutex();
                Debug.consoleMsg("Could not close connection to database. Error message: " + e.Number + e.Message);
                return false;
            }
        }

        private void addForeignKeys()
        {
            List<string> modifications = new List<string>();
            modifications.Add("ALTER TABLE libraryworkroom.book " +
                "ADD CONSTRAINT FK_renter FOREIGN KEY(renter_username) REFERENCES libraryworkroom.user(username) ON DELETE CASCADE ON UPDATE CASCADE," +
                "ADD CONSTRAINT FK_floorB FOREIGN KEY(belonging_floor) REFERENCES libraryworkroom.floor(floor_no) ON DELETE CASCADE ON UPDATE CASCADE," +
                "ADD CONSTRAINT FK_bookkeeper FOREIGN KEY(bookkeeper_ID) REFERENCES libraryworkroom.employee(employee_ID) ON DELETE CASCADE ON UPDATE CASCADE;");

            modifications.Add("ALTER TABLE libraryworkroom.user " +
                "ADD CONSTRAINT FK_admin FOREIGN KEY(admin_ID) REFERENCES libraryworkroom.employee(employee_id) ON DELETE CASCADE ON UPDATE CASCADE;");

            modifications.Add("ALTER TABLE libraryworkroom.employee " +
                "ADD CONSTRAINT FK_user FOREIGN KEY(emp_username) REFERENCES libraryworkroom.user(username) ON DELETE CASCADE ON UPDATE CASCADE;");

            modifications.Add("ALTER TABLE libraryworkroom.users_in_programs " +
                "ADD CONSTRAINT FK_prog FOREIGN KEY(program_name) REFERENCES libraryworkroom.program(name) ON DELETE CASCADE ON UPDATE CASCADE," +
                "ADD CONSTRAINT FK_userP FOREIGN KEY(username) REFERENCES libraryworkroom.user(username) ON DELETE CASCADE ON UPDATE CASCADE;");

            modifications.Add("ALTER TABLE libraryworkroom.workroom " +
                "ADD CONSTRAINT FK_floorW FOREIGN KEY(belonging_floor) REFERENCES libraryworkroom.floor(floor_no) ON DELETE CASCADE ON UPDATE CASCADE," +
                "ADD CONSTRAINT FK_adminW FOREIGN KEY(admin_ID) REFERENCES libraryworkroom.employee(employee_id) ON DELETE CASCADE ON UPDATE CASCADE;");

            modifications.Add("ALTER TABLE libraryworkroom.program " +
                "ADD CONSTRAINT FK_teacher FOREIGN KEY(teacher_ID) REFERENCES libraryworkroom.employee(employee_id) ON DELETE CASCADE ON UPDATE CASCADE;");

            modifications.Add("ALTER TABLE libraryworkroom.workroombookings " +
                "ADD CONSTRAINT FK_flrN FOREIGN KEY(floor_no) REFERENCES libraryworkroom.floor(floor_no) ON DELETE CASCADE ON UPDATE CASCADE," +
                "ADD CONSTRAINT FK_WrkR FOREIGN KEY(workroom_no) REFERENCES libraryworkroom.workroom(workroom_no) ON DELETE CASCADE ON UPDATE CASCADE," +
                "ADD CONSTRAINT FK_Bker FOREIGN KEY(reserver_username) REFERENCES libraryworkroom.user(username) ON DELETE CASCADE ON UPDATE CASCADE;");
            



            try
            {
                foreach (string constraint in modifications)
                {
                    if (openConnection())
                    {
                        MySqlCommand command = new MySqlCommand(constraint, connection);
                        command.ExecuteNonQuery();

                        
                    }
                }
            }
            catch (MySqlException e)
            {
                Debug.consoleMsg("Unable to create foreign key references "
                      + e.Number + e.Message);
            }
            finally {
                closeConnection();
            }
        }
    }

    /// <summary>
    /// This portion of the class contains the member variables.
    /// </summary>
    public abstract partial class AbstractDatabase
    {
        /// <summary>
        /// This object is used for making all queries to the database.
        /// It should be opened and closed
        /// for each query made using the functions provided above.
        /// </summary>
        protected MySqlConnection connection;

        /// <summary>
        /// This is the username used to login to the database by the connection
        /// </summary>
        private const string UID = "root";

        /// <summary>
        /// This is the password used to login to the database by the connection
        /// </summary>
        //TODO: Change before deployment
        private const string Password = "WeLoveHim!97";

        /// <summary>
        /// This is the name of the database. This property must be defined by the inheriting class
        /// </summary>
        public abstract String databaseName { get; }

        /// <summary>
        /// This represents the tables in the database. The inheriting class must define and populate
        /// this property so that this class may properly create or delete the database
        /// </summary>
        protected abstract Table[] tables { get; }

        /// <summary>
        /// The mutex object used to ensure that only one process may access the database at any given time
        /// </summary>
        private Mutex mutex;
    }
}
