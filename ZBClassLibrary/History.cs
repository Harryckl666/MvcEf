using System;
using System.Collections.Generic;
using System.Text;
using ZbClassLibrary.Security;


namespace ZbClassLibrary
{
    /// <summary>
    /// 浏览记录
    /// </summary>
    public class HistoryView
    {
        private string _id = "";
        /// <summary>
        /// 记录Id
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        private string _title = "";
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        private string _thumb = "";
        /// <summary>
        /// 缩略图
        /// </summary>
        public string Thumb
        {
            set { _thumb = value; }
            get { return _thumb; }
        }
        private string _url = "";
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        private string _price = "";
        /// <summary>
        /// 价格
        /// </summary>
        public string Price
        {
            set { _price = value; }
            get { return _price; }
        }
        private DateTime _viewtime = DateTime.Now;
        /// <summary>
        /// 浏览时间
        /// </summary>
        public DateTime ViewTime
        {
            set { _viewtime = value; }
            get { return _viewtime; }
        }
    }

    /// <summary>
    /// 历史浏览记录
    /// </summary>
    public class History
    {
        private List<HistoryView> _HistoryViews = new List<HistoryView>();

        /// <summary>
        /// 历史浏览列表
        /// </summary>
        public List<HistoryView> HistoryViews
        {
            get { _HistoryViews.Reverse(); return _HistoryViews; }
        }

        /// <summary>
        /// 添加历史记录
        /// </summary>
        /// <param name="view"></param>
        public void AddView(HistoryView view)
        {
            if (_HistoryViews.Find(c => c.Id == view.Id) != null)
            {
                _HistoryViews.Remove(_HistoryViews.Find(c => c.Id == view.Id));
                _HistoryViews.Add(view);
            }
            else
            {
                _HistoryViews.Add(view);

                if(_HistoryViews.Count>5)
                {
                    _HistoryViews.Remove(_HistoryViews[0]);
                }
            }
        }

        /// <summary>
        /// 获取浏览历史记录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static History GetHistory(System.Web.HttpContextBase context)
        {
            string id = GetTicket();

            if (!string.IsNullOrEmpty(id))
            {
                object obj = Cache.getCacheObject(id);

                if (obj != null)
                {
                    return (History)obj;
                }
                else
                {
                    Cache.setCacheObject(id, new History(), 24.0);
                }
            }
            else
            {
                id = System.Guid.NewGuid().ToString();
                Cache.setCacheObject(id, new History(), 24.0);
            }

            return new History();
        }

        /// <summary>
        /// 设置浏览历史记录
        /// </summary>
        /// <param name="context"></param>
        /// <param name="history"></param>
        public static void SetHistory(System.Web.HttpContextBase context, History history)
        {
            string id = GetTicket();

            if (string.IsNullOrEmpty(id))
            {
                id = System.Guid.NewGuid().ToString();
                SetTicket("History", id);

                Cache.setCacheObject(id, history, 24.0);
            }
            else
            {
                Cache.setCacheObject(id, history, 24.0);
            }
        }

        /// <summary>
        /// 清空历史浏览记录
        /// </summary>
        public static void Clear()
        {
            string id = GetTicket();

            if (!string.IsNullOrEmpty(id))
            {
                Cache.removeCacheObject(id);
            }
        }

        /// <summary>
        /// 设置客户端授权信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        static void SetTicket(string name, string value)
        {
            Ticket ticket = new Ticket(name, value);
            ticket.Expires = DateTime.Now.AddHours(24.0);
            ticket.Issue("History");
        }

        /// <summary>
        /// 获取客户端授权信息
        /// </summary>
        /// <returns></returns>
        static string GetTicket()
        {
            Ticket ticket = Ticket.Get("History");

            if (Ticket.Authenticate(ticket))
            {
                return ticket.UserData;
            }

            return "";
        }

    }
}
