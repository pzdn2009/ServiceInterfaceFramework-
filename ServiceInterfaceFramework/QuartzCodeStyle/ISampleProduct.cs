using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceInterfaceFramework.QuartzCodeStyle
{
    public class ISampleProduct
    {
        public Type JobType { get; set; }
        public string Description { get; set; }
        public SampleKey Key { get; set; }
        public bool RequestsRecovery { get; set; }
        public bool Durable { get; set; }
    }
}
