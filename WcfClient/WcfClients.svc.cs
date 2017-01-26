using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfClient.bdd;
namespace WcfClient
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class WcfClients : IWcfClient
    {
        public Error insert(Client value)
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
                erreur.message = "Impossible d'inserer";

            }
            return erreur;
        }



        public Client[] select()
        {
            bdd.Service1Client client;
            Client[] t = null;
            try
            {
                client = new bdd.Service1Client();

                object[] y = client.select(new Client(),null);
                t = new Client[y.Count()];
                int compteur = 0;
                foreach (object r in y)
                {
                    t[compteur] = (Client)r;
                    compteur++;
                }

                int i = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return t;
        }

        public Error update(Client value)
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
                erreur.message = "Impossible de mettre à jour le client";

            }
            return erreur;
        }
        public Error delete(Client value)
        {
            Error erreur;
            bdd.Service1Client client;
            try
            {
                client = new Service1Client();


                erreur = client.delete(value);

            }
            catch (Exception e)
            {
                erreur = new Error();
                erreur.code = 2;
                erreur.message = "Impossible de supprimer le client";
            }
            return erreur;
        }

        public Error insertT(Transaction value)
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
                erreur.message = "Impossible d'inserer";

            }
            return erreur;
        }

        public Error updateT(Transaction value)
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
                erreur.message = "Impossible de mettre à jour la transaction";

            }
            return erreur;
        }

        public Error deleteT(Transaction value)
        {
            Error erreur;
            bdd.Service1Client client;
            try
            {
                client = new Service1Client();


                erreur = client.delete(value);

            }
            catch (Exception e)
            {
                erreur = new Error();
                erreur.code = 2;
                erreur.message = "Impossible de supprimer le client";
            }
            return erreur;
        }

        public Transaction[] selectT(object obj2 =null)
        {
            bdd.Service1Client client;
            Transaction[] t = null;
            try
            {
                client = new bdd.Service1Client();

                object[] y = client.select(new Transaction(),obj2);
                t = new Transaction[y.Count()];
                int compteur = 0;
                foreach (object r in y)
                {
                    t[compteur] = (Transaction)r;
                    compteur++;
                }

                int i = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return t;
        }
    }

}
