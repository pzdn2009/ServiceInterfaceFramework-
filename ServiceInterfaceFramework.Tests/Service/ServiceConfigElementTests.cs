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
    public class ServiceConfigElementTests
    {
        #region 时间转化测试

        [Test]
        public void SecondTests()
        {
            ServiceConfigElement se = new ServiceConfigElement()
            {
                Interval = 6,
                Unit = EUnitType.second
            };

            se.ToMiniSecond().ShouldEqual(6000);
        }

        [Test]
        public void MinuteTests()
        {
            ServiceConfigElement se = new ServiceConfigElement()
            {
                Interval = 6,
                Unit = EUnitType.minute
            };

            se.ToMiniSecond().ShouldEqual(360000);
        }

        [Test]
        public void HourTests()
        {
            ServiceConfigElement se = new ServiceConfigElement()
            {
                Interval = 24,
                Unit = EUnitType.hour
            };

            se.ToMiniSecond().ShouldEqual(1000 * 3600 * 24);
        }

        #endregion

    }
}
