using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ServiceInterfaceFramework.Common;
using ServiceInterfaceFramework.Exceptions;
using Should;

namespace ServiceInterfaceFramework.Tests.Common
{
    [TestFixture]
    public class ConfigReaderTests
    {
        [Test]
        [ExpectedException(typeof(ServiceInterfaceFrameworkException))]
        public void GetConnectionString_Throw()
        {
            ConfigReader.GetConnectionString("notright");
        }

        [Test]
        [ExpectedException(typeof(ServiceInterfaceFrameworkException))]
        public void AppSetting_Throw()
        {
            ConfigReader.GetAppSetting("notright");
        }

        [Test]
        public void GetConnectionString_Right()
        {
            ConfigReader.GetConnectionString("right").ShouldEqual("rightconnectionString");
        }

        [Test]
        public void AppSetting_Right()
        {
            ConfigReader.GetAppSetting("right").ShouldEqual("right2");
        }
    }
}
