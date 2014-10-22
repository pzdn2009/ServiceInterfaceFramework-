using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceInterfaceFramework.Configuration
{
    public class DemoConfig : ConfigBase
    {
        public string Name { get; set; }
    }

    public class DemoConfig2 : DemoConfig
    {
        public int? Age { get; set; }
    }
}
