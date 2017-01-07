using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCFUser.bdd;
using WCFUser.classe;




namespace WCFUser
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class wcfUser : WcfUser
    {
      
        public Error insert(User value)
        {
            Error erreur;
            bdd.Service1Client client;
            try {
                client = new Service1Client();
                TimeSpan t = new TimeSpan();
                value.salt = hash.GetMd5Hash(value.username + t.Days + "" + t.Hours + "" + t.Minutes + t.Seconds);
                erreur = client.insert(value);
            }
            catch(Exception e)
            {
                erreur = new Error();
                erreur.code = 2;
                erreur.message = "Impossible de créer le salt";
              
            }
            return erreur;
        }

       

        public User[] select()
        {
            bdd.Service1Client client;
            User[] t = null;
            try
            {
                client = new Service1Client();

                 object[] y = client.select(new User());
                t = new User[y.Count()];
                int compteur = 0;
                foreach(object r in y)
                {
                    t[compteur] = (User) r;
                    compteur++;
                }
               
                int i = 0;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
          return t;
        }

        public Error update(User value)
        {
            Error erreur;
            bdd.Service1Client client;
            try
            {
                client = new Service1Client();
                
                
                erreur = client.insert(value);
            }
            catch (Exception e)
            {
                erreur = new Error();
                erreur.code = 2;
                erreur.message = "Impossible de mettre à jour l'utilisateur";

            }
            return erreur;
        }
        public Error delete(User value)
        {
            Error erreur;
            bdd.Service1Client client;
            try
            {
                client = new Service1Client();


                erreur = client.delete(value);

            }
            catch(Exception e)
            {
                erreur = new Error();
                erreur.code = 2;
                erreur.message = "Impossible de supprimer l'utilisateur";
            }
            return erreur;
        }
    }
}
