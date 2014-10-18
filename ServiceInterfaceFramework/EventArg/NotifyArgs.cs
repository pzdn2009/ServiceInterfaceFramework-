using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceInterfaceFramework.EventArg
{
    public class NotifyArgs : EventArgs
    {
        public NotifyArgs(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
