﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCFUser.bdd;

namespace WCFUser
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface WcfUser
    {

        
        [OperationContract]
        Error insert(User value);
        [OperationContract]
        Error update(User value);
        [OperationContract]
        Error delete(User value);
        [OperationContract]
        User[] select();

      

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    
}
