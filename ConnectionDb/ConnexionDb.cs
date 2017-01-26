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
        /*=============================================
                  Initialisation de la connextion avec les mdps ...
                  systeme de jeton sur la connexion, si deja connecter, retourne l'object.
              =======================================
              */
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
        /*=============================================
               Vérification de la connection de l'utilisateur
            =======================================
            */
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
        /*=============================================
                Fermeture de la connexion
            =======================================
            */
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

        /*=============================================
                Gestion de l'instert automatique en 3 cas, cas simple data string ou premitive
                cas 2 juste un autre object 
                cas 3 tableau d'object.
            =======================================
            */

        public static Error insert(object obj)
        {

           
            // récupération du tuype de propriete et les différentes variable;
                      
            Type type = obj.GetType();
            // PropertyInfo method = type.GetProperty("Nom");
            PropertyInfo[] propriete = type.GetProperties();
            string variable = "", value = "";
            int counter = 0;
            List<String> Params = new List<String>();
            List<String> Values = new List<String>();
            List<String> Querys = new List<String>();
            Boolean update = false;

            PropertyInfo methodId = type.GetProperty("Id");
            string id = (String)methodId.GetValue(obj).ToString();
            int idint = 0;
            int.TryParse(id, out idint);
            if (id != null && idint != 0) {
                // recherche si l'id est pas déja dans la bdd vérification en plus
             
                update = true;
            }
            /*=============================================
               Parcour tout les champs de l'object
            =======================================
            */
            foreach (PropertyInfo i in propriete)
            {
                if (i.Name.ToLower() != "id")
                {
                    /*=============================================
                cas 1
            =======================================
            */

                    if (!i.PropertyType.IsArray && (i.PropertyType.IsPrimitive || i.PropertyType == typeof(string)))
                    {
                        if (counter > 0)
                        {
                            variable += " , ";
                            if (!update) value += ",";
                        }
                        try {
                            variable += i.Name.ToLower();
                            value += "@" + i.Name;
                            Params.Add(i.Name);
                            Values.Add((String)i.GetValue(obj).ToString());
                        }
                        catch (Exception e)
                        {
                          
                            return new Error(300,"impoosible d'ecrire"); 
                        }
                        if (update)
                        {
                            variable += "=" + value;
                            value = "";
                        }

                        //value += "'"+(String)method.GetValue(obj).ToString()+"'";
                        counter++;
                    }
                    /*=============================================
                cas 2
            =======================================
            */
                    else if (!i.PropertyType.IsArray)
                    {
                        try {
                            object tabObj = (object)i.GetValue(obj);
                            if (tabObj != null)
                            {
                                Type type2 = tabObj.GetType();
                                PropertyInfo method2 = type2.GetProperty("Id");
                                int idv = (int)method2.GetValue(tabObj);
                                if (counter > 0)
                                {
                                    variable += " , ";
                                    if (!update) value += ",";
                                }
                                variable += i.Name.ToLower();
                                value += "@" + i.Name;
                                Params.Add(i.Name);
                                Values.Add(idv + "");
                                if (update)
                                {
                                    variable += "=" + value;
                                    value = "";
                                }
                            }
                        }
                        catch (Exception e)
                        {

                        }

                    }
                    /*=============================================
                cas 3
            =======================================
            */
                    else if (i.PropertyType.IsArray && ! update)
                    {
                        object[] tabObj = (object[])i.GetValue(obj);
                        
                        if (tabObj != null && !update)
                        {
                            Type type2 = tabObj[0].GetType();
                            PropertyInfo method2 = type2.GetProperty(type.Name[0].ToString().ToUpper() + type.Name.Remove(0, 1));
                            User[] u =(User[]) i.GetValue(obj);
                            
                            if (method2.PropertyType.IsArray)
                            {
                              
                                
                                Querys.Add("INSERT INTO " + getBDD(type.Name.ToLower(),type2.Name.ToLower()) +" ( id"+ type.Name.ToLower() + ", id"+ type2.Name.ToLower()+") VALUES (@ID,"+u[0].Id+")");
                            }
                        }
                    }
                    else
                    {
                      

                    }              
                }
               

                
            }

            /*=============================================
               gestion de l'insertion avec requete preparer
            =======================================
            */

            string query;
            if (update) query = "UPDATE " + type.Name.ToLower() + " SET "+variable +" WHERE id ='"+id+"'";
            else query = "INSERT INTO " + type.Name.ToLower() + " (" + variable + ") VALUES(" + value + "); SELECT @@IDENTITY AS LastId;";
            Error erreur = new Error(1, "Succes");
            string errInst = "";
            if (OpenConnection() == true)
            {
                
                try {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd = connection.CreateCommand();
                    cmd.CommandText = query;
                    int cpt = 0;
                    foreach (string tmp in Params)
                    {
                       // PropertyInfo method = type.GetProperty(tmp);
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
                        
                        cmd.Parameters.AddWithValue("@"+tmp,Values[cpt]);
                        cpt++;
                       // method.SetValue()
                    }

                    //Execute command

                    errInst = cmd.CommandText + "";
                    cmd.Prepare();
                    int m_lastid = -1;
                    object temps = new object(); 
                    if (update  ) cmd.ExecuteNonQuery();
                    else  temps =  cmd.ExecuteScalar();
                   int.TryParse(temps.ToString(), out m_lastid);
                    if(m_lastid != -1)
                    {
                        foreach (string sql in Querys)
                        {
                            MySqlCommand cmd2 = new MySqlCommand(sql, connection);
                            cmd2 = connection.CreateCommand();

                            cmd2.CommandText = sql;
                            cmd2.Parameters.AddWithValue("@ID", m_lastid);
                            cmd2.ExecuteNonQuery();
                        }
                    }
                 
                   
                }

                catch (Exception e)
                {
                    erreur =  new Error(0, "insertion impossible " + errInst);
                }
                //close connection
                finally{
                    CloseConnection();
                }


                return erreur;
            }
            return new Error(404, "Erreur non trouver");
        }


        /*=============================================
               Gestion de la suppression
            =======================================
            */
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

        /*=============================================
               Gestion de la selection, et tu tries, si nous devons filtrer les datas.
            =======================================
            */
        public static List<object> select(object obj, object obj2 =null)
        {
            Error err = new Error(1, "Selection Faite");
            List<object> test = new List<object>();
            try
            {
                Type type = obj.GetType();
                int idv = -1;
                string query = "";
               
                if (obj2 != null)
                {
                    Type type2 = obj2.GetType();
               
                    
                    PropertyInfo method2 = type2.GetProperty("Id");
                    idv = (int)method2.GetValue(obj2);
                    query = "SELECT * From " + type.Name.ToLower() + " WHERE " + type2.Name.ToLower() + " = "+ idv +" ;";

                }
                else {
                    query = "SELECT * From " + type.Name.ToLower() +" ;";
                }
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
                            PropertyInfo method = type.GetProperty(text[0].ToString().ToUpper() + text.Remove(0, 1));
                            Object to = dataReader[c];
                            Object to2 = dataReader[c].ToString();
                            try {
                                method.SetValue(monObject, dataReader[c]);
                            }
                            catch(Exception e)
                            {

                            }
                                                       
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

        /*=============================================
                Pour 1 utilisateur la gestion de connexion
            =======================================
            */
        public static User selectUser(User user)
        {
            Error err = new Error(1, "Selection Faite");
            User user2 = new User();
            Type type = typeof(User);
            try
            {
               
                string query = "SELECT * From user where username ='"+user.Username+"'" ;

                if (OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {

                        user2.Username = (string)dataReader["username"];
                        user2.Id = (int)dataReader["id"];
                        user2.Mdp = (string)dataReader["mdp"];
                       user2.Salt= (string)dataReader["salt"];

                    }
                }
            }
            catch (Exception e)
            {
                err = new Error(3, "Impossible de selectionner");
            }
            finally
            {
                CloseConnection();
            }

            return user2;

        }


        private static string getBDD(string s1, string s2)
        {
            int cpt = 0;
            foreach (char s in s1)
            {
                if (s < s2[cpt]) return s1 + "to" + s2;
                else if (s > s2[cpt]) return s2 + "to" + s1;
                else cpt++;
            }
            return s1 + "to" + s2;
        }


    }


}