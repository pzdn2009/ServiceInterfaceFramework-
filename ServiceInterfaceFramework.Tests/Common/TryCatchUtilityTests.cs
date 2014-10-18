using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ServiceInterfaceFramework.Common;

namespace ServiceInterfaceFramework.Tests.Common
{
    [TestFixture]
    public class TryCatchUtilityTests
    {
        [Test]
        public static void TrycatchAndLog_View_LogError()
        {
            TryCatchBlock.TrycatchAndLog(() => { throw new Exception(""); });
        }

        [Test]
        public static void TrycatchAndLog_T_ViewLogError()
        {
            TryCatchBlock.TrycatchAndLog<string>(() => { throw new Exception(""); });
        }
    }
}
