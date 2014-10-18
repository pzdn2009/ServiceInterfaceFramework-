using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using ServiceInterfaceFramework.Common;

namespace ServiceInterfaceFramework.WebReq
{
    public class WebReqUtility
    {
        #region ASPX
        public static string GetViewState(string html)
        {
            string name = "__VIEWSTATE";
            return GetHtmlInput(html, name);
        }

        public static string GetEventArgument(string html)
        {
            string name = "__EVENTARGUMENT";
            return GetHtmlInput(html, name);
        }

        public static string GetEventValidation(string html)
        {
            string name = "__EVENTVALIDATION";
            return GetHtmlInput(html, name);
        }
        #endregion


        public static string GetHtmlInput(string html, string name)
        {
            var pattern = "<input.*?name=\"{0}\".*?value=\"(?<value>.*?)\"";
            string value = RegexHelper.GetFullOptionsMatchCollection(string.Format(pattern, name),
                html)[0].Groups["value"].Value;
            return value;
        }

        public static string Escape(string instring)
        {
            string tmp = Uri.EscapeDataString(instring);
            return tmp;
        }


        public static Dictionary<string, string> GetAllHiddenKeyValue(string html)
        {
            var coll = RegexHelper.GetFullOptionsMatchCollection("<input.*?type=\"hidden\".*?name=\"(?<name>.*?)\".*?value=\"(?<value>.*?)\"", html);
            Dictionary<string, string> hash = new Dictionary<string, string>();
            foreach (Match match in coll)
            {
                hash[match.Groups["name"].Value] = match.Groups["value"].Value;
            }
            return hash;
        }
    }
}
