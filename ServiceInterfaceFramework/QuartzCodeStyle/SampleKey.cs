using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceInterfaceFramework.QuartzCodeStyle
{
    [Serializable]
    public sealed class SampleKey : Key<SampleKey>
    {
        public SampleKey(string name)
            : base(name, null)
        {
        }

        public SampleKey(string name, string group)
            : base(name, group)
        {
        }

        public static SampleKey Create(string name)
        {
            return new SampleKey(name, null);
        }

        public static SampleKey Create(string name, string group)
        {
            return new SampleKey(name, group);
        }
    }
}
