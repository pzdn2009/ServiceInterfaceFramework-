using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace ServiceInterfaceFramework.Common
{
    public class LogHelper
    {
        private static readonly bool enable = true;
        //!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ServiceLoggersEnable"]);

        public static void Configure()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void WriteDebug(string message, int stackDeep = 1)
        {
            var sf = new System.Diagnostics.StackFrame(stackDeep);
            var method = sf.GetMethod();

            Console.WriteLine(method.DeclaringType.Name);
            Console.WriteLine(method.Name);

            WriteDebug(method.DeclaringType.Name, method.Name, message);
        }

        public static void WriteDebug(string className, string methodName, string message)
        {
            ILog log = LogManager.GetLogger("DEBUG_ERROR");
            if (log.IsDebugEnabled)
            {
                string strText = GetMessage("D", className, methodName, message);
                log.Debug(strText);
            }
            log = null;
        }

        public static void WriteError(Exception e, int stackDeep = 1)
        {
            var sf = new System.Diagnostics.StackFrame(stackDeep);
            var method = sf.GetMethod();

            Console.WriteLine(method.DeclaringType.Name);
            Console.WriteLine(method.Name);

            WriteError(method.DeclaringType.Name, method.Name, e);
        }
        public static void WriteError(string className, string methodName, Exception e)
        {
            ILog log = LogManager.GetLogger("DEBUG_ERROR");
            if (log.IsErrorEnabled)
            {
                string strText = GetMessage("E", className, methodName, e.Message + e.StackTrace);
                log.Error(strText);
            }
            log = null;
        }

        public static void WriteError(string message, int stackDeep = 1)
        {
            var sf = new System.Diagnostics.StackFrame(stackDeep);
            var method = sf.GetMethod();

            Console.WriteLine(method.DeclaringType.Name);
            Console.WriteLine(method.Name);

            WriteError(method.DeclaringType.Name, method.Name, message);
        }
        public static void WriteError(string className, string methodName, string message)
        {
            ILog log = LogManager.GetLogger("DEBUG_ERROR");

            if (log.IsErrorEnabled)
            {
                string strText = GetMessage("E", className, methodName, message);
                log.Error(strText);
            }
            log = null;
        }

        public static void WriteAction(string message, int stackDeep = 1)
        {
            var sf = new System.Diagnostics.StackFrame(stackDeep);
            var method = sf.GetMethod();

            Console.WriteLine(method.DeclaringType.Name);
            Console.WriteLine(method.Name);

            WriteAction(method.DeclaringType.Name, method.Name, message);
        }
        public static void WriteAction(string className, string methodName, string message)
        {
            ILog log = LogManager.GetLogger("ACTION");
            if (log.IsInfoEnabled)
            {
                string strText = GetMessage("A", className, methodName, message);
                log.Info(strText);
            }
            log = null;
        }

        public static void WritePacket(string message, int stackDeep = 1)
        {
            var sf = new System.Diagnostics.StackFrame(stackDeep);
            var method = sf.GetMethod();

            Console.WriteLine(method.DeclaringType.Name);
            Console.WriteLine(method.Name);

            WritePacket(method.DeclaringType.Name, method.Name, message);
        }
        public static void WritePacket(string className, string methodName, string message)
        {
            ILog log = LogManager.GetLogger("PACKET");
            if (log.IsInfoEnabled)
            {
                string strText = GetMessage("P", className, methodName, message);
                log.Info(strText);
            }
            log = null;
        }
        #region GetMessage
        private static string GetMessage(
            string level,
            string className,
            string methodName,
            string message
            )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n");
            sb.Append("DateTime:" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "\r\n");
            sb.Append("Level:" + level + "\r\n");
            sb.Append("ClassName:" + className + "\r\n");
            sb.Append("MethodName:" + methodName + "\r\n");
            sb.Append("Message:" + message);
            return sb.ToString();
        }
        #endregion
    }
}
