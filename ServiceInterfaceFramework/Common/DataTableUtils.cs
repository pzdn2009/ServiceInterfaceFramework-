using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ServiceInterfaceFramework.Common
{
    public static class DataTableUtils
    {
        public static DataTable CopyToDataTable<T>(IEnumerable<T> list) where T : class
        {
            Guard.IsNotNull(list);

            var ret = new DataTable();
            foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(typeof(T)))
            {
                if (dp.PropertyType.IsGenericType && dp.PropertyType
                    .GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    ret.Columns.Add(dp.Name, Nullable.GetUnderlyingType(dp.PropertyType));
                }
                else
                {
                    ret.Columns.Add(dp.Name, dp.PropertyType);
                }
            }

            foreach (T item in list)
            {
                var row = ret.NewRow();
                foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(typeof(T)))
                {
                    row[dp.Name] = dp.GetValue(item) ?? DBNull.Value;
                }
                ret.Rows.Add(row);
            }
            return ret;
        }
    }
}
