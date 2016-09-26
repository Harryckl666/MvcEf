using System;
using System.Collections.Generic;

using System.Text;

using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;

namespace ZbClassLibrary
{
    public class DynamicClass
    {
        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="objClass"></param>
        /// <param name="propertyName"></param>
        public static object getProperty(object objClass, string propertyName)
        {
            PropertyInfo[] infos = objClass.GetType().GetProperties();
            foreach (PropertyInfo info in infos)
            {
                if (info.Name == propertyName && info.CanRead)
                {
                    return info.GetValue(objClass, null);
                }
            }

            return null;
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="objClass"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void setProperty(ref object objClass, string propertyName, object value)
        {
            PropertyInfo[] infos = objClass.GetType().GetProperties();
            foreach (PropertyInfo info in infos)
            {
                if (info.Name == propertyName && info.CanWrite)
                {
                    info.SetValue(objClass, value, null);
                }
            }
        }

        /// <summary>
        /// 替换变量
        /// </summary>
        /// <param name="input"></param>
        /// <param name="result"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ReplaceObject(string input, string result, object model)
        {
            Type t = model.GetType();

            foreach (PropertyInfo pi in t.GetProperties())
            {
                object value = pi.GetValue(model, null);//用pi.GetValue获得值
                string name = pi.Name;//获得属性的名字,后面就可以根据名字判断来进行些自己想要的操作

                if (value.GetType() == typeof(int))
                {
                    input = input.Replace("{$" + result + "[" + name + "]}", value.ToString());
                }
                if (value.GetType() == typeof(string))
                {
                    input = input.Replace("{$" + result + "[" + name + "]}", value.ToString());
                }
                if (value.GetType() == typeof(bool))
                {
                    input = input.Replace("{$" + result + "[" + name + "]}", value.ToString());
                }
                if (value.GetType() == typeof(decimal))
                {
                    input = input.Replace("{$" + result + "[" + name + "]}", Convert.ToDecimal(value).ToString("C").TrimStart('￥'));
                }
                if (value.GetType() == typeof(DateTime))
                {
                    input = input.Replace("{$" + result + "[" + name + "]}", Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }

            return input;

        }
    }
}
