using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ServiceInterfaceFramework.Service
{
    public class ServiceStatusInfo
    {
        public string ThreadName
        {
            get;
            set;
        }

        public ThreadState ThreadState
        {
            get;
            set;
        }
    }
}
