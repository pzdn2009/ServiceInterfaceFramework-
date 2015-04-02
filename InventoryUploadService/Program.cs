using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InventoryUploadService
{
    /// <summary>
    /// Reference : http://www.codeproject.com/Articles/17704/WCF-Duplex-Operations-and-UI-Threads
    /// </summary>
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Initializing Service...");

            // The service configuration is loaded from app.config
            using (ServiceHost host = new ServiceHost(typeof(InventoryChangeService)))
            {
                host.Open();

                Console.WriteLine("Service is ready for requests.  Press any key to close service.");
                Console.WriteLine();
                Console.Read();

                Console.WriteLine("Closing service...");
            }
        }
    }
}
