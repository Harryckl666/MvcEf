using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;

namespace ZbClassLibrary.Security
{
    /// <summary>
    /// 授权验证类
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// 内部数据加解密密钥
        /// </summary>
        public const string CONST_CRYPT_KEY = "HGB_769as*uut7ta";
        /// <summary>
        /// 根据一系列数据生成授权序列
        /// </summary>
        /// <param name="key">授权密钥</param>
        /// <param name="data">授权的内容</param>
        /// <param name="version">0到255之间的版本号</param>
        /// <returns></returns>
        public static string Encrypt(string key, string data, int version)
        {
            return Encrypt(key, data, version, DateTime.Now.AddDays(1));
        }

        /// <summary>
        /// 根据一系列数据生成授权序列
        /// </summary>
        /// <param name="key">授权密钥</param>
        /// <param name="data">授权的内容</param>
        /// <param name="version">0到255之间的版本号</param>
        /// <param name="expires">有效期</param>
        /// <returns></returns>
        public static string Encrypt(string key, string data, int version, DateTime expires)
        {
            return FormsAuthentication.Encrypt(new FormsAuthenticationTicket(version, key, DateTime.Now, expires, true, data, "/"));
        }

        /// <summary>
        /// 解密授权序列
        /// 返回：已解密的授权内容
        /// </summary>
        /// <param name="ticket">待解密的授权序列</param>
        /// <param name="key">授权密钥</param>
        /// <param name="version">0到255之间的版本号</param>
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
        /// 从客户端Cookie中获取指定名称的授权
        /// </summary>
        /// <param name="name">授权名称</param>
        /// <returns></returns>
        public static Ticket Get(string name)
        {
            HttpCookie ticket = HttpContext.Current.Request.Cookies.Get(name);
            if (ticket != null)
                return new Ticket(ticket);
            return null;
        }

        /// <summary>
        /// 从客户端Cookie中获取指定名称的授权
        /// </summary>
        /// <param name="name">授权名称</param>
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
        /// 获取或设置授权名称
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
        /// 获取授权数据
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
        /// 获取或设置版本号
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
        /// 获取或设置授权域
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
        /// 获取或设置授权失效日期时间
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
        /// 获取或设置授权仅能通过HTTP传送而针对脚本隐藏
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
        /// 获取或设置授权有效路径
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
        /// 获取或设置是否通过SSL安全连接传输授权凭证
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
        /// 授权凭证构造函数
        /// </summary>
        /// <param name="name">授权名称</param>
        public Ticket(string name)
            : this(name, String.Empty)
        {
        }

        /// <summary>
        /// 授权凭证构造函数
        /// </summary>
        /// <param name="name">授权名称</param>
        /// <param name="data">授权数据</param>
        public Ticket(string name, string data)
            : this(name, data, 1)
        {
        }

        /// <summary>
        /// 授权凭证构造函数
        /// </summary>
        /// <param name="name">授权名称</param>
        /// <param name="data">授权数据</param>
        /// <param name="version">授权版本</param>
        public Ticket(string name, string data, int version)
        {
            this.name = name;
            this.data = data;
            this.version = version;
            this.expires = DateTime.Now.AddDays(1);
        }

        /// <summary>
        /// 颁布本授权凭证
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
        /// 验证本授权凭证的真实性
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
