using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using ServiceInterfaceFramework.Common;
using ServiceInterfaceFramework.Exceptions;

namespace ServiceInterfaceFramework.Service
{
    internal class ServiceHostProxy
    {
        private Thread thread;
        private IService service;
        private ServiceConfigElement configElement;

        #region 构造函数

        public ServiceHostProxy(ServiceConfigElement configElement)
        {
            Guard.IsNotNull(configElement, "configElement");
            
            if (string.IsNullOrEmpty(configElement.Type))
            {
                throw new ServiceException("配置类型为空！");
            }

            this.configElement = configElement;
            this.service = CreateWorkItem();

            this.service.Interval = configElement.Interval;
            this.service.Name = configElement.Name;
            this.service.RunOnlyOnce = configElement.RunOnlyOnce;
            this.service.DoWorkAtStart = configElement.DoWorkAtStart;
        }

        public ServiceHostProxy(IService service)
        {
            Guard.IsNotNull(service, "service");

            this.service = service;
        }

        public ServiceHostProxy(ServiceConfigElement configElement, IService service)
        {
            Guard.IsNotNull(configElement, "configElement");

            if (string.IsNullOrEmpty(configElement.Type))
            {
                throw new ServiceException("配置类型为空！");
            }

            Guard.IsNotNull(service, "service");

            this.configElement = configElement;

            this.service = service;
            this.service.Interval = configElement.Interval;
            this.service.Name = configElement.Name;
            this.service.RunOnlyOnce = configElement.RunOnlyOnce;
            this.service.DoWorkAtStart = configElement.DoWorkAtStart;
        }

        #endregion
        public ServiceStatusInfo Stauts
        {
            get
            {
                return new ServiceStatusInfo()
                {
                    ThreadName = thread.Name,
                    ThreadState = thread.ThreadState
                };
            }
        }


        private IService CreateWorkItem()
        {
            var types = configElement.Type.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            return (IService)Assembly.Load(types[1]).CreateInstance(types[0]);
        }

        public void Start()
        {
            if (thread == null || thread.ThreadState == ThreadState.Aborted)
            {
                thread = new Thread(new ThreadStart(service.Start));
                thread.Name = service.Name;
                thread.Start();
            }
        }

        public void Stop()
        {
            if (thread != null)
            {
                service.Stop();
                thread.Interrupt();
            }
        }
    }
}
