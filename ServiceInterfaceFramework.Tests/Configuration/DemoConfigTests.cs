using NUnit.Framework;
using ServiceInterfaceFramework.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;

namespace ServiceInterfaceFramework.Tests.Configuration
{
    [TestFixture]
    public class DemoConfigTests
    {
        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void XmlConfig_File_NotExist_Throw()
        {
            var xml = AppDomain.CurrentDomain.BaseDirectory + "\\DemoConfigTestAnyway.xml";
            
            ConfigBase.XmlConfig<DemoConfig>(xml, "Root");
        }

        [Test]
        public void XmlConfig_File_Element_NotExist_Count_Zero()
        {
            var xml = AppDomain.CurrentDomain.BaseDirectory + "\\DemoConfigTest.xml";
            var list = ConfigBase.XmlConfig<DemoConfig>(xml, "Root2");
            list.Count().Should().Be(0);
        }

        [Test]
        public void XmlConfig_Element_Exist_Count_One()
        {
            var xml = AppDomain.CurrentDomain.BaseDirectory + "\\DemoConfigTest.xml";
            var list = ConfigBase.XmlConfig<DemoConfig>(xml, "MyNameTest");
            list.Count().Should().Be(1);
            list.First().Name.Should().Be("daniu a!");
        }

        [Test]
        public void XmlConfig_Element_Exist_Count_Many()
        {
            var xml = AppDomain.CurrentDomain.BaseDirectory + "\\DemoConfigTest2.xml";
            var list = ConfigBase.XmlConfig<DemoConfig2>(xml, "MyNameTest");
            list.Count().Should().Be(3);
            list.Last().Name.Should().Be("daniu a3!");
            list.Last().Age.Should().Be(27);
        }
    }
}
