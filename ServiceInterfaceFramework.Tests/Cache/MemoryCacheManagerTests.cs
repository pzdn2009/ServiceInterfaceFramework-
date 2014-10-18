using NUnit.Framework;
using ServiceInterfaceFramework.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;

namespace ServiceInterfaceFramework.Tests.Cache
{
    [TestFixture]
    public class MemoryCacheManagerTests
    {
        [Test]
        public void Instance_Only()
        {
            var ins1 = MemoryCacheManager.Instance;
            var ins2 = MemoryCacheManager.Instance;
            ins1.Should().Be(ins2);
        }

        [Test]
        public void Get_None()
        {
            var item = MemoryCacheManager.Instance.Get<string>("none");
            item.Should().Be(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Set_Not_Null()
        {
            MemoryCacheManager.Instance.Set("One", null, 1);
        }

        [Test]
        public void Set_Should_Be_pzdn()
        {
            MemoryCacheManager.Instance.Set("One", "pzdn", 1);
            var ret = MemoryCacheManager.Instance.Get<string>("One");
            ret.Should().Be("pzdn");
        }

        [Test]
        public void Set_Should_Be_Not_Expiration()
        {
            MemoryCacheManager.Instance.Set("One", "pzdn", 1);
            System.Threading.Thread.Sleep(800);
            var ret = MemoryCacheManager.Instance.Get<string>("One");
            ret.Should().Be("pzdn");
        }

        [Test]
        public void Set_Should_Be_Expiration()
        {
            MemoryCacheManager.Instance.Set("One", "pzdn", 1);
            System.Threading.Thread.Sleep(1000);
            var ret = MemoryCacheManager.Instance.Get<string>("One");
            ret.Should().Be(null);
        }

        [Test]
        public void Contain_Key_None()
        {
            MemoryCacheManager.Instance.Contain("none").Should().Be(false);
        }

        [Test]
        public void Contain_Key_Return_True()
        {
            MemoryCacheManager.Instance.Set("One", "pzdn", 1);
            MemoryCacheManager.Instance.Contain("One").Should().Be(true);
        }

        [Test]
        public void Remove_None()
        {
            MemoryCacheManager.Instance.Remove("none");
        }

        [Test]
        public void Remove_Real_Key()
        {
            MemoryCacheManager.Instance.Set("One", "pzdn", 5);
            MemoryCacheManager.Instance.Remove("One");
            MemoryCacheManager.Instance.Contain("One").Should().Be(false);
        }

        [Test]
        public void RemoveByPattern_Test()
        {
            MemoryCacheManager.Instance.Set("One", "pzdn", 5);
            MemoryCacheManager.Instance.Set("sdfOned", "pzdn2", 5);
            MemoryCacheManager.Instance.Set("Three", "pzdn3", 5);
            MemoryCacheManager.Instance.RemoveByPattern("One");
            MemoryCacheManager.Instance.Contain("sdfOned").Should().Be(false);
        }

        [Test]
        public void Clear_None_Cache()
        {
            MemoryCacheManager.Instance.Clear();
        }

        [Test]
        public void Clear_Three_Cache()
        {
            MemoryCacheManager.Instance.Set("One", "pzdn", 5);
            MemoryCacheManager.Instance.Set("Two", "pzdn2", 5);
            MemoryCacheManager.Instance.Set("Three", "pzdn3", 5);
            MemoryCacheManager.Instance.Clear();

            MemoryCacheManager.Instance.Contain("One").Should().Be(false);
            MemoryCacheManager.Instance.Contain("Two").Should().Be(false);
            MemoryCacheManager.Instance.Contain("Three").Should().Be(false);
        }
    }
}
