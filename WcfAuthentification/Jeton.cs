using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WcfAuthentification.bdd;



namespace WcfAuthentification
{
    [DataContract]
    public class Jeton
    {
        [DataMember]
        private User user;
        [DataMember]
        private string key;
        [DataMember]
        private DateTime connexion;
        [DataMember]
        private Error erreur;

        public Jeton(User user)
        {
            this.user = user;
            connexion = new DateTime();
           key = hash.GetMd5Hash(user.username + connexion.ToString());
        }
        public Jeton(Error erreur)
        {
            this.erreur = erreur;
        }

        public User User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }

        public string Key
        {
            get
            {
                return key;
            }

            set
            {
                key = value;
            }
        }

        public DateTime Connexion
        {
            get
            {
                return connexion;
            }

            set
            {
                connexion = value;
            }
        }

        public Error Erreur
        {
            get
            {
                return erreur;
            }

            set
            {
                erreur = value;
            }
        }
    }
}