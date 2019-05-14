using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlCache
{
    public class CacheHelper
    {
        private ICache commonCache;       
        public CacheHelper(enumCacheType cacheType)
        {
            commonCache = GetCache(cacheType);
        }
        public  ICache GetCache(enumCacheType cacheType)
        {
            switch (cacheType)
            {
                case enumCacheType.Memcache:
                    return new MemcacheCache();                    
                case enumCacheType.Redis:
                    return new RedisCache();
                case enumCacheType.WebCache:
                    return new WebCache();                
                default:
                    return null;
            }
                   
        }

        /// <summary>
        /// 判断键值是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>判断结果</returns>
        public bool HasKey(string key)
        {
            return commonCache.HasKey(key);
        }
        /// <summary>
        /// 设置键值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>      
        public void Set(string key, object value)
        {
            commonCache.Set(key, value);
        }
        /// <summary>
        /// 设置键值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expired">过期时间</param>   
        public void Set(string key, object value, DateTime expired)
        {

            commonCache.Set(key, value, expired);
        }
        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public T Get<T>(string key)
        {
            return commonCache.Get<T>(key);
        }
        /// <summary>
        /// 根据键移除值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>移除是否成功</returns>
        public bool Remove(string key)
        {
            return commonCache.Remove(key);
        }
        /// <summary>
        /// 移除所有键值
        /// </summary>   
        public void RemoveAll()
        {
            commonCache.RemoveAll();
        }

      
    }
}
