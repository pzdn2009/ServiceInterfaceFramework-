﻿using ServiceInterfaceFramework.EventArg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceInterfaceFramework
{
    public interface INotifyMessage
    {
        event EventHandler<NotifyArgs> Sth;
        void Notify();
    }
}
