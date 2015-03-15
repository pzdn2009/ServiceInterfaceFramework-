using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Reflection;

namespace EbayAPILibrary
{
    public class APIConfigBase
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// 平台类型
        /// </summary>
        public string PlatformType { get; set; }

        public string ShopID { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 根据XML文件，生成一个配置信息
        /// </summary>
        /// <param name="xmlFileName">XML文件路径</param>
        /// <param name="parentElementName">父元素名称</param>
        /// <returns>配置对象列表</returns>
        public static IEnumerable<T> XmlConfig<T>(string xmlFileName, string parentElementName) where T : APIConfigBase
        {
            if (!File.Exists(xmlFileName))
            {
                throw new FileNotFoundException(xmlFileName);
            }

            var xmlParentElements = XElement.Load(xmlFileName).Descendants(parentElementName);
            var list = new List<T>();
            foreach (var element in xmlParentElements)
            {
                var instance = Activator.CreateInstance(typeof(T)) as APIConfigBase;
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
                SetValue(this, pro, xmlDoc.Element(pro.Name).Value);
            }
        }

        private static void SetValue(object entity, PropertyInfo property, object value)
        {
            var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            var safeValue = (value == null || value.ToString().Equals(string.Empty)) ? null : Convert.ChangeType(value, t);
            property.SetValue(entity, safeValue, null);
        }
    }
}
