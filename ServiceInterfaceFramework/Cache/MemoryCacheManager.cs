using ServiceInterfaceFramework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;

namespace ServiceInterfaceFramework.Cache
{
    public class MemoryCacheManager
    {
        private MemoryCacheManager()
        {

        }

        private static object lockObject = new object();
        private static MemoryCacheManager _instance;
        public static MemoryCacheManager Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (_instance != null)
                    {
                        return _instance;
                    }
                    return _instance = new MemoryCacheManager();
                }
            }
        }

        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        public T Get<T>(string key)
        {
            return (T)Cache.Get(key);
        }


        public void Set(string key, object data, int cacheTime)
        {
            Guard.IsNotNull(data);

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromSeconds(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }

        public bool Contain(string key)
        {
            return (Cache.Contains(key));
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public void RemoveByPattern(string keyPattern)
        {
            var regex = new Regex(keyPattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();

            foreach (var item in Cache)
                if (regex.IsMatch(item.Key))
                    keysToRemove.Add(item.Key);

            foreach (string key in keysToRemove)
            {
                Remove(key);
            }
        }

        public void Clear()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }
    }
}
