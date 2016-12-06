using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;


namespace ConnectionDb.classe
{
    [DataContract]
    public class User
    {
        [DataMember]
        private string username;
        [DataMember]
        private string mdp;
        [DataMember]
        private int id;
        [DataMember]
        private string salt;


        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public string Mdp
        {
            get
            {
                return mdp;
            }

            set
            {
                mdp = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Salt
        {
            get
            {
                return salt;
            }

           set
            {
                salt = value;
            }
        }


        public bool verification()
        {
            if (this.username != null && this.mdp != null)
            {
                if (this.username.Length >= 3 && this.mdp.Length >= 3)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}