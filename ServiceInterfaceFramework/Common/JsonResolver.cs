using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace ServiceInterfaceFramework.Common
{
    public class JsonResolver
    {
        public string Serialize<T>(T obj)
        {
            var jser = new JavaScriptSerializer();
            return jser.Serialize(obj);
        }

        public T Deserialize<T>(string str)
        {
            var jser = new JavaScriptSerializer();
            return jser.Deserialize<T>(str);
        }
    }
}
