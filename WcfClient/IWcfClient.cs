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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IWcfClient
    {

        [OperationContract]
        Error insert(Client value);
        [OperationContract]
        Error update(Client value);
        [OperationContract]
        Error delete(Client value);
        [OperationContract]
        Client[] select();

        [OperationContract]
        Error insertT(Transaction value);
        [OperationContract]
        Error updateT(Transaction value);
        [OperationContract]
        Error deleteT(Transaction value);
        [OperationContract]
        [ServiceKnownType(typeof(Client))]
        Transaction[] selectT(object obj = null);


        // TODO: Add your service operations here
    }



}
