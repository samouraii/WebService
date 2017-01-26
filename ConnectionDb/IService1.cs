using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ConnectionDb.classe;
namespace ConnectionDb
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

       
        [OperationContract]
        bool initialise();

        [OperationContract]
        [ServiceKnownType(typeof(User))]
        [ServiceKnownType(typeof(Client))]
        [ServiceKnownType(typeof(Transaction))]
        Error insert(object toto);

        [OperationContract]
        [ServiceKnownType(typeof(User))]
        [ServiceKnownType(typeof(Client))]
        [ServiceKnownType(typeof(Transaction))]
        object[] select(object toto, object obj2 = null);
        [OperationContract]
        [ServiceKnownType(typeof(User))]
        [ServiceKnownType(typeof(Client))]
        [ServiceKnownType(typeof(Transaction))]
        Error delete(object toto);
        [OperationContract]
        User selectUser(User user);
        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
   
}
