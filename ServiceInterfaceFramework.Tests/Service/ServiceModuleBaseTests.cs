using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using ServiceInterfaceFramework.Service;
using ServiceInterfaceFramework.ServiceModule;

namespace ServiceInterfaceFramework.Tests.Service
{
    [TestFixture]
    public class ServiceModuleBaseTests
    {
        [Test]
        public void Start_Test()
        {
            var mock = new Mock<ServiceModuleBase>();
            mock.Object.Start();
            var instance = new DemoServiceModule();
            instance.Start();
        }
    }



}
