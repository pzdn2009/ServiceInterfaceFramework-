using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Linq.Expressions;
using System.Threading;

namespace TicketProccess.Console
{
    public class WorkThread
    {
        #region 跨线程读写属性值
        delegate object WinUIGetter(object target, string propertyName);
        delegate void WinUISetter(object target, string propertyName, object value);
        public delegate object WinUICaller(object target, string methodName, object[] args);

        public static object GetValue(object target, string propertyName)
        {
            Control control = target as Control;
            if (null != control && control.InvokeRequired)  //使用代理调用
            {
                WinUIGetter getter = new WinUIGetter(GetPropertyValue);
                return control.Invoke(getter, new object[] { target, propertyName });
            }
            return GetPropertyValue(target, propertyName);
        }

        public static object GetValue<T>(T control, Expression<Func<T, object>> propertyName)
        {
            var member = propertyName.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentNullException("propertyName");
            }
            return GetValue(control, member.Member.Name);
        }

        public static void SetValue(object target, string propertyName, object value)
        {
            Control control = target as Control;
            if (control != null && control.InvokeRequired)
            {
                WinUISetter setter = new WinUISetter(SetPropertyValue);
                control.Invoke(setter, new object[] { target, propertyName, value });
            }
            SetPropertyValue(target, propertyName, value);
        }
        public static void SetValue<T>(Control control, Expression<Func<T, object>> propertyName, object value)
        {
            var member = propertyName.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentNullException("propertyName");
            }
            SetValue(control, member.Member.Name, value);
        }

        public static object CallMethod(object target, string methodName, object[] args)
        {
            Control control = target as Control;
            if (null != control && control.InvokeRequired)
            {
                WinUICaller caller = new WinUICaller(CallMethodInternal);
                return control.Invoke(caller, new object[] { target, methodName, args });
            }
            return CallMethodInternal(target, methodName, args);
        }
        public static object CallMethod<T>(Control control,Expression<Action<T>> expression)
        {
            return null;
        }

        #endregion

        #region PropertyInfo
        private static void SetPropertyValue(object target, string propertyName, object value)
        {
            GetPropertyInfo(target, propertyName).SetValue(target, value, null);
        }
        private static object GetPropertyValue(object target, string propertyName)
        {
            return GetPropertyInfo(target, propertyName).GetValue(target, null);
        }

        private static PropertyInfo GetPropertyInfo(object target, string name)
        {
            Type type = target.GetType();
            BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
            PropertyInfo propertyInfo = type.GetProperty(name, bindingAttr);
            if (null == propertyInfo)
            {
                throw new Exception("Can not find the property '" + name + "'.");
            }
            return propertyInfo;
        }
        #endregion

        #region MethodInfo
        private static object CallMethodInternal(object target, string methodName, object[] args)
        {
            MethodInfo methodInfo = GetMethodInfo(target, methodName);
            return methodInfo.Invoke(target, args);
        }

        private static MethodInfo GetMethodInfo(object target, string name)
        {
            Type type = target.GetType();
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
            MethodInfo methodInfo = type.GetMethod(name, bindingFlags);
            if (null == methodInfo)
            {
                throw new Exception("Can not find the method '" + name + "'.");
            }
            return methodInfo;
        }
        #endregion

    }
}
