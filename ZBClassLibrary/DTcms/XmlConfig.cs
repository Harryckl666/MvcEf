using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ZbClassLibrary.DTcms
{
    public partial class XmlConfig<T> where T : new()
    {
        private static object lockHelper = new object();

        /// <summary>
        ///  读取配置文件
        /// </summary>
        public T loadConfig(string configFilePath)
        {
            return (T)SerializationHelper.Load(typeof(T), configFilePath);
        }

        /// <summary>
        /// 写入配置文件
        /// </summary>
        public T saveConifg(T model, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(model, configFilePath);
            }
            return model;
        }

        /// <summary>
        ///  读取会员配置文件
        /// </summary>
        public T loadMemberConfig()
        {
            T model = CacheHelper.Get<T>(DTKeys.CACHE_MEMBER_CONFIG);
            if (model == null)
            {
                CacheHelper.Insert(DTKeys.CACHE_MEMBER_CONFIG, loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_MEMBER_XML_CONFING)),
                    Utils.GetXmlMapPath(DTKeys.FILE_MEMBER_XML_CONFING));
                model = CacheHelper.Get<T>(DTKeys.CACHE_MEMBER_CONFIG);
            }
            return model;
        }

        /// <summary>
        ///  保存会员配置文件
        /// </summary>
        public T saveMemberConifg(T model)
        {
            return saveConifg(model, Utils.GetXmlMapPath(DTKeys.FILE_MEMBER_XML_CONFING));
        }
        /// <summary>
        ///  读取订单配置文件
        /// </summary>
        public T loadOrderConfig()
        {
            T model = CacheHelper.Get<T>(DTKeys.CACHE_ORDER_CONFIG);
            if (model == null)
            {
                CacheHelper.Insert(DTKeys.CACHE_ORDER_CONFIG, loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_ORDER_XML_CONFING)),
                    Utils.GetXmlMapPath(DTKeys.FILE_ORDER_XML_CONFING));
                model = CacheHelper.Get<T>(DTKeys.CACHE_ORDER_CONFIG);
            }
            return model;
        }

        /// <summary>
        ///  保存订单配置文件
        /// </summary>
        public T saveOrderConifg(T model)
        {
            return saveConifg(model, Utils.GetXmlMapPath(DTKeys.FILE_ORDER_XML_CONFING));
        }
    }
}
