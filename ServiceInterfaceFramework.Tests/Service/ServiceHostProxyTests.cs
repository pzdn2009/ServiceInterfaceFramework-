using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ServiceInterfaceFramework.Service;
using Moq;
using System.Threading;
using Should;
using ServiceInterfaceFramework.Exceptions;

namespace ServiceInterfaceFramework.Tests.Service
{
    [TestFixture]
    public class ServiceHostProxyTests
    {
        #region 构造函数

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Null_ConfigElement()
        {
            ServiceConfigElement configElement = null;
            ServiceHostProxy proxy = new ServiceHostProxy(configElement);
        }

        [Test]
        [ExpectedException(typeof(ServiceException), ExpectedMessage = "配置类型为空！")]
        public void Constructor_Null_ConfigElement_Type()
        {
            ServiceConfigElement configElement = new ServiceConfigElement();
            ServiceHostProxy proxy = new ServiceHostProxy(configElement);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ConfigElement_Type_NotExist()
        {
            ServiceConfigElement configElement = new ServiceConfigElement()
            {
                Type = "FormTrackingService.Expresss100ServiceModule,FormTrackingService"
            };
            ServiceHostProxy proxy = new ServiceHostProxy(configElement);
        }

        #endregion

        [Test]
        public void Start_Thread_Will_Call_Start()
        {
            var mock = new Mock<IService>();
            mock.Setup(zw => zw.Start()).Verifiable();

            ServiceHostProxy proxy = new ServiceHostProxy(mock.Object);
            proxy.Start();
            Thread.Sleep(1000);
            mock.VerifyAll();
        }

        [Test]
        public void Start_ThreadName_Be_TEST()
        {
            var mock = new Mock<IService>();
            mock.Setup(zw => zw.Name).Returns("TEST");

            ServiceHostProxy proxy = new ServiceHostProxy(mock.Object);
            proxy.Start();
            proxy.Stauts.ThreadName.ShouldEqual("TEST");
            Thread.Sleep(1000);
        }

        [Test]
        public void Start_ThreadStatus_Will_Running()
        {
            var mock = new Mock<IService>();
            ServiceHostProxy proxy = new ServiceHostProxy(mock.Object);
            proxy.Start();
            proxy.Stauts.ThreadState.ToString().ShouldEqual("Running");
            Thread.Sleep(1000);
        }

        [Test]
        public void Start_ThreadStatus_Will_Stopped()
        {
            var mock = new Mock<IService>();
            ServiceHostProxy proxy = new ServiceHostProxy(mock.Object);
            proxy.Start();
            Thread.Sleep(1000);
            proxy.Stauts.ThreadState.ToString().ShouldEqual("Stopped");
        }

        [Test]
        public void Stop_Will_Call_Stop()
        {
            var mock = new Mock<IService>();
            mock.Setup(zw => zw.Stop()).Verifiable();

            ServiceHostProxy proxy = new ServiceHostProxy(mock.Object);
            proxy.Start();
            proxy.Stop();
            Thread.Sleep(1000);
            mock.Verify(zw => zw.Stop());
        }

        [Test]
        public void Stop_Thread_Aborted()
        {
            var mock = new Mock<IService>();

            ServiceHostProxy proxy = new ServiceHostProxy(mock.Object);
            proxy.Start();
            proxy.Stop();
            Thread.Sleep(1000);

            proxy.Stauts.ThreadState.ToString().ShouldEqual("Aborted");
        }
    }
}
