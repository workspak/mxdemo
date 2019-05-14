using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlCache
{
    class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            CacheHelper cache = new CacheHelper(enumCacheType.Redis);
            //Set(cache, "pp", new Person { Id = 2, Name = "gaici", Age = 20 });
            List<Person> list = new List<Person>();        
           
            //cache.Set("p", new Person { Id=11,Name="kkkk",Age=18});

            //cache.Set("list", list);
            //RemoveAll(cache);
            Console.WriteLine("ok");
            //Console.WriteLine(cache.Get<List<Person>>("list").Count);
            
                //Remove(cache, "pp");
                //Get(cache, "pp");

                Console.ReadKey();
        }

        static void Set(CacheHelper cache, string key, Person p)
        {
            cache.Set(key, p);
            Console.WriteLine("存上啦");
        }

        static void SetExpired(CacheHelper cache, string key, Person p, DateTime expired)
        {
            cache.Set(key, p,expired);
            Console.WriteLine("有过期时间，存上啦");
        }

        static void Get(CacheHelper cache, string key)
        {
            Person p = cache.Get<Person>(key);
            if (p == null)
            {
                Console.WriteLine("null");
            }
            else
            {
                Console.WriteLine(p.Id+"---"+p.Name);
            }
        }

        static void Remove(CacheHelper cache, string key)
        {
            cache.Remove(key);
            Console.WriteLine("删除了"+key);
        }

        static void RemoveAll(CacheHelper cache)
        {
            cache.RemoveAll();
            Console.WriteLine("全删了");
        }
    }
}
