using System;
using System.Web;

namespace ZBClassLibrary
{
    public class CookieHelper
    {
        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookieName">cookieName</param>
        public static void ClearCookie(String cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                //cookie.Domain = ".test.com";
                cookie.Expires = DateTime.Now.AddYears(-1);

                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        //public static String GetCookieValue(String cookieName)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
        //    String str = String.Empty;
        //    if (cookie != null)
        //    {
        //        str = cookie.Value;
        //    }
        //    return str;
        //}

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookieName">cookie名</param>
        /// <param name="cookieValue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        //public static void SetCookie(String cookieName, String cookieValue, DateTime expires, bool httpOnly = true)
        //{
        //    HttpCookie cookie = new HttpCookie(cookieName, cookieValue);
        //    cookie.Expires = expires;
        //    //cookie.Domain = HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString();
        //    //cookie.Domain = ".test.com";
        //    cookie.HttpOnly = httpOnly;
        //    HttpContext.Current.Response.Cookies.Add(cookie);
        //}
        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookieName">cookie名</param>
        /// <param name="cookieValue">cookie值</param>
        //public static void SetCookie(String cookieName, String cookieValue, bool httpOnly = true)
        //{
        //    HttpCookie cookie = new HttpCookie(cookieName, cookieValue);
        //    //cookie.Domain = ".test.com";
        //    //cookie.Domain = HttpContext.Current.Request.ServerVariables["HTTP_HOST"].ToString();
        //    cookie.HttpOnly = httpOnly;
        //    HttpContext.Current.Response.Cookies.Add(cookie);
        //}
    }
}
