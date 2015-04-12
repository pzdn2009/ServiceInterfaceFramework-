using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace ServiceInterfaceFramework.Common
{
    public class LogHelper
    {

        public static void Debug(string message, int stackDeep = 1)
        {
            var sf = new System.Diagnostics.StackFrame(stackDeep);
            var method = sf.GetMethod();

            Debug(method.DeclaringType.Name, method.Name, message);
        }

        public static void Debug(string className, string methodName, string message)
        {
            ILog log = LogManager.GetLogger(typeof(LogHelper));
            if (log.IsDebugEnabled)
            {
                string strText = GetMessage("DEBUG", className, methodName, message);
                log.Debug(strText);
            }
            log = null;
        }

        public static void Error(Exception e, int stackDeep = 1)
        {
            var sf = new System.Diagnostics.StackFrame(stackDeep);
            var method = sf.GetMethod();

            Error(method.DeclaringType.Name, method.Name, e);
        }
        public static void Error(string className, string methodName, Exception e)
        {
            ILog log = LogManager.GetLogger(typeof(LogHelper));
            if (log.IsErrorEnabled)
            {
                string strText = GetMessage("ERROR", className, methodName, e.Message + e.StackTrace);
                log.Error(strText);
            }
            log = null;
        }

        public static void Error(string message, int stackDeep = 1)
        {
            var sf = new System.Diagnostics.StackFrame(stackDeep);
            var method = sf.GetMethod();

            Error(method.DeclaringType.Name, method.Name, message);
        }
        public static void Error(string className, string methodName, string message)
        {
            ILog log = LogManager.GetLogger(typeof(LogHelper));

            if (log.IsErrorEnabled)
            {
                string strText = GetMessage("E", className, methodName, message);
                log.Error(strText);
            }
            log = null;
        }

        public static void Info(string message, int stackDeep = 1)
        {
            var sf = new System.Diagnostics.StackFrame(stackDeep);
            var method = sf.GetMethod();

            Info(method.DeclaringType.Name, method.Name, message);
        }
        public static void Info(string className, string methodName, string message)
        {
            ILog log = LogManager.GetLogger(typeof(LogHelper));
            if (log.IsInfoEnabled)
            {
                string strText = GetMessage("INFO", className, methodName, message);
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
            sb.Append("ClassName:" + className + "\r\n");
            sb.Append("MethodName:" + methodName + "\r\n");
            sb.Append("Message:" + message + "\r\n");
            return sb.ToString();
        }
        #endregion

        private static readonly bool enable = true;
        //!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ServiceLoggersEnable"]);

        public static void Configure()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
