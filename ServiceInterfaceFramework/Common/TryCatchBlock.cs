using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Configuration;

namespace ServiceInterfaceFramework.Common
{
    public class TryCatchBlock
    {
        public static void TrycatchAndLog(Action body)
        {
            try
            {
                body();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex, 2);
            }
        }

        public static T TrycatchAndLog<T>(Func<T> body)
        {
            try
            {
                return body();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex, 2);
                return default(T);
            }
        }
    }
}
