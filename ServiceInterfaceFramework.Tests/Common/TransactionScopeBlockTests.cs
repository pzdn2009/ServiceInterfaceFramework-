using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ServiceInterfaceFramework.Common;
using System.Transactions;
using Moq;

namespace ServiceInterfaceFramework.Tests.Common
{
    [TestFixture]
    public class TransactionScopeBlockTests
    {
        [Test]
        public void New_Test_Throw()
        {
            DistributionTransactionScopeBlock.New(() =>
            {
                throw new Exception();
            });
        }

        [Test]
        public void NewWithLock_Test_Throw()
        {
            DistributionTransactionScopeBlock.NewWithLock(() =>
            {
                throw new Exception();
            });
        }
    }
}
