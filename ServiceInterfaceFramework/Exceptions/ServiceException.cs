using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ServiceInterfaceFramework.Exceptions
{
    [Serializable]
    public class ServiceException : ApplicationException
    {
        public ServiceException()
        {

        }

        public ServiceException(string message)
            : base(message)
        {

        }

        public ServiceException(string message, Exception innerException) :
            base(message, innerException)
        {

        }

        public ServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }

}
