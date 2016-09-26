using System;
using System.Collections.Generic;
using System.Text;

namespace ZbClassLibrary
{
    public class Cache
    {
        /// <summary>
        /// 设置缓存对象
        /// </summary>
        /// <param name="_cacheName">键值</param>
        /// <param name="obj">缓存对象</param>
        /// <param name="hours">缓存时间，以小时计算</param>
        public static void setCacheObject(string _cacheName, object obj,double hours)
        {
            if (System.Web.HttpRuntime.Cache[_cacheName] != null)
            {
                System.Web.HttpRuntime.Cache.Remove(_cacheName);
            }
            System.Web.HttpRuntime.Cache.Add(_cacheName, obj, null, DateTime.Now.AddHours(hours), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Default, null);
        }

        /// <summary>
        /// 设置缓存对象
        /// </summary>
        /// <param name="_cacheName">键值</param>
        /// <param name="obj">缓存对象</param>
        /// <param name="hours">缓存时间，以小时计算</param>
        public static void setCacheObject(string _cacheName, object obj, int min)
        {
            if (System.Web.HttpRuntime.Cache[_cacheName] != null)
            {
                System.Web.HttpRuntime.Cache.Remove(_cacheName);
            }
            System.Web.HttpRuntime.Cache.Add(_cacheName, obj, null, DateTime.Now.AddMinutes(min), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Default, null);
        }

        /// <summary>
        /// 返回缓存对象
        /// </summary>
        /// <param name="_cacheName"></param>
        /// <param name="objs"></param>
        public static object getCacheObject(string _cacheName)
        {
            return System.Web.HttpRuntime.Cache[_cacheName];
        }

        /// <summary>
        /// 统计缓存中的项数
        /// </summary>
        /// <returns></returns>
        public static int Count()
        {
            return System.Web.HttpRuntime.Cache.Count;
        }

        /// <summary>
        /// 移除缓存对象
        /// </summary>
        /// <param name="_cacheName"></param>
        public static void removeCacheObject(string _cacheName)
        {
            System.Web.HttpRuntime.Cache.Remove(_cacheName);
        }
    }
}
