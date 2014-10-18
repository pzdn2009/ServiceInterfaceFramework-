using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Should;

namespace ServiceInterfaceFramework.Tests
{
    [TestFixture]
    public class DefineTests
    {
        [Test]
        public void CustomConfig_Value()
        {
            var path = @"E:\项目集合\ServiceInterfaceFramework\ServiceInterfaceFramework.Tests\bin\Debug";
            var val = path + @"\" + "CustomConfig.xml";
            Define.CustomConfig.ShouldEqual(val);
        }
    }
}
