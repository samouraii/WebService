using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ConnectionDb.classe
{
    [DataContract]
    public class Transaction
    {
        [DataMember]
        private int id;
        [DataMember]
        private Transaction achat;
        [DataMember]
        private int tva;
        [DataMember]
        private double prixhtv;
        [DataMember]
        private string voiture;
        [DataMember]
        private Client client;

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

        public Transaction Achat
        {
            get
            {
                return achat;
            }

            set
            {
                achat = value;
            }
        }

        public int Tva
        {
            get
            {
                return tva;
            }

            set
            {
                tva = value;
            }
        }

        public double Prixhtv
        {
            get
            {
                return prixhtv;
            }

            set
            {
                prixhtv = value;
            }
        }

        public Client Client
        {
            get
            {
                return client;
            }

            set
            {
                client = value;
            }
        }

        public string Voiture
        {
            get
            {
                return voiture;
            }

            set
            {
                voiture = value;
            }
        }
    }
}