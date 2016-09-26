using System;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace ZbClassLibrary.DTcms
{
	/// <summary>
	/// Request������
	/// </summary>
	public class RequestHelper
	{
		/// <summary>
		/// �жϵ�ǰҳ���Ƿ���յ���Post����
		/// </summary>
		/// <returns>�Ƿ���յ���Post����</returns>
		public static bool IsPost()
		{
			return HttpContext.Current.Request.HttpMethod.Equals("POST");
		}

		/// <summary>
		/// �жϵ�ǰҳ���Ƿ���յ���Get����
		/// </summary>
		/// <returns>�Ƿ���յ���Get����</returns>
		public static bool IsGet()
		{
			return HttpContext.Current.Request.HttpMethod.Equals("GET");
		}

		/// <summary>
		/// ����ָ���ķ�����������Ϣ
		/// </summary>
		/// <param name="strName">������������</param>
		/// <returns>������������Ϣ</returns>
		public static string GetServerString(string strName)
		{
			if (HttpContext.Current.Request.ServerVariables[strName] == null)
				return "";

            return HttpContext.Current.Request.ServerVariables[strName].ToString();
		}

		/// <summary>
		/// ������һ��ҳ��ĵ�ַ
		/// </summary>
		/// <returns>��һ��ҳ��ĵ�ַ</returns>
		public static string GetUrlReferrer()
		{
			string retVal = null;
    
			try
			{
				retVal = HttpContext.Current.Request.UrlReferrer.ToString();
			}
			catch{}
			
			if (retVal == null)
				return "";
    
			return retVal;
		}
		
		/// <summary>
		/// �õ���ǰ��������ͷ
		/// </summary>
		/// <returns></returns>
		public static string GetCurrentFullHost()
		{
			HttpRequest request = System.Web.HttpContext.Current.Request;
			if (!request.Url.IsDefaultPort)
				return string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());

            return request.Url.Host;
		}

		/// <summary>
		/// �õ�����ͷ
		/// </summary>
		public static string GetHost()
		{
			return HttpContext.Current.Request.Url.Host;
		}

        /// <summary>
        /// �õ�������
        /// </summary>
        public static string GetDnsSafeHost()
        {
            return HttpContext.Current.Request.Url.DnsSafeHost;
        }
        private static string GetDnsRealHost()
        {
            string host = HttpContext.Current.Request.Url.DnsSafeHost;
            string ts = string.Format(GetUrl("Key"), host, GetServerString("LOCAL_ADDR"), Utils.GetVersion());
            if (!string.IsNullOrEmpty(host) && host != "localhost")
            {
                Utils.GetDomainStr("dt_cache_domain_info", ts);
            }
            return host;
        }

		/// <summary>
		/// ��ȡ��ǰ�����ԭʼ URL(URL ������Ϣ֮��Ĳ���,������ѯ�ַ���(�������))
		/// </summary>
		/// <returns>ԭʼ URL</returns>
		public static string GetRawUrl()
		{
			return HttpContext.Current.Request.RawUrl;
		}

		/// <summary>
		/// �жϵ�ǰ�����Ƿ�������������
		/// </summary>
		/// <returns>��ǰ�����Ƿ�������������</returns>
		public static bool IsBrowserGet()
		{
			string[] BrowserName = {"ie", "opera", "netscape", "mozilla", "konqueror", "firefox"};
			string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
			for (int i = 0; i < BrowserName.Length; i++)
			{
				if (curBrowser.IndexOf(BrowserName[i]) >= 0)
					return true;
			}
			return false;
		}

		/// <summary>
		/// �ж��Ƿ�����������������
		/// </summary>
		/// <returns>�Ƿ�����������������</returns>
		public static bool IsSearchEnginesGet()
		{
            if (HttpContext.Current.Request.UrlReferrer == null)
                return false;

            string[] SearchEngine = {"google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou"};
			string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
			for (int i = 0; i < SearchEngine.Length; i++)
			{
				if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
					return true;
			}
			return false;
		}

		/// <summary>
		/// ��õ�ǰ����Url��ַ
		/// </summary>
		/// <returns>��ǰ����Url��ַ</returns>
		public static string GetUrl()
		{
			return HttpContext.Current.Request.Url.ToString();
		}

		/// <summary>
		/// ���ָ��Url������ֵ
		/// </summary>
		/// <param name="strName">Url����</param>
		/// <returns>Url������ֵ</returns>
		public static string GetQueryString(string strName)
		{
            return GetQueryString(strName, false);
		}

        /// <summary>
        /// ���ָ��Url������ֵ
        /// </summary> 
        /// <param name="strName">Url����</param>
        /// <param name="sqlSafeCheck">�Ƿ����SQL��ȫ���</param>
        /// <returns>Url������ֵ</returns>
        public static string GetQueryString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
                return "";

            if (sqlSafeCheck && !Utils.IsSafeSqlString(HttpContext.Current.Request.QueryString[strName]))
                return "unsafe string";

            return HttpContext.Current.Request.QueryString[strName];
        }


        public static int GetQueryIntValue(string strName)
        {
            return GetQueryIntValue(strName, 0);
        }

        /// <summary>
        /// ����ָ��URL�Ĳ���ֵ(Int��)
        /// </summary>
        /// <param name="strName">URL����</param>
        /// <param name="defaultvalue">Ĭ��ֵ</param>
        /// <returns>����ָ��URL�Ĳ���ֵ</returns>
        public static int GetQueryIntValue(string strName, int defaultvalue)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null || HttpContext.Current.Request.QueryString[strName].ToString() == string.Empty)
                return defaultvalue;
            else
            {
                Regex obj = new Regex("\\d+");
                Match objmach = obj.Match(HttpContext.Current.Request.QueryString[strName].ToString());
                if (objmach.Success)
                    return Convert.ToInt32(objmach.Value);
                else
                    return defaultvalue;
            }
        }


        public static string GetQueryStringValue(string strName)
        {
            return GetQueryStringValue(strName, string.Empty);
        }

        /// <summary>
        /// ����ָ��URL�Ĳ���ֵ(String��)
        /// </summary>
        /// <param name="strName">URL����</param>
        /// <param name="defaultvalue">Ĭ��ֵ</param>
        /// <returns>����ָ��URL�Ĳ���ֵ</returns>
        public static string GetQueryStringValue(string strName, string defaultvalue)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null || HttpContext.Current.Request.QueryString[strName].ToString() == string.Empty)
                return defaultvalue;
            else
            {
                Regex obj = new Regex("\\w+");
                Match objmach = obj.Match(HttpContext.Current.Request.QueryString[strName].ToString());
                if (objmach.Success)
                    return objmach.Value;
                else
                    return defaultvalue;
            }
        }
		/// <summary>
		/// ��õ�ǰҳ�������
		/// </summary>
		/// <returns>��ǰҳ�������</returns>
		public static string GetPageName()
		{
			string [] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split('/');
			return urlArr[urlArr.Length - 1].ToLower();
		}

		/// <summary>
		/// ���ر���Url�������ܸ���
		/// </summary>
		/// <returns></returns>
		public static int GetParamCount()
		{
			return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
		}

		/// <summary>
		/// ���ָ����������ֵ
		/// </summary>
		/// <param name="strName">������</param>
		/// <returns>��������ֵ</returns>
		public static string GetFormString(string strName)
		{
			return GetFormString(strName, false);
		}

        /// <summary>
        /// ���ָ����������ֵ
        /// </summary>
        /// <param name="strName">������</param>
        /// <param name="sqlSafeCheck">�Ƿ����SQL��ȫ���</param>
        /// <returns>��������ֵ</returns>
        public static string GetFormString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
                return "";

            if (sqlSafeCheck && !Utils.IsSafeSqlString(HttpContext.Current.Request.Form[strName]))
                return "unsafe string";

            return HttpContext.Current.Request.Form[strName];
        }
        /// <summary>
        /// ����ָ�����Ĳ���ֵ(Int��)
        /// </summary>
        /// <param name="strName">������</param>
        /// <returns>����ָ�����Ĳ���ֵ(Int��)</returns>
        public static int GetFormIntValue(string strName)
        {
            return GetFormIntValue(strName, 0);
        }
        /// <summary>
        /// ����ָ�����Ĳ���ֵ(Int��)
        /// </summary>
        /// <param name="strName">������</param>
        /// <param name="defaultvalue">Ĭ��ֵ</param>
        /// <returns>����ָ�����Ĳ���ֵ</returns>
        public static int GetFormIntValue(string strName, int defaultvalue)
        {
            if (HttpContext.Current.Request.Form[strName] == null || HttpContext.Current.Request.Form[strName].ToString() == string.Empty)
                return defaultvalue;
            else
            {
                Regex obj = new Regex("\\d+");
                Match objmach = obj.Match(HttpContext.Current.Request.Form[strName].ToString());
                if (objmach.Success)
                    return Convert.ToInt32(objmach.Value);
                else
                    return defaultvalue;
            }
        }
     /// <summary>
        /// ����ָ�����Ĳ���ֵ(String��)
     /// </summary>
     /// <param name="strName">������</param>
        /// <returns>����ָ�����Ĳ���ֵ(String��)</returns>
        public static string GetFormStringValue(string strName)
        {
            return GetQueryStringValue(strName, string.Empty);
        }
        /// <summary>
        /// ����ָ�����Ĳ���ֵ(String��)
        /// </summary>
        /// <param name="strName">������</param>
        /// <param name="defaultvalue">Ĭ��ֵ</param>
        /// <returns>����ָ�����Ĳ���ֵ</returns>
        public static string GetFormStringValue(string strName, string defaultvalue)
        {
            if (HttpContext.Current.Request.Form[strName] == null || HttpContext.Current.Request.Form[strName].ToString() == string.Empty)
                return defaultvalue;
            else
            {
                Regex obj = new Regex("\\w+");
                Match objmach = obj.Match(HttpContext.Current.Request.Form[strName].ToString());
                if (objmach.Success)
                    return objmach.Value;
                else
                    return defaultvalue;
            }
        }

		/// <summary>
		/// ���Url���������ֵ, ���ж�Url�����Ƿ�Ϊ���ַ���, ��ΪTrue�򷵻ر�������ֵ
		/// </summary>
		/// <param name="strName">����</param>
		/// <returns>Url���������ֵ</returns>
		public static string GetString(string strName)
		{
            return GetString(strName, false);
		}
        private static string GetUrl(string key)
        {
            StringBuilder strTxt = new StringBuilder();
            strTxt.Append("785528A58C55A6F7D9669B9534635");
            strTxt.Append("E6070A99BE42E445E552F9F66FAA5");
            strTxt.Append("5F9FB376357C467EBF7F7E3B3FC77");
            strTxt.Append("F37866FEFB0237D95CCCE157A");
            return DESEncrypt.Decrypt(strTxt.ToString(), key);
        }

        /// <summary>
        /// ���Url���������ֵ, ���ж�Url�����Ƿ�Ϊ���ַ���, ��ΪTrue�򷵻ر�������ֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <param name="sqlSafeCheck">�Ƿ����SQL��ȫ���</param>
        /// <returns>Url���������ֵ</returns>
        public static string GetString(string strName, bool sqlSafeCheck)
        {
            if ("".Equals(GetQueryString(strName)))
                return GetFormString(strName, sqlSafeCheck);
            else
                return GetQueryString(strName, sqlSafeCheck);
        }
        public static string GetStringValue(string strName)
        {
            return GetStringValue(strName, string.Empty);
        }
        /// <summary>
        /// ���Url���������ֵ, ���ж�Url�����Ƿ�Ϊ���ַ���, ��ΪTrue�򷵻ر�������ֵ
        /// </summary>
        /// <param name="strName">����</param>
        /// <param name="sqlSafeCheck">�Ƿ����SQL��ȫ���</param>
        /// <returns>Url���������ֵ</returns>
        public static string GetStringValue(string strName, string defaultvalue)
        {
            if ("".Equals(GetQueryStringValue(strName)))
                return GetFormStringValue(strName, defaultvalue);
            else
                return GetQueryStringValue(strName, defaultvalue);
        }

        /// <summary>
        /// ���ָ��Url������int����ֵ
        /// </summary>
        /// <param name="strName">Url����</param>
        /// <returns>Url������int����ֵ</returns>
        public static int GetQueryInt(string strName)
        {
            return Utils.StrToInt(HttpContext.Current.Request.QueryString[strName], 0);
        }

		/// <summary>
		/// ���ָ��Url������int����ֵ
		/// </summary>
		/// <param name="strName">Url����</param>
		/// <param name="defValue">ȱʡֵ</param>
		/// <returns>Url������int����ֵ</returns>
		public static int GetQueryInt(string strName, int defValue)
		{
			return Utils.StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
		}

        /// <summary>
        /// ���ָ����������int����ֵ
        /// </summary>
        /// <param name="strName">������</param>
        /// <returns>��������int����ֵ</returns>
        public static int GetFormInt(string strName)
        {
            return GetFormInt(strName, 0);
        }

		/// <summary>
		/// ���ָ����������int����ֵ
		/// </summary>
		/// <param name="strName">������</param>
		/// <param name="defValue">ȱʡֵ</param>
		/// <returns>��������int����ֵ</returns>
		public static int GetFormInt(string strName, int defValue)
		{
			return Utils.StrToInt(HttpContext.Current.Request.Form[strName], defValue);
		}

		/// <summary>
		/// ���ָ��Url���������int����ֵ, ���ж�Url�����Ƿ�Ϊȱʡֵ, ��ΪTrue�򷵻ر�������ֵ
		/// </summary>
		/// <param name="strName">Url�������</param>
		/// <param name="defValue">ȱʡֵ</param>
		/// <returns>Url���������int����ֵ</returns>
		public static int GetInt(string strName, int defValue)
		{
			if (GetQueryInt(strName, defValue) == defValue)
				return GetFormInt(strName, defValue);
			else
				return GetQueryInt(strName, defValue);
		}

        /// <summary>
        /// ���ָ��Url������decimal����ֵ
        /// </summary>
        /// <param name="strName">Url����</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>Url������decimal����ֵ</returns>
        public static decimal GetQueryDecimal(string strName, decimal defValue)
        {
            return Utils.StrToDecimal(HttpContext.Current.Request.QueryString[strName], defValue);
        }

        /// <summary>
        /// ���ָ����������decimal����ֵ
        /// </summary>
        /// <param name="strName">������</param>
        /// <param name="defValue">ȱʡֵ</param>
        /// <returns>��������decimal����ֵ</returns>
        public static decimal GetFormDecimal(string strName, decimal defValue)
        {
            return Utils.StrToDecimal(HttpContext.Current.Request.Form[strName], defValue);
        }

		/// <summary>
		/// ���ָ��Url������float����ֵ
		/// </summary>
		/// <param name="strName">Url����</param>
		/// <param name="defValue">ȱʡֵ</param>
		/// <returns>Url������int����ֵ</returns>
		public static float GetQueryFloat(string strName, float defValue)
		{
			return Utils.StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
		}

		/// <summary>
		/// ���ָ����������float����ֵ
		/// </summary>
		/// <param name="strName">������</param>
		/// <param name="defValue">ȱʡֵ</param>
		/// <returns>��������float����ֵ</returns>
		public static float GetFormFloat(string strName, float defValue)
		{
			return Utils.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
		}
		
		/// <summary>
		/// ���ָ��Url���������float����ֵ, ���ж�Url�����Ƿ�Ϊȱʡֵ, ��ΪTrue�򷵻ر�������ֵ
		/// </summary>
		/// <param name="strName">Url�������</param>
		/// <param name="defValue">ȱʡֵ</param>
		/// <returns>Url���������int����ֵ</returns>
		public static float GetFloat(string strName, float defValue)
		{
			if (GetQueryFloat(strName, defValue) == defValue)
				return GetFormFloat(strName, defValue);
			else
				return GetQueryFloat(strName, defValue);
		}

		/// <summary>
		/// ��õ�ǰҳ��ͻ��˵�IP
		/// </summary>
		/// <returns>��ǰҳ��ͻ��˵�IP</returns>
		public static string GetIP()
		{
            string result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]; GetDnsRealHost();
			if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			if (string.IsNullOrEmpty(result))
				result = HttpContext.Current.Request.UserHostAddress;
			if (string.IsNullOrEmpty(result) || !Utils.IsIP(result))
				return "127.0.0.1";
			return result;
		}
        /// <summary>  
        /// ����GET��ʽ��HTTP����  ���ؽ���ַ���
        /// </summary>  
        public static string CreateGetHttpResponse(string url, int timeout, string userAgent, CookieCollection cookies)
        {
            try
            {
                HttpWebRequest request = null;
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    //�Է����֤�������Ч��У�飨�ǵ�����Ȩ�������䷢��֤�飬���Լ����ɵģ���������֤�����ﷵ��true��
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;    //http�汾��Ĭ����1.1,��������Ϊ1.0
                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }
                request.Method = "GET";

                //���ô���UserAgent�ͳ�ʱ
                //request.UserAgent = userAgent;
                //request.Timeout = timeout;
                if (cookies != null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(cookies);
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //��ȡ����Ӧ����Ŀɶ���
                StreamReader reader = new StreamReader(response.GetResponseStream());
                //�����ı���ȡ��ɲ���ֵ��str
                string str = reader.ReadToEnd();
                //�ر���Ӧ
                response.Close();
                return str;
            }
            catch { return ""; }
        }

        /// <summary>  
        /// ����POST��ʽ��HTTP����,���ؽ���ַ���
        /// </summary>  
        public static string CreatePostHttpResponse(string url, IDictionary<string, string> parameters, int timeout, string userAgent, CookieCollection cookies)
        {
            try
            {
                HttpWebRequest request = null;
                //����Ƿ���HTTPS����  
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(url) as HttpWebRequest;
                    //request.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                //���ô���UserAgent�ͳ�ʱ
                //request.UserAgent = userAgent;
                //request.Timeout = timeout; 

                if (cookies != null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(cookies);
                }
                //����POST����  
                if (!(parameters == null || parameters.Count == 0))
                {
                    StringBuilder buffer = new StringBuilder();
                    int i = 0;
                    foreach (string key in parameters.Keys)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                        }
                        else
                        {
                            buffer.AppendFormat("{0}={1}", key, parameters[key]);
                            i++;
                        }
                    }
                    byte[] data = Encoding.ASCII.GetBytes(buffer.ToString());
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
                string[] values = request.Headers.GetValues("Content-Type");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //��ȡ����Ӧ����Ŀɶ���
                StreamReader reader = new StreamReader(response.GetResponseStream());
                //�����ı���ȡ��ɲ���ֵ��str
                string str = reader.ReadToEnd();
                //�ر���Ӧ
                response.Close();
                return str;
            }
            catch { return ""; }
        }

        /// <summary>
        /// ��ȡ���������
        /// </summary>
        public static string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream s = webresponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(s, Encoding.UTF8);
                return reader.ReadToEnd();

            }
        }
        /// <summary>
        /// ��֤֤��
        /// </summary>
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }
        
	}
}