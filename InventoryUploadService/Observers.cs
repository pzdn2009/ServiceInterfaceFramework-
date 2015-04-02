using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryUploadService
{
    public class Observers
    {
        public static object lockObject = new object();
        private static Dictionary<string, IMessageCallback> dict;
        static Observers()
        {
            dict = new Dictionary<string, IMessageCallback>();
        }

        public static void Add(string key, IMessageCallback value)
        {
            lock (lockObject)
            {
                dict[key] = value;
            }
        }

        public static IMessageCallback Get(string key)
        {
            lock (lockObject)
            {
                if (dict.ContainsKey(key))
                    return dict[key];
                return null;
            }
        }
        
        public static void Remove(string key)
        {
            lock (lockObject)
            {
                dict.Remove(key);
            }
        }

        public static int Count()
        {
            return dict.Count;
        }
    }
}
