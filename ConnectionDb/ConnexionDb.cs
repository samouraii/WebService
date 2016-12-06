using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Runtime.Serialization;
using System.Reflection;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

namespace ConnectionDb
{
   
  
    public class ConnexionDb
    {
        private static MySqlConnection connection;
        private static string server;
        private static string database;
        private static string uid;
        private static string password;

        private ConnexionDb()
        {

        }
      
       public static MySqlConnection initialise()
        {
            if (connection == null)
            {
                server = "localhost";
                database = "techinfo";
                uid = "root";
                password = "";
                string connectionString;
                connectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

                connection = new MySqlConnection(connectionString);
            }
           
                return connection;
            
        }

        private static bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        private static bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool insert(object obj)
        {

           
            // récupération du tuype de propriete et les différentes variable;
                      
            Type type = obj.GetType();
            // PropertyInfo method = type.GetProperty("Nom");
            PropertyInfo[] propriete = type.GetProperties();
            string variable = "", value = "";
            int counter = 0;
            List<String> Params = new List<String>();
            foreach (PropertyInfo i in propriete)
            {
                if (i.Name.ToLower() != "id")
                {
                     

                    variable += i.Name.ToLower();
                    value += "@" + i.Name;
                    Params.Add(i.Name);
                   
                    //value += "'"+(String)method.GetValue(obj).ToString()+"'";
                    if (propriete.Length - 1 > counter)
                    {
                        variable += ",";
                        value += ",";
                    }                    
                }
                counter++;
            }

            
            string query = "INSERT INTO "+type.Name.ToLower()+" ("+variable+") VALUES("+value+");";
            
            if (OpenConnection() == true)
            {
                try {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.CommandText = query;
                    foreach (string tmp in Params)
                    {
                        PropertyInfo method = type.GetProperty(tmp);
                        cmd.Parameters.AddWithValue("@"+tmp, (String)method.GetValue(obj).ToString());
                    }

                    //Execute command
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    return false;
                }
                //close connection
                finally{
                    CloseConnection();
                }
               
                return true;
            }
            return false;
        }



        public static bool delete(object obj)
        {

            return true;
        }

        }

}