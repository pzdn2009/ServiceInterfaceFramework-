using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceInterfaceFramework.WebReq
{
    public interface IPostString
    {
        string GetConstructPostString(string source);
        Dictionary<string, string> Attributes { get; set; }
    }
}
