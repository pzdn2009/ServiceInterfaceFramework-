using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceInterfaceFramework.Service
{
    public interface IService
    {
        void Start();
        void Stop();

        int Interval { get; set; }
        bool DoWorkAtStart { get; set; }
        bool RunOnlyOnce { get; set; }

        string Name { get; set; }
    }
}
