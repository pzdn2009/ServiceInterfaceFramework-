using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using ServiceInterfaceFramework.Exceptions;
using ServiceInterfaceFramework.Resource;

namespace ServiceInterfaceFramework.Common
{
    public class ConfigReader
    {
        public static string GetConnectionString(string name)
        {
            if (ConfigurationManager.ConnectionStrings[name] == null)
            {
                throw new ServiceInterfaceFrameworkException(String.Format(Res.NoneConn, name));
            }
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static string GetAppSetting(string name)
        {
            if (ConfigurationManager.AppSettings[name] == null)
            {
                throw new ServiceInterfaceFrameworkException(String.Format(Res.NoneAppSetting, name));
            }
            return ConfigurationManager.AppSettings[name];
        }
    }
}
