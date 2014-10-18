using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ServiceInterfaceFramework.WebReq
{
    public class CertifactionSwitch
    {
        public static void On()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; };
        }

       
    }
}
