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
using ConnectionDb.classe;

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

        public static Error insert(object obj)
        {

           
            // récupération du tuype de propriete et les différentes variable;
                      
            Type type = obj.GetType();
            // PropertyInfo method = type.GetProperty("Nom");
            PropertyInfo[] propriete = type.GetProperties();
            string variable = "", value = "";
            int counter = 0;
            List<String> Params = new List<String>();
            Boolean update = false;

            PropertyInfo methodId = type.GetProperty("Id");
            string id = (String)methodId.GetValue(obj).ToString();
            int idint = 0;
            int.TryParse(id, out idint);
            if (id != null && idint != 0) {
                // recherche si l'id est pas déja dans la bdd vérification en plus
             
                update = true;
            }
            foreach (PropertyInfo i in propriete)
            {
                if (i.Name.ToLower() != "id")
                {

                    
                    variable += i.Name.ToLower();
                    value += "@" + i.Name;
                    Params.Add(i.Name);
                    if (update)
                    {
                        variable +="="+value;
                        value = "";
                    }

                    //value += "'"+(String)method.GetValue(obj).ToString()+"'";
                    if (propriete.Length - 1 > counter )
                    {
                        variable += " , ";
                        if (!update) value += ",";
                    }                    
                }
               

                counter++;
            }



            string query;
            if (update) query = "UPDATE " + type.Name.ToLower() + " SET "+variable +" WHERE id ='"+id+"'";
            else query = "INSERT INTO " + type.Name.ToLower() + " (" + variable + ") VALUES(" + value + ");";
            Error erreur = new Error(1, "Succes"); 
            if (OpenConnection() == true)
            {
                string tutu = "";
                try {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd = connection.CreateCommand();
                    cmd.CommandText = query;
                    foreach (string tmp in Params)
                    {
                        PropertyInfo method = type.GetProperty(tmp);
                        //cmd.Parameters.Add("@" + tmp, (String)method.GetValue(obj).ToString());
                        
                      /*  
                        Type o = methode.gettype .. 
                        object test = "";
                        switch (o.GetType().ToString())
                        {
                            case ("Int32"):
                                test = (int)method.GetValue(obj);
                                break;
                            default:
                                test = (String)method.GetValue(obj).ToString();
                                break;
                        }*/
                        string test = (String)method.GetValue(obj).ToString();
                        cmd.Parameters.AddWithValue("@"+tmp,test);
                       // method.SetValue()
                    }

                    //Execute command

                    tutu = cmd.CommandText + "";
                    cmd.Prepare();
                    
                    cmd.ExecuteNonQuery();
                   
                }

                catch (Exception e)
                {
                    erreur =  new Error(0, "insertion impossible " + tutu);
                }
                //close connection
                finally{
                    CloseConnection();
                }


                return erreur;
            }
            return new Error(404, "Erreur non trouver");
        }



        public static Error delete(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo method = type.GetProperty("Id");
            
             string value = (String)method.GetValue(obj).ToString();
            if (value == null) return new Error(5,"pas de champ dans l'object, suprresion impossible");
            string query = "DELETE FROM " + type.Name.ToLower() +" WHERE id='"+value+"'";
            try {
                if (OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();                   
                }
            }
            catch(Exception e)
            {
                return new Error(500,"Une erreur inconnue est arrivé sur le serveur");
            }
            finally
            {
                CloseConnection();
            }

            return new Error(200, "Suppression réussie"); 
        }


        public static List<object> select(object obj)
        {
            Error err = new Error(1, "Selection Faite");
            List<object> test = new List<object>();
            try
            {
                Type type = obj.GetType();
                string query = "SELECT * From " + type.Name.ToLower();
               
                

                if (OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        object monObject = Activator.CreateInstance(type);

                        for (int c = 0; dataReader.FieldCount > c; c++)
                        {
                            string text = dataReader.GetName(c);
                            PropertyInfo method = type.GetProperty(text[0].ToString().ToUpper() + text.Remove(0,1));
                            method.SetValue(monObject, dataReader[c]);

                            
                        }
                        test.Add(monObject);
                    }

                    
                }
            }
            catch(Exception e)
            {
                err = new Error(3, "Impossible de selectionner");
            }
            finally
            {
                CloseConnection();
            }

            return test ;
        }


        }


}