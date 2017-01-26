using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfAuthentification.bdd;

namespace WcfAuthentification
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAuthentification" in both code and config file together.
    [ServiceContract]
    public interface IAuthentification
    {

        [OperationContract]
         Jeton login(User user);
      
        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
  
}
