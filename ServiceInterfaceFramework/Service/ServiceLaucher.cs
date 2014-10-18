using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceInterfaceFramework.Common;
using ServiceInterfaceFramework.Exceptions;

namespace ServiceInterfaceFramework.Service
{
    public static class ServiceLaucher
    {
        private static Dictionary<string, ServiceHostProxy> dictServices = new Dictionary<string, ServiceHostProxy>();
        private static IEnumerable<ServiceConfigElement> configElements = null;

        internal static void EnsureConfigs()
        {
            if (configElements == null)
            {
                configElements = ServiceConfiguration.GetConfig(Define.CustomConfig);
            }
        }

        #region 服务控制

        public static void StartAll()
        {
            EnsureConfigs();

            foreach (ServiceConfigElement serviceElement in configElements)
            {
                StartService(serviceElement);
            }
        }

        public static void Start(string serviceName)
        {
            EnsureConfigs();

            var element = configElements.Where(zw => zw.Name == serviceName).FirstOrDefault();
            if (element != null)
            {
                StartService(element);
            }
        }

        public static void Stop(string serviceName)
        {
            if (configElements == null) return;

            var element = configElements.Where(zw => zw.Name == serviceName).FirstOrDefault();
            if (element != null)
            {
                StopService(element);
            }
        }

        public static void StopAll()
        {
            if (configElements == null) return;

            foreach (ServiceConfigElement serviceElement in configElements)
            {
                StopService(serviceElement);
            }
        }

        public static ServiceStatusInfo GetStatus(string serviceName)
        {
            //if (configElements == null) return ServiceStatusInfo.Empty;
            return QueryStatus(serviceName);
        }

        public static IEnumerable<ServiceStatusInfo> GetStatuses()
        {
            IList<ServiceStatusInfo> list = null;
            if (configElements == null) return list;

            list = new List<ServiceStatusInfo>();
            foreach (var element in configElements)
            {
                list.Add(QueryStatus(element.Name));
            }
            return list;
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="service"></param>
        public static void RegisterAndStartService(string serviceName, IService service)
        {
            EnsureConfigs();

            TryCatchBlock.TrycatchAndLog(() =>
            {
                var config = configElements.FirstOrDefault(zw => zw.Name == serviceName);
                if (config == null)
                {
                    throw new ServiceException(string.Format("没有服务：{0}的配置", serviceName));
                }

                if (dictServices.ContainsKey(serviceName))
                {
                    LogHelper.WriteDebug(string.Format("Service {0} 已经在运行了...", serviceName));
                    return;
                }

                ServiceHostProxy proxy = new ServiceHostProxy(config, service);
                dictServices.Add(serviceName, proxy);
                proxy.Start();
            });
        }

        /// <summary>
        /// 卸载服务
        /// </summary>
        /// <param name="serviceName"></param>
        public static void UnRegisterService(string serviceName)
        {
            EnsureConfigs();

            TryCatchBlock.TrycatchAndLog(() =>
            {
                var config = configElements.FirstOrDefault(zw => zw.Name == serviceName);
                if (config == null)
                {
                    return;
                }

                if (dictServices.ContainsKey(serviceName))
                {
                    dictServices[serviceName].Stop();
                    dictServices.Remove(serviceName);
                    LogHelper.WriteDebug(string.Format("Service {0} 已经在移除了...", serviceName));
                    return;
                }
            });
        }

        #endregion

        private static void StartService(ServiceConfigElement serviceElement)
        {
            TryCatchBlock.TrycatchAndLog(() =>
            {
                lock (dictServices)
                {
                    ServiceHostProxy proxy = null;
                    if (dictServices.ContainsKey(serviceElement.Name))
                    {
                        proxy = dictServices[serviceElement.Name];
                    }
                    else
                    {
                        proxy = new ServiceHostProxy(serviceElement);
                        dictServices[serviceElement.Name] = proxy;
                    }
                    proxy.Start();
                }
                LogHelper.WriteDebug(string.Format("Service {0} started at StartService", serviceElement.Name));
            });
        }

        private static void StopService(ServiceConfigElement serviceElement)
        {
            TryCatchBlock.TrycatchAndLog(() =>
            {
                lock (dictServices)
                {
                    if (dictServices.ContainsKey(serviceElement.Name))
                    {
                        dictServices[serviceElement.Name].Stop();
                        dictServices.Remove(serviceElement.Name);
                    }
                }
                LogHelper.WriteDebug(string.Format("Service {0} started at StopService", serviceElement.Name));
            });
        }

        private static ServiceStatusInfo QueryStatus(string serviceName)
        {
            return TryCatchBlock.TrycatchAndLog<ServiceStatusInfo>(() =>
            {
                lock (dictServices)
                {
                    if (dictServices.ContainsKey(serviceName))
                    {
                        var proxy = dictServices[serviceName];
                        return proxy.Stauts;
                    }
                }
                LogHelper.WriteDebug(string.Format("Service {0} started at QueryStatus", serviceName));
                return null;
            });
        }
    }
}
