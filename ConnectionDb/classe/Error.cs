using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ConnectionDb.classe
{
    [DataContract]
    public class Error
    {
        [DataMember]
        private int code;
        [DataMember]
        private string message;
        public Error(int code, string message)
        {
            this.code = code;
            this.message = message;
        }

        public int Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }
    }
}