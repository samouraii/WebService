using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfUser.BDD;

namespace WcfUser
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
       
        public bool addUser(User utilisateur)
        {
            if (utilisateur.username.Length >= 3 && utilisateur.mdp.Length >= 3)
            {
                BDD.Service1Client t = new Service1Client();
                utilisateur.salt = hash.GetMd5Hash(utilisateur.id + utilisateur.username);
                

                t.insert(utilisateur);
                
                
                return true;
            }
            return false;
        }
    }
}
