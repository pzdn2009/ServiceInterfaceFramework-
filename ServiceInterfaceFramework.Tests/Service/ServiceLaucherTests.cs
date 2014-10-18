using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ServiceInterfaceFramework.Service;
using Moq;
using ServiceInterfaceFramework.Exceptions;

namespace ServiceInterfaceFramework.Tests.Service
{
    [TestFixture]
    public class ServiceLaucherTests
    {

        [Test]
        [ExpectedException(typeof(ServiceException))]
        public void RegisterAndStartService_Test()
        {
            var mock = new Mock<IService>();
            ServiceLaucher.RegisterAndStartService("NO_Exsit Service", mock.Object);
        }

    }
}
