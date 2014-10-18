using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ServiceInterfaceFramework.Common;
using System.IO;

namespace ServiceInterfaceFramework.Configuration
{
    /// <summary>
    /// 自定义配置的基类，在子类中重写属性
    /// </summary>
    public abstract class ConfigBase
    {
        protected ConfigBase()
        {

        }

        /// <summary>
        /// 根据XML文件，生成一个配置信息
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <param name="configType"></param>
        /// <returns></returns>
        public static IEnumerable<T> XmlConfig<T>(string xmlFile, string elementName, Type configType) where T : ConfigBase
        {
            if (!xmlFile.FileExist())
            {
                throw new FileNotFoundException(xmlFile);
            }

            var xmlParentElements = XElement.Load(xmlFile).Descendants(elementName);
            var list = new List<T>();
            foreach (var element in xmlParentElements)
            {
                var instance = Activator.CreateInstance(configType) as ConfigBase;
                instance.ResolveProperty(element);
                list.Add((T)instance);
            }
            return list;
        }

        protected void ResolveProperty(XElement xmlDoc)
        {
            var type = this.GetType();
            foreach (var pro in type.GetProperties())
            {
                pro.SetValue(this, xmlDoc.Element(pro.Name).Value, null);
            }
        }
    }
}
