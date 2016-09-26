using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;

namespace ZbClassLibrary
{
    public static class ConvertUtility
    {
        #region 基本值类型转换
        /// <summary>
        /// 字符串转换成byte类型（转换失败返回0）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <returns>字符串转换成byte类型</returns>
        public static byte ToByte(this string input)
        {
            return input.ToByte(0);
        }
        /// <summary>
        /// 字符串转换成byte类型（转换失败返回默认值）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="defaultValue">转换失败时默认值</param>
        /// <returns>字符串转换成byte类型</returns>
        public static byte ToByte(this string input, byte defaultValue)
        {
            byte iReturn;
            if (byte.TryParse(input, out iReturn))
                return iReturn;
            return defaultValue;
        }

        /// <summary>
        /// 字符串转换成short类型（转换失败返回0）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <returns>字符串转换成short类型</returns>
        public static Int16 ToInt16(this string input)
        {
            return input.ToInt16(0);
        }
        /// <summary>
        /// 字符串转换成short类型（转换失败返回默认值）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="defaultValue">转换失败时默认值</param>
        /// <returns>字符串转换成short类型</returns>
        public static Int16 ToInt16(this string input, Int16 defaultValue)
        {
            Int16 iReturn;
            if (Int16.TryParse(input, out iReturn))
                return iReturn;
            return defaultValue;
        }

        /// <summary>
        /// 字符串转换成int类型（转换失败返回0）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <returns>字符串转换成int类型</returns>
        public static Int32 ToInt32(this string input)
        {
            return input.ToInt32(0);
        }
        /// <summary>
        /// 字符串转换成int类型（转换失败返回默认值）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="defaultValue">转换失败时默认值</param>
        /// <returns>字符串转换成int类型</returns>
        public static Int32 ToInt32(this string input, Int32 defaultValue)
        {
            Int32 iReturn;
            if (Int32.TryParse(input, out iReturn))
                return iReturn;
            return defaultValue;
        }

        /// <summary>
        /// 字符串转换成Int64类型（转换失败返回0）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <returns>字符串转换成Int64类型</returns>
        public static Int64 ToInt64(this string input)
        {
            return input.ToInt64(0);
        }
        /// <summary>
        /// 字符串转换成Int64类型（转换失败返回默认值）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="defaultValue">转换失败时默认值</param>
        /// <returns>字符串转换成Int64类型</returns>
        public static Int64 ToInt64(this string input, Int64 defaultValue)
        {
            Int64 iReturn;
            if (Int64.TryParse(input, out iReturn))
                return iReturn;
            return defaultValue;
        }

        /// <summary>
        /// 字符串转换成Decimal类型（转换失败返回0m）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="defaultValue">转换失败时默认值</param>
        /// <returns>字符串转换成Decimal类型</returns>
        public static Decimal ToDecimal(this string input)
        {
            return input.ToDecimal(0m);
        }
        /// <summary>
        /// 字符串转换成Decimal类型（转换失败返回默认值）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="defaultValue">转换失败时默认值</param>
        /// <returns>字符串转换成Decimal类型</returns>
        public static Decimal ToDecimal(this string input, Decimal defaultValue)
        {
            Decimal iReturn;
            if (Decimal.TryParse(input, out iReturn))
                return iReturn;
            return defaultValue;
        }

        /// <summary>
        /// 字符串转换成Single类型（转换失败返回0f）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <returns>字符串转换成Single类型</returns>
        public static Single ToSingle(this string input)
        {
            return input.ToSingle(0f);
        }
        /// <summary>
        /// 字符串转换成Single类型（转换失败返回默认值）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="defaultValue">转换失败时默认值</param>
        /// <returns>字符串转换成Single类型</returns>
        public static Single ToSingle(this string input, Single defaultValue)
        {
            Single iReturn;
            if (Single.TryParse(input, out iReturn))
                return iReturn;
            return defaultValue;
        }
        /// <summary>
        /// 字符串转换成Double类型（转换失败返回0d）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <returns>字符串转换成Double类型</returns>
        public static Double ToDouble(this string input)
        {
            return input.ToDouble(0d);
        }
        /// <summary>
        /// 字符串转换成Double类型（转换失败返回默认值）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="defaultValue">转换失败时默认值</param>
        /// <returns>字符串转换成Double类型</returns>
        public static Double ToDouble(this string input, Double defaultValue)
        {
            Double iReturn;
            if (Double.TryParse(input, out iReturn))
                return iReturn;
            return defaultValue;
        }
        /// <summary>
        /// 字符串转换成Boolean类型（转换失败返回false）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <returns>字符串转换成Boolean类型</returns>
        public static Boolean ToBoolean(this string input)
        {
            return input.ToBoolean(false);
        }
        /// <summary>
        /// 字符串转换成Boolean类型（转换失败返回默认值）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="defaultValue">转换失败时默认值</param>
        /// <returns>字符串转换成Boolean类型</returns>
        public static Boolean ToBoolean(this string input, Boolean defaultValue)
        {
            Boolean iReturn;
            if (Boolean.TryParse(input, out iReturn))
                return iReturn;
            return false;
        }

        /// <summary>
        /// 字符串转换成DateTime类型（转换失败返回Now）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="defaultValue">转换失败时默认值</param>
        /// <returns>字符串转换成DateTime类型</returns>
        public static DateTime ToDateTime(this string input)
        {
            return input.ToDateTime(DateTime.Now);
        }
        /// <summary>
        /// 字符串转换成DateTime类型（转换失败返回默认值）
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <param name="defaultValue">转换失败时默认值</param>
        /// <returns>字符串转换成DateTime类型</returns>
        public static DateTime ToDateTime(this string input, DateTime defaultValue)
        {
            DateTime iReturn;
            if (DateTime.TryParse(input, out iReturn))
                return iReturn;
            return defaultValue;
        }

        /// <summary>
        /// 把可空时间类型转换为字符
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime? dt)
        {
            if (dt == null)
            {
                return string.Empty;
            }
            else
            {
                return dt.Value.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 把字符串数组转化为整形数组
        /// </summary>
        /// <param name="arrstr"></param>
        /// <returns></returns>
        public static int[] StrArrayConvertIntArray(string[] arrstr)
        {
            if (arrstr == null || arrstr.Length == 0)
            {
                return new int[0];
            }
            int[] ia = new int[arrstr.Length];
            for (int i = 0; i < arrstr.Length; i++)
            {
                ia[i] = int.Parse(arrstr[i]);
            }
            return ia;
        }
        #endregion
        /// <summary>
        /// 转化object值类型为对应值类型
        /// </summary>
        /// <returns></returns>
        public static object ConvertToType(object value, Type t)
        {
            string val = value.ToString().Trim();
            if (t != typeof(string))
            {
                if (t == typeof(byte) || t == typeof(byte?))
                {
                    return val.ToByte();
                }
                if (t == typeof(Int16) || t == typeof(Int16?))
                {
                    return val.ToInt16();
                }
                if (t == typeof(Int32) || t == typeof(Int32?))
                {
                    return val.ToInt32();
                }
                if (t == typeof(Int64) || t == typeof(Int64?))
                {
                    return val.ToInt64();
                }
                if (t == typeof(Decimal) || t == typeof(Decimal?))
                {
                    return val.ToDecimal();
                }
                if (t == typeof(Single) || t == typeof(Single?))
                {
                    return val.ToSingle();
                }
                if (t == typeof(Double) || t == typeof(Double?))
                {
                    return val.ToDouble();
                }
                if (t == typeof(Boolean) || t == typeof(Boolean?))
                {
                    return val.ToBoolean();
                }
                if (t == typeof(DateTime) || t == typeof(DateTime?))
                {
                    return val.ToDateTime();
                }
            }
            return val;
        }
        /// <summary>
        /// 获取属性key value 对
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IDictionary<String, Object> GetPropertiesValueDict<T>(T t)
        {
            IDictionary<String, Object> dict = new Dictionary<String, Object>();
            if (t == null)
            {
                return dict;
            }
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0)
            {
                return dict;
            }
            foreach (PropertyInfo item in properties)
            {
                dict.Add(item.Name, item.GetValue(t, null));
            }
            return dict;
        }
        /// <summary>
        /// 获取表单数据转换为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="form"></param>
        /// <returns></returns>
        public static T ToModel<T>(HttpContextBase context, params string[] notcontainerfields) where T : new()
        {
            T model = new T();
            Type type = typeof(T);
            PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo p in ps)
            {
                object value = context.Request[p.Name];
                if (value != null && !p.Name.EqualsIgnoreCase(notcontainerfields))
                {
                    //type.GetProperty(p.Name).SetValue(model, Convert.ChangeType(form[p.Name], p.PropertyType), null);
                    //if (p.PropertyType == typeof(string))
                    //    p.SetValue(model, System.Web.HttpUtility.HtmlEncode(form[p.Name].ToString()), null);
                    //else
                    p.SetValue(model, ConvertToType(value, p.PropertyType), null);
                }
                else
                {
                    p.SetValue(model, null, null);
                }
            }
            return model;
        }
        /// <summary>
        /// 获取表单数据转换为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="form"></param>
        /// <returns></returns>
        public static T ToModel<T>(this System.Collections.Specialized.NameValueCollection form, params string[] notcontainerfields) where T : new()
        {
            T model = new T();
            Type type = typeof(T);
            PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo p in ps)
            {

                if (form[p.Name] != null && !p.Name.EqualsIgnoreCase(notcontainerfields))
                {
                    //type.GetProperty(p.Name).SetValue(model, Convert.ChangeType(form[p.Name], p.PropertyType), null);
                    //if (p.PropertyType == typeof(string))
                    //    p.SetValue(model, System.Web.HttpUtility.HtmlEncode(form[p.Name].ToString()), null);
                    //else
                    if (string.IsNullOrEmpty(form[p.Name]) && (p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(Nullable<DateTime>)))
                    {
                        p.SetValue(model, null, null);
                    }
                    else
                    {
                        p.SetValue(model, ConvertToType(form[p.Name], p.PropertyType), null);
                    }
                }
                else
                {
                    p.SetValue(model, null, null);
                }
            }
            return model;
        }
        /// <summary>
        /// AES表单解密
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="form"></param>
        /// <param name="notcontainerfields"></param>
        /// <returns></returns>
        public static T ToAESModel<T>(this System.Collections.Specialized.NameValueCollection form, params string[] notcontainerfields) where T : new()
        {
            T model = new T();
            Type type = typeof(T);
            System.Collections.Specialized.NameValueCollection myCol = new System.Collections.Specialized.NameValueCollection();
            PropertyInfo[] ps = type.GetProperties();
            foreach (var item in form.Keys)
            {
                if (item.ToString().ToLower() == "id" || item.ToString().ToLower() == "oldpwd")
                {
                    myCol.Add(item.ToString(), form[item.ToString()]);
                }
                else
                {
                    myCol.Add(AES.AES.AESDecrypt(item.ToString()), form[item.ToString()]);
                }
            }
            foreach (PropertyInfo p in ps)
            {
                if (myCol[p.Name] != null && !p.Name.EqualsIgnoreCase(notcontainerfields))
                {
                    //type.GetProperty(p.Name).SetValue(model, Convert.ChangeType(form[p.Name], p.PropertyType), null);
                    //if (p.PropertyType == typeof(string))
                    //    p.SetValue(model, System.Web.HttpUtility.HtmlEncode(form[p.Name].ToString()), null);
                    //else
                    p.SetValue(model, ConvertToType(myCol[p.Name], p.PropertyType), null);

                }
            }
            return model;
        }
        /// <summary>
        /// 获取表单数据更新实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="form"></param>
        /// <returns></returns>
        public static T UpdateModel<T>(this System.Collections.Specialized.NameValueCollection form, T entity, params string[] notcontainerfields) where T : new()
        {
            Type type = typeof(T);
            PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo p in ps)
            {
                if (form[p.Name] != null && !p.Name.EqualsIgnoreCase(notcontainerfields))
                {
                    //type.GetProperty(p.Name).SetValue(entity, Convert.ChangeType(form[p.Name], p.PropertyType), null);
                    //if (p.PropertyType == typeof(string))
                    //    p.SetValue(entity, System.Web.HttpUtility.HtmlEncode(form[p.Name].ToString()), null);
                    //else
                    p.SetValue(entity, ConvertToType(form[p.Name], p.PropertyType), null);
                }
            }
            return entity;
        }
        /// <summary>
        /// 获取表单数据转换为组合查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="form"></param>
        /// <returns></returns>
        public static string ToWhere<T>(this System.Collections.Specialized.NameValueCollection form, string asStr = "", string ingro = "", params string[] notcontainerfields) where T : new()
        {
            StringBuilder sb = new StringBuilder();
            T model = new T();
            Type type = typeof(T);
            PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo p in ps)
            {
                Type m = p.PropertyType;
                if (ingro.ToUpper() != p.Name.ToUpper())
                {
                    if (!form[p.Name].IsEmpty() && !p.Name.EqualsIgnoreCase(notcontainerfields))
                    {
                        if (string.IsNullOrEmpty(asStr))
                        {
                            if (m == typeof(Int32) || m == typeof(Int32?) || m == typeof(Int64) || m == typeof(Decimal) || m == typeof(Single) || m == typeof(Decimal?) || m == typeof(Single?))
                            {
                                sb.Append(" AND " + p.Name + "=" + form[p.Name]);
                            }
                            else if (m == typeof(byte) || m == typeof(Boolean) || m == typeof(bool?))
                            {
                                sb.Append(" AND " + p.Name + "='" + form[p.Name] + "'");
                            }
                            else if (m == typeof(String) || m == typeof(string))
                            {
                                sb.Append(" AND " + p.Name + " LIKE '%" + form[p.Name] + "%'");
                            }
                            else
                            {
                                sb.Append(" AND " + p.Name + "='" + form[p.Name] + "'");
                            }
                        }
                        else
                        {
                            if (m == typeof(Int32) || m == typeof(Int32?) || m == typeof(Int64) || m == typeof(Decimal) || m == typeof(Single) || m == typeof(Decimal?) || m == typeof(Single?))
                            {
                                sb.Append(" AND " + asStr + "." + p.Name + "=" + form[p.Name]);
                            }
                            else if (m == typeof(byte) || m == typeof(Boolean) || m == typeof(bool?))
                            {
                                sb.Append(" AND " + asStr + "." + p.Name + "='" + form[p.Name] + "'");
                            }
                            else if (m == typeof(String) || m == typeof(string))
                            {
                                sb.Append(" AND " + asStr + "." + p.Name + " LIKE '%" + form[p.Name] + "%'");
                            }
                            else
                            {
                                sb.Append(" AND " + asStr + "." + p.Name + "='" + form[p.Name] + "'");
                            }
                        }
                    }
                    if (m == typeof(DateTime) || m == typeof(DateTime?))
                    {
                        if (string.IsNullOrEmpty(asStr))
                        {
                            if (!form[p.Name + "Start"].IsEmpty() && !form[p.Name + "End"].IsEmpty())
                            {
                                sb.Append(" AND " + p.Name + " BETWEEN  '" + ConvertToType(form[p.Name + "Start"], p.PropertyType) + "' AND   '" + Convert.ToDateTime(form[p.Name + "End"]).ToString("yyyy/MM/dd 23:59:59") + "'");
                            }
                            else if (!form[p.Name + "Start"].IsEmpty())
                            {
                                sb.Append(" AND " + p.Name + ">'" + ConvertToType(form[p.Name + "Start"], p.PropertyType) + "'");
                            }
                            else if (!form[p.Name + "End"].IsEmpty())
                            {
                                sb.Append(" AND " + p.Name + "<'" + Convert.ToDateTime(form[p.Name + "End"]).ToString("yyyy/MM/dd 23:59:59") + "'");
                            }
                        }
                        else
                        {
                            if (!form[p.Name + "Start"].IsEmpty() && !form[p.Name + "End"].IsEmpty())
                            {
                                sb.Append(" AND " + asStr + "." + p.Name + " BETWEEN  '" + ConvertToType(form[p.Name + "Start"], p.PropertyType) + "' AND   '" + Convert.ToDateTime(form[p.Name + "End"]).ToString("yyyy/MM/dd 23:59:59") + "'");
                            }
                            else if (!form[p.Name + "Start"].IsEmpty())
                            {
                                sb.Append(" AND " + asStr + "." + p.Name + ">'" + ConvertToType(form[p.Name + "Start"], p.PropertyType) + "'");
                            }
                            else if (!form[p.Name + "End"].IsEmpty())
                            {
                                sb.Append(" AND " + asStr + "." + p.Name + "<'" + Convert.ToDateTime(form[p.Name + "End"]).ToString("yyyy/MM/dd 23:59:59") + "'");
                            }
                        }
                    }
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 获取表单数据转换为组合查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="form"></param>
        /// <returns></returns>
        public static string ToWhere<T>(HttpContextBase context, params string[] notcontainerfields) where T : new()
        {
            StringBuilder sb = new StringBuilder();
            T model = new T();
            Type type = typeof(T);
            PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo p in ps)
            {
                Type m = p.PropertyType;
                string value = context.Request[p.Name];
                if (!string.IsNullOrEmpty(value) && !p.Name.EqualsIgnoreCase(notcontainerfields))
                {
                    if (m == typeof(Int32) || m == typeof(Int32?) || m == typeof(Int64) || m == typeof(Decimal) || m == typeof(Single) || m == typeof(Decimal?) || m == typeof(Single?))
                    {
                        sb.Append(" AND " + p.Name + "=" + value);
                    }
                    else if (m == typeof(byte) || m == typeof(Boolean) || m == typeof(bool?))
                    {
                        sb.Append(" AND " + p.Name + "='" + value + "'");
                    }
                    else if (m == typeof(String) || m == typeof(string))
                    {
                        sb.Append(" AND " + p.Name + " LIKE '%" + value + "%'");
                    }
                    else
                    {
                        sb.Append(" AND " + p.Name + "='" + value + "'");
                    }
                }
                if (m == typeof(DateTime) || m == typeof(DateTime?))
                {
                    if (!context.Request[p.Name + "Start"].IsEmpty() && !context.Request[p.Name + "End"].IsEmpty())
                    {
                        sb.Append(" AND " + p.Name + " BETWEEN  '" + ConvertToType(context.Request[p.Name + "Start"], p.PropertyType) + "' AND   '" + Convert.ToDateTime(context.Request[p.Name + "End"]).ToString("yyyy/MM/dd 23:59:59") + "'");
                    }
                    else if (!context.Request[p.Name + "Start"].IsEmpty())
                    {
                        sb.Append(" AND " + p.Name + ">'" + ConvertToType(context.Request[p.Name + "Start"], p.PropertyType) + "'");
                    }
                    else if (!context.Request[p.Name + "End"].IsEmpty())
                    {
                        sb.Append(" AND " + p.Name + "<'" + Convert.ToDateTime(context.Request[p.Name + "End"]).ToString("yyyy/MM/dd 23:59:59") + "'");
                    }
                }
            }
            return sb.ToString();
        }
    }
}
