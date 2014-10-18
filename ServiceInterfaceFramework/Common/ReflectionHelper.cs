using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceInterfaceFramework.Common
{
    public class ReflectionHelper
    {
        public static T GetProperty<T>(object entity, string propertyName)
        {
            var properties = entity.GetType().GetProperties();
            object val = null;
            foreach (var item in properties)
            {
                if (item.Name == propertyName)
                {
                    val = item.GetValue(entity, null);
                }
            }

            return (T)val;
        }

        public static void Clone<T>(T source, T target) where T : new()
        {
            var type = source.GetType();
            var properties = type.GetProperties();

            foreach (var prop in properties)
            {
                if (prop.PropertyType.IsValueType || prop.PropertyType.FullName == "System.String")
                {
                    var propertyType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    object val = prop.GetValue(source,null);
                    if (val == null)
                    {
                        val = null;
                    }
                    else
                    {
                        val = Convert.ChangeType(val, propertyType);
                    }

                    prop.SetValue(target, val, null);
                }
            }
        }


    }
}
