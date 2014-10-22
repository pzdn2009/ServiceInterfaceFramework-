using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceInterfaceFramework.Common;
namespace ServiceInterfaceFramework.Tests.Common
{
    [TestFixture]
    public class GuardTests
    {
        #region IsTrue

        [Test]
        public void IsTrue_True_Nothing()
        {
            ServiceInterfaceFramework.Common.Guard.IsTrue(true);
        }

        [Test]
        public void IsTrue_True_Nothing_CustomMessage()
        {
            ServiceInterfaceFramework.Common.Guard.IsTrue(true, "我的消息");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "条件为False")]
        public void IsTrue_False_Throw()
        {
            ServiceInterfaceFramework.Common.Guard.IsTrue(false);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "我的消息")]
        public void IsTrue_False_Throw_CustomMessage()
        {
            ServiceInterfaceFramework.Common.Guard.IsTrue(false, "我的消息");
        }

        #endregion

        #region IsFalse

        [Test]
        public void IsFalse_False_Nothing()
        {
            ServiceInterfaceFramework.Common.Guard.IsFalse(false);
        }

        [Test]
        public void IsFalse_False_Nothing_CustomMessage()
        {
            ServiceInterfaceFramework.Common.Guard.IsFalse(false, "我的消息");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "条件为True")]
        public void IsFalse_True_Throw()
        {
            ServiceInterfaceFramework.Common.Guard.IsFalse(true);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException), ExpectedMessage = "我的消息")]
        public void IsFalse_True_Throw_CustomMessage()
        {
            ServiceInterfaceFramework.Common.Guard.IsFalse(true, "我的消息");
        }

        #endregion

        #region IsNotNull

        [Test]
        public void IsNotNull_NotNull_Nothing()
        {
            ServiceInterfaceFramework.Common.Guard.IsNotNull("哈哈");
        }

        [Test]
        public void IsNotNull_NotNull_Nothing_CustomMessage()
        {
            ServiceInterfaceFramework.Common.Guard.IsNotNull("哈哈", "呵呵");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNotNull_Null_Throw()
        {
            ServiceInterfaceFramework.Common.Guard.IsNotNull(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "我的消息")]
        public void IsNotNull_Null_Throw_CustomMessage()
        {
            ServiceInterfaceFramework.Common.Guard.IsNotNull(null, "我的消息");
        }

        #endregion

        #region IsNull

        [Test]
        public void IsNull_Null_Nothing()
        {
            ServiceInterfaceFramework.Common.Guard.IsNull(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsNull_NotNull_Throw()
        {
            ServiceInterfaceFramework.Common.Guard.IsNull("哈哈");
        }

        #endregion

        #region IsOneOfSupplied

        [Test]
        public void IsOneOfSupplied_Yes_Nothing()
        {
            var list = new List<string>() { "one", "two", "three" };
            ServiceInterfaceFramework.Common.Guard.IsOneOfSupplied<string>("one",list);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void IsOneOfSupplied_No_Throw()
        {
            var list = new List<string>() { "one", "two", "three" };
            ServiceInterfaceFramework.Common.Guard.IsOneOfSupplied<string>("one1", list);
        }
         
        #endregion
    }
}
