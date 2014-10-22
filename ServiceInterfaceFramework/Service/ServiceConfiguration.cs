using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Linq.Expressions;
using ServiceInterfaceFramework.Exceptions;

namespace ServiceInterfaceFramework.Service
{
    public class ServiceConfiguration
    {
        /// <summary>
        /// 从指定的配置文件读取服务配置
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>列表集合</returns>
        public static IEnumerable<ServiceConfigElement> GetConfig(string fileName = "")
        {
            var name = String.IsNullOrEmpty(fileName) ? Define.CustomConfig : fileName;
            if (!name.FileExist())
            {
                throw new FileNotFoundException("找不到服务配置文件！");
            }

            XDocument doc = XDocument.Load(name);
            var query = doc.Descendants("Service").Select(zw => ReadConfigElement(zw)).ToList();

            return query;
        }

        private static ServiceConfigElement ReadConfigElement(XElement xElement)
        {
            var configElement = new ServiceConfigElement();

            #region 必填
            var tmp = xElement.Element("Name");
            if (tmp == null)
            {
                throw new ServiceException("找不到配置节点Name");
            }
            configElement.Name = tmp.Value;

            tmp = xElement.Element("Interval");
            if (tmp == null)
            {
                throw new ServiceException("找不到服务Interval");
            }
            configElement.Interval = int.Parse(tmp.Value);

            #endregion

            tmp = xElement.Element("Unit");
            configElement.Unit = tmp == null ? EUnitType.second : (EUnitType)Enum.Parse(typeof(EUnitType), tmp.Value);
            configElement.Interval = configElement.ToMiniSecond();

            tmp = xElement.Element("DoWorkAtStart");
            configElement.DoWorkAtStart = tmp == null ? false : tmp.Value != "0";

            tmp = xElement.Element("RunOnlyOnce");
            configElement.RunOnlyOnce = tmp == null ? false : tmp.Value != "0";

            tmp = xElement.Element("Type");
            configElement.Type = tmp == null ? string.Empty : tmp.Value;

            return configElement;
        }
    }
}
