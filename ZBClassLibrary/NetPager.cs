using System;
using System.Collections.Generic;

using System.Text;

namespace ZbClassLibrary
{
    /// <summary>
    /// 内容分页
    /// </summary>
    public class NetPager
    {
        private int currentPage = 1;
        private int pageSize;
        private int pageCount;
        private long recordCount;

        #region Property
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return pageCount; }
            set { pageCount = value; }
        }

        /// <summary>
        /// 记录总条数
        /// </summary>
        public long RecordCount
        {
            get { return recordCount; }
            set { recordCount = value; }
        }

        /// <summary>
        /// 第一页页码
        /// </summary>
        public int FirstPage
        {
            get { return 1; }
        }

        /// <summary>
        /// 上一页页码
        /// </summary>
        public int PreviousPage
        {
            get { return this.GetPreviousPage(); }
        }

        /// <summary>
        /// 下一页页码
        /// </summary>
        public int NextPage
        {
            get { return this.GetNextPage(); }
        }

        /// <summary>
        /// 最后一页页码
        /// </summary>
        public int LastPage
        {
            get { return this.pageCount; }
        }
        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        public NetPager(int pageSize, long recordCount) : this(1, pageSize, recordCount) { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        public NetPager(int currentPage, int pageSize, long recordCount)
        {
            this.currentPage = currentPage;
            this.pageSize = pageSize;
            this.recordCount = recordCount;
            this.Init();
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        private void Init()
        {
            if (this.pageSize > 0 && this.recordCount > 0)
            {
                this.pageCount = (int)Math.Ceiling((double)this.recordCount / this.PageSize);

                if (this.currentPage > this.pageCount)
                {
                    this.currentPage = this.pageCount;
                }

                if (this.currentPage < 1)
                {
                    this.currentPage = 1;
                }
            }
        }

        /// <summary>
        /// 获取上一页
        /// </summary>
        /// <returns></returns>
        private int GetPreviousPage()
        {
            return this.currentPage > 1 ? this.currentPage - 1 : 1;
        }

        /// <summary>
        /// 获取下一页
        /// </summary>
        /// <returns></returns>
        private int GetNextPage()
        {
            return this.currentPage < this.pageCount ? this.currentPage + 1 : this.currentPage;
        }

        /// <summary>
        /// 输出不带数字的分页标签
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="currentClassNamr"></param>
        /// <param name="formatUrl"></param>
        /// <returns></returns>
        public static string WriteNoNumberPager(NetPager pager,string currentClassName, string formatUrl)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<a href=\"" + string.Format(formatUrl, pager.FirstPage) + "\">首页</a>");
            sb.Append("<a href=\"" + string.Format(formatUrl, pager.PreviousPage) + "\">上一页</a>");
            sb.Append("<a>第" + pager.currentPage + "/" + pager.PageCount + " 页</a>");
            sb.Append("<a href=\"" + string.Format(formatUrl, pager.NextPage) + "\">下一页</a>");
            sb.Append("<a href=\"" + string.Format(formatUrl, pager.LastPage) + "\">尾页</a>");

            return sb.ToString();
        }

        /// <summary>
        /// 输出带数字的分页标签
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="currentClassNamr"></param>
        /// <param name="formatUrl"></param>
        /// <returns></returns>
        public static string WriteNumberPager(NetPager pager, string currentClassName, string formatUrl)
        {
            StringBuilder sb = new StringBuilder();

            if (pager.CurrentPage > 1)
            {
                sb.Append("<a  href=\"" + string.Format(formatUrl, pager.PreviousPage) + "\">上一页</a>");
            }
            else
            {
                sb.Append("<a disabled=\"disabled\" href=\"javascript:;\">上一页</a>");
            }

            //只有1页或者0页的情况
            if (pager.pageCount <= 1)
            {
                return "";
            }
            else if (pager.pageCount > 1 && pager.PageCount <= 10 && pager.CurrentPage <= 6)
            {
                for (int i = 1; i <= pager.PageCount; i++)
                {
                    if (i == pager.currentPage)
                    {
                        sb.Append("<a class=\"" + currentClassName + "\">" + i + "</a>");
                    }
                    else
                    {
                        sb.Append("<a href=\"" + string.Format(formatUrl, i) + "\">" + i + "</a>");
                    }
                }
            }
            else if (pager.PageCount > 10 && pager.CurrentPage <= 6)
            {
                for (int i = 1; i <= 10; i++)
                {
                    if (i == pager.currentPage)
                    {
                        sb.Append("<a class=\"" + currentClassName + "\">" + i + "</a>");
                    }
                    else
                    {
                        sb.Append("<a href=\"" + string.Format(formatUrl, i) + "\">" + i + "</a>");
                    }
                }
            }
            else if (pager.PageCount > 10 && pager.CurrentPage > 6)
            {
                sb.Append("<a  href=\"" + string.Format(formatUrl, 10) + "\">1</a>");
                sb.Append("<a class=\"split\">...</a>");

                if (pager.CurrentPage < pager.PageCount - 4)
                {
                    for (int i = pager.CurrentPage - 3; i <= pager.CurrentPage + 4; i++)
                    {
                        if (i == pager.currentPage)
                        {
                            sb.Append("<a class=\"" + currentClassName + "\">" + i + "</a>");
                        }
                        else
                        {
                            sb.Append("<a href=\"" + string.Format(formatUrl, i) + "\">" + i + "</a>");
                        }
                    }
                }
                else
                {
                    for (int i = pager.PageCount - 8; i <= pager.PageCount; i++)
                    {
                        if (i == pager.currentPage)
                        {
                            sb.Append("<a class=\"" + currentClassName + "\">" + i + "</a>");
                        }
                        else
                        {
                            sb.Append("<a href=\"" + string.Format(formatUrl, i) + "\">" + i + "</a>");
                        }
                    }
                }
            }

            if (pager.CurrentPage != pager.LastPage)
            {
                sb.Append("<a  href=\"" + string.Format(formatUrl, pager.NextPage) + "\">下一页</a>");
            }
            else
            {
                sb.Append("<a disabled=\"disabled\" href=\"javascript:;\">下一页</a>");
            }

            return sb.ToString();
        }
    }
}
