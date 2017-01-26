using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;


namespace ConnectionDb.classe
{
    [DataContract]
    public class Client
    {
        [DataMember]
        private int id; //unique
        [DataMember]
        private int numdossier;
        [DataMember]
        private string nom;
        [DataMember]
        private string numtva;
        [DataMember]
        private User[] user;

        //private string typeDeVente;



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

        public string Numtva
        {
            get
            {
                return numtva;
            }

            set
            {
                numtva = value;
            }
        }

      

        public int Numdossier
        {
            get
            {
                return numdossier;
            }

            set
            {
                numdossier = value;
            }
        }

        public User[] User
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
    }
}