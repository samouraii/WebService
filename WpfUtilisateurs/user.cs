using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WpfUtilisateurs.Bdd;
namespace WpfUtilisateurs
{

    [DataContract]
    public class user
    {

        private string nom;
        
        [DataMember]
        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                nom = value;
            }
        }
    }
}