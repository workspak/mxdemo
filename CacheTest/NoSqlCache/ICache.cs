using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSqlCache
{
    public interface ICache
    {
        /// <summary>
        /// 判断键值是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>判断结果</returns>
        bool HasKey(string key);
        /// <summary>
        /// 设置键值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>      
        void Set(string key,object value);
        /// <summary>
        /// 设置键值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expired">过期时间</param>       
        void Set(string key,object value,DateTime expired);
        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        T Get<T>(string key);
       
        /// <summary>
        /// 根据键移除值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>移除是否成功</returns>
        bool Remove(string key);
        /// <summary>
        /// 移除所有键值
        /// </summary>        
        void RemoveAll();
    }
}
