using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace NoSqlCache
{
    public class RedisCache:ICache
    {
        private static string conn = ConfigurationManager.AppSettings["redisip"] ?? "127.0.0.1:6379";
        private static object locker = new Object();
        

        private static ConnectionMultiplexer instance;
        //静态构造方法初始化instance
        static RedisCache()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null || !instance.IsConnected)
                    {
                        instance = ConnectionMultiplexer.Connect(conn);
                    }
                }
            }     
        }

        public bool HasKey(string key)
        {
            
            return instance.GetDatabase().KeyExists(key);
        }
        public void Set(string key, object value)
        {
            instance.GetDatabase().StringSet(key, SerializerHelper.SerializeToString(value));
        }

        public void Set(string key, object value, DateTime expired)
        {
           
            instance.GetDatabase().StringSet(key, SerializerHelper.SerializeToString(value));
            instance.GetDatabase().KeyExpire(key, expired);
        }

        public T Get<T>(string key)
        {
            return SerializerHelper.DeserializeToObject<T>(instance.GetDatabase().StringGet(key));
        }

        public bool Remove(string key)
        {            
            return instance.GetDatabase().KeyDelete(key);
        }

        public void RemoveAll()
        {
            var keyList=instance.GetServer(conn).Keys();
            foreach (var key in keyList)
            {
                instance.GetDatabase().KeyDelete(key);
            }
        }   

         
    }
}
