using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ServiceInterfaceFramework.Service;
using Should;
using ServiceInterfaceFramework.Exceptions;

namespace ServiceInterfaceFramework.Tests.Service
{
    [TestFixture]
    public class ServiceConfigurationTests
    {
        [Test]
        [ExpectedException(ExpectedMessage = "找不到服务配置文件！")]
        public void GetConfig_FileNotFoud_Exception()
        {
            ServiceConfiguration.GetConfig("abc");
        }

        [Test]
        public void GetConfig_NoList_Tests()
        {
            var list = ServiceConfiguration.GetConfig("./CustomConfig2.xml");
            list.Count().ShouldEqual(0);
        }

        [Test]
        public void GetConfig_HasList_Tests()
        {
            var list = ServiceConfiguration.GetConfig();
            list.Count().ShouldEqual(1);
        }

        [Test]
        [ExpectedException(typeof(ServiceException), ExpectedMessage = "找不到配置节点Name")]
        public void GetConfig_No_Name_Throw()
        {
            var list = ServiceConfiguration.GetConfig("./CustomConfig3.xml");
        }


        [Test]
        [ExpectedException(typeof(ServiceException), ExpectedMessage = "找不到服务Interval")]
        public void GetConfig_No_Interval_Throw()
        {
            var list = ServiceConfiguration.GetConfig("./CustomConfig4.xml");
        }

        [Test]
        public void  GetConfig_Read_Default()
        {
            var list = ServiceConfiguration.GetConfig("./CustomConfig5.xml");
            var first = list.First();
            first.Type.ShouldEqual(string.Empty);
            first.Unit.ShouldEqual(EUnitType.second);
            first.DoWorkAtStart.ShouldEqual(false);
            first.RunOnlyOnce.ShouldEqual(false);
        }

        [Test]
        public void GetConfig_HasList_Interval_Success_Tests()
        {
            var list = ServiceConfiguration.GetConfig("");
            list.First().Interval.ShouldEqual(10 * 3600 * 1000);
        }
    }
}
