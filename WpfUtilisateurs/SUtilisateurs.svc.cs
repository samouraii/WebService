using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;


namespace WpfUtilisateurs
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class SUtilisateurs : IUtilisateurs1
    {



        public bool createUser()
        {
            Bdd.Service1Client connexion = new Bdd.Service1Client();
          WpfUtilisateurs.Bdd.user t = new WpfUtilisateurs.Bdd.user();
            
            t.Nom = "oliver";
           
          //  connexion.insert(new Object());
            return connexion.insert(t);
        }
        public user personne()
        {
            user t = new user();

            t.Nom = "oliver";
            return t;
        }
    }
}
