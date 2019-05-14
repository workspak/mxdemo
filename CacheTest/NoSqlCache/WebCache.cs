using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NoSqlCache
{
    public class WebCache:ICache
    {
        public bool HasKey(string key)
        {
            return HttpRuntime.Cache.Get(key) != null;
        }

        public void Set(string key, object value)
        {
            if (HttpRuntime.Cache.Get(key) != null)
            {
                HttpRuntime.Cache.Remove(key);
            }
            HttpRuntime.Cache.Insert(key, value, null, DateTime.MaxValue, TimeSpan.Zero);
        }

        public void Set(string key, object value, DateTime expired)
        {
            if (HttpRuntime.Cache.Get(key) != null)
            {
                HttpRuntime.Cache.Remove(key);
            }
            HttpRuntime.Cache.Insert(key, value, null, expired, TimeSpan.Zero);
        }

        public T Get<T>(string key)
        {
            return (T)HttpRuntime.Cache.Get(key);
        }

        public bool Remove(string key)
        {
           return HttpRuntime.Cache.Remove(key)!=null;
        }

        public void RemoveAll()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                _cache.Remove(CacheEnum.Key.ToString());
            }
        }
      
    }
}
