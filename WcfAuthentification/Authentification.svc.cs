using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfAuthentification.bdd;

namespace WcfAuthentification
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Authentification" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Authentification.svc or Authentification.svc.cs at the Solution Explorer and start debugging.
    public class Authentification : IAuthentification
    {
        private List<Jeton> jetons;
        public Authentification()
        {
            jetons = new List<Jeton>();
        }
        /*=============================================
              Gestion vérification de connexion
            =======================================
            */

        public Jeton login(User user)
        {
            bdd.Service1Client client = new Service1Client();
            User user2 = client.selectUser(user);
            user.mdp = hash.GetMd5Hash(user.mdp + user2.salt);
            if (user.username == user2.username && user.mdp == user2.mdp)
            {
                user2.salt = "";
                user2.mdp = "";
                Jeton jeton = new Jeton(user2);
                jetons.Add(jeton);
                return jeton;
            }
            Error e = new Error();
            
            e.message = "bad Credential " + user2.mdp + " " + user.salt;
            e.code = 900;
            return new Jeton(e);
        }
    }
}
