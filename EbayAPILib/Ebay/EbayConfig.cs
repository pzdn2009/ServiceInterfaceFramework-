using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbayAPILibrary
{
    public class EbayConfig : APIConfigBase
    {
        public string Developer { get; set; }
        public string Application { get; set; }
        public string Certificate { get; set; }
        public string RuName { get; set; }
    }
}
