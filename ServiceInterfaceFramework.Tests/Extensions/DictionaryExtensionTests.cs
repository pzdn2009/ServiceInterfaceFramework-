using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;

namespace ServiceInterfaceFramework.Tests.Extensions
{
    [TestFixture]
    public class DictionaryExtensionTests
    {
        [Test]
        public void GetOrAddGet_Get_Exist()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("key1", "value1");
            var val = dict.GetOrAddGet<string>("key1", () => { return "value1"; });
            val.Should().Be("value1");
        }

        [Test]
        public void GetOrAddGet_Get_Not_Exist()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var val = dict.GetOrAddGet<string>("key1", () => { return "value1"; });
            val.Should().Be("value1");
        }
    }
}
