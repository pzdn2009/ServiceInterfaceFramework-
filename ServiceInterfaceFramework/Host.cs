using ServiceInterfaceFramework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading;

namespace ServiceInterfaceFramework
{
    class Host
    {
        public static ServiceHost HostService(Type concreteService)
        {
            ServiceHost ebayAPIClientHost = null;
            TryCatchBlock.TrycatchAndLog(() =>
            {
                ebayAPIClientHost = new ServiceHost(concreteService);
                //绑定服务行为
                ServiceMetadataBehavior behavior = ebayAPIClientHost.Description.Behaviors.
                    Find<ServiceMetadataBehavior>();
                {
                    if (behavior == null)
                    {
                        behavior = new ServiceMetadataBehavior();
                        behavior.HttpGetEnabled = true;
                        ebayAPIClientHost.Description.Behaviors.Add(behavior);
                    }
                    else
                    {
                        behavior.HttpGetEnabled = true;
                    }
                }

                //启动事件
                ebayAPIClientHost.Opened += delegate
                {
                    //LogHelper.Debug("ebayAPIClientHost-->终结点为：" + ebayAPIClientHost.Description.Endpoints.FirstOrDefault().Address);
                    //LogHelper.Debug("ebayAPIClientHost-->服务启动，开始监听：" + ebayAPIClientHost.Description.ConfigurationName);
                    Console.WriteLine("ebayAPIClientHost-->终结点为：" + ebayAPIClientHost.Description.Endpoints.FirstOrDefault().Address);
                    Console.WriteLine("ebayAPIClientHost-->服务启动，开始监听：" + ebayAPIClientHost.Description.ConfigurationName);
                };

                Thread th = new Thread(ebayAPIClientHost.Open);
                th.Start();
            });

            return ebayAPIClientHost;
        }

        public static void Close(ServiceHost host)
        {
            TryCatchBlock.TrycatchAndLog(() =>
            {
                host.Close();
            });

            host.Abort();
        }
    }
}
