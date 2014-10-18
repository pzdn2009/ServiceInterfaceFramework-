using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceInterfaceFramework.Service;
using ServiceInterfaceFramework.Common;

namespace ServiceInterfaceFramework
{
    public static class ServiceStarter
    {
        public static void Initialize()
        {

        }

        public static void StartAll()
        {
            TryCatchBlock.TrycatchAndLog(() =>
            {
                LogHelper.Configure();
                //var configs = DeliveryConfigManager.GetBusConfigs();
                //foreach (var config in configs)
                //{
                //    ServiceLaucher.RegisterAndStartService("", null);
                //}
            });
        }

        public static void StopAll()
        {
            ServiceLaucher.StopAll();
        }
    }
}
