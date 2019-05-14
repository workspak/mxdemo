using Memcached.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace NoSqlCache
{
    public class MemcacheCache:ICache
    {
        
        private static readonly MemcachedClient client;

        //静态构造函数中初始化client
        static MemcacheCache()
        {
            string ipStr = ConfigurationManager.AppSettings["memcachedip"];
            if (string.IsNullOrEmpty(ipStr))
            {
                throw new Exception("请先在web.config里配置memcachedip。");
            }
            string[] servers = ipStr.Split(',');

            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(servers);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            pool.Initialize();
            //客户端实例
            client = new Memcached.Client.MemcachedClient();
            client.EnableCompression = true;
        
        }

        public bool HasKey(string key)
        {
            return client.KeyExists(key);
        }

        
        public void Set(string key, object value)
        {           
           
           client.Set(key, SerializerHelper.SerializeToString(value));          
           
        }

        public void Set(string key, object value, DateTime expired)
        {
            
            client.Set(key, SerializerHelper.SerializeToString(value), expired);
        }

        public T Get<T>(string key)
        {          
            object obj = client.Get(key);
            if (obj == null)
            {
                return default(T);
            }            
            return SerializerHelper.DeserializeToObject<T>(obj.ToString());
        }

        public bool Remove(string key)
        {
            if (client.KeyExists(key))
            {
                return client.Delete(key);
            }
            return true;
        }

        public void RemoveAll()
        {
           client.FlushAll();
        }




       
    }
}
