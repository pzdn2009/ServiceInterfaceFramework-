using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ServiceInterfaceFramework.Exceptions
{
    public class ServiceInterfaceFrameworkException : ApplicationException
    {
        public ServiceInterfaceFrameworkException()
        {

        }
        public ServiceInterfaceFrameworkException(string message)
            : base(message)
        {

        }
        public ServiceInterfaceFrameworkException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
        public ServiceInterfaceFrameworkException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
