using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;

namespace ZbClassLibrary.Security
{
    /// <summary>
    /// ��Ȩ��֤��
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// �ڲ����ݼӽ�����Կ
        /// </summary>
        public const string CONST_CRYPT_KEY = "HGB_769as*uut7ta";
        /// <summary>
        /// ����һϵ������������Ȩ����
        /// </summary>
        /// <param name="key">��Ȩ��Կ</param>
        /// <param name="data">��Ȩ������</param>
        /// <param name="version">0��255֮��İ汾��</param>
        /// <returns></returns>
        public static string Encrypt(string key, string data, int version)
        {
            return Encrypt(key, data, version, DateTime.Now.AddDays(1));
        }

        /// <summary>
        /// ����һϵ������������Ȩ����
        /// </summary>
        /// <param name="key">��Ȩ��Կ</param>
        /// <param name="data">��Ȩ������</param>
        /// <param name="version">0��255֮��İ汾��</param>
        /// <param name="expires">��Ч��</param>
        /// <returns></returns>
        public static string Encrypt(string key, string data, int version, DateTime expires)
        {
            return FormsAuthentication.Encrypt(new FormsAuthenticationTicket(version, key, DateTime.Now, expires, true, data, "/"));
        }

        /// <summary>
        /// ������Ȩ����
        /// ���أ��ѽ��ܵ���Ȩ����
        /// </summary>
        /// <param name="ticket">�����ܵ���Ȩ����</param>
        /// <param name="key">��Ȩ��Կ</param>
        /// <param name="version">0��255֮��İ汾��</param>
        /// <returns></returns>
        public static string Decrypt(string ticket, string key, int version)
        {
            try
            {
                FormsAuthenticationTicket fat = FormsAuthentication.Decrypt(ticket);
                if (fat != null && fat.Name == key && fat.Version == version)
                    return fat.UserData;
            }
            catch
            {
            }
            return null;
        }

        /// <summary>
        /// �ӿͻ���Cookie�л�ȡָ�����Ƶ���Ȩ
        /// </summary>
        /// <param name="name">��Ȩ����</param>
        /// <returns></returns>
        public static Ticket Get(string name)
        {
            HttpCookie ticket = HttpContext.Current.Request.Cookies.Get(name);
            if (ticket != null)
                return new Ticket(ticket);
            return null;
        }

        /// <summary>
        /// �ӿͻ���Cookie�л�ȡָ�����Ƶ���Ȩ
        /// </summary>
        /// <param name="name">��Ȩ����</param>
        /// <returns></returns>
        public static void Remove(string name)
        {
            HttpCookie ticket = HttpContext.Current.Request.Cookies.Get(name);
            if (ticket != null)
            {
                ticket.Expires = DateTime.Now.AddYears(-100);
                HttpContext.Current.Response.Cookies.Add(ticket);
            }
        }

        private string ticket = String.Empty;

        private string name = String.Empty;

        /// <summary>
        /// ��ȡ��������Ȩ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        private string data = String.Empty;
        /// <summary>
        /// ��ȡ��Ȩ����
        /// </summary>
        public string UserData
        {
            get
            {
                return this.data;
            }
        }

        private int version = 1;
        /// <summary>
        /// ��ȡ�����ð汾��
        /// </summary>
        public int Version
        {
            get
            {
                return this.version;
            }
            set
            {
                this.version = value;
            }
        }

        private string domain = String.Empty;
        /// <summary>
        /// ��ȡ��������Ȩ��
        /// </summary>
        public string Domain
        {
            get
            {
                return this.domain;
            }
            set
            {
                this.domain = value;
            }
        }

        private DateTime expires;
        /// <summary>
        /// ��ȡ��������ȨʧЧ����ʱ��
        /// </summary>
        public DateTime Expires
        {
            get
            {
                return this.expires;
            }
            set
            {
                this.expires = value;
            }
        }

        private bool httpOnly = true;
        /// <summary>
        /// ��ȡ��������Ȩ����ͨ��HTTP���Ͷ���Խű�����
        /// </summary>
        public bool HttpOnly
        {
            get
            {
                return this.httpOnly;
            }
            set
            {
                this.httpOnly = value;
            }
        }

        private string path = "/";
        /// <summary>
        /// ��ȡ��������Ȩ��Ч·��
        /// </summary>
        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
            }
        }

        private bool secure = false;
        /// <summary>
        /// ��ȡ�������Ƿ�ͨ��SSL��ȫ���Ӵ�����Ȩƾ֤
        /// </summary>
        public bool Secure
        {
            get
            {
                return this.secure;
            }
            set
            {
                this.secure = value;
            }
        }

        private Ticket(HttpCookie cookie)
        {
            this.name = cookie.Name;
            this.path = cookie.Path;

            this.ticket = cookie.Value;
        }

        /// <summary>
        /// ��Ȩƾ֤���캯��
        /// </summary>
        /// <param name="name">��Ȩ����</param>
        public Ticket(string name)
            : this(name, String.Empty)
        {
        }

        /// <summary>
        /// ��Ȩƾ֤���캯��
        /// </summary>
        /// <param name="name">��Ȩ����</param>
        /// <param name="data">��Ȩ����</param>
        public Ticket(string name, string data)
            : this(name, data, 1)
        {
        }

        /// <summary>
        /// ��Ȩƾ֤���캯��
        /// </summary>
        /// <param name="name">��Ȩ����</param>
        /// <param name="data">��Ȩ����</param>
        /// <param name="version">��Ȩ�汾</param>
        public Ticket(string name, string data, int version)
        {
            this.name = name;
            this.data = data;
            this.version = version;
            this.expires = DateTime.Now.AddDays(1);
        }

        /// <summary>
        /// �䲼����Ȩƾ֤
        /// </summary>
        public void Issue(string cookieName)
        {
            HttpCookie cookie = new HttpCookie(cookieName, Encrypt(this.name, this.data, this.version));

            cookie.Path = this.path;
            cookie.Domain = this.domain;
            cookie.Expires = this.expires;
            cookie.HttpOnly = this.httpOnly;
            cookie.Secure = this.secure;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// ��֤����Ȩƾ֤����ʵ��
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public static bool Authenticate(Ticket ticket)
        {
            if (ticket != null)
            {
                try
                {
                    FormsAuthenticationTicket fat = FormsAuthentication.Decrypt(ticket.ticket);
                    if (fat != null && !fat.Expired)
                    {
                        ticket.name = fat.Name;
                        ticket.data = fat.UserData;
                        ticket.version = fat.Version;
                        ticket.expires = fat.Expiration;
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}
