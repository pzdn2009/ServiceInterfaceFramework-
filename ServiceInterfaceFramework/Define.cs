using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ServiceInterfaceFramework
{
    public class Define
    {
        /// <summary>
        /// 自定义配置文件名称
        /// </summary>
        public static string CustomConfig
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CustomConfig.xml"); }
        }
    }
}
