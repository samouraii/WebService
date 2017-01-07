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
        private string numTva;

        //private string typeDeVente;

      

        public int Id
        {
            get
            {
                return id;
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

        public string NumTva
        {
            get
            {
                return numTva;
            }

            set
            {
                numTva = value;
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
    }
}