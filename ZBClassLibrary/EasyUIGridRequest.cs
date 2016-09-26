using System;
using System.Web;

namespace ZbClassLibrary
{
    public class EasyUIGridRequest
    {
        /// <summary>
        /// 排序字段名称
        /// </summary>
        public string SortName { get; set; }

        /// <summary>
        /// Ids
        /// </summary>
        public string[] IdList { get; set; }
        /// <summary>
        /// Ids
        /// </summary>
        public string Ids { get; set; }
        /// <summary>
        /// 排序规则
        /// </summary>
        public bool SortOrder { get; set; }

        private int _PageNumber;
        /// <summary>
        /// 页号
        /// </summary>
        public int PageNumber
        {
            get
            {
                if (this._PageNumber <= 0)
                {
                    return 1;
                }
                else
                {
                    return _PageNumber;
                }
            }
            set
            {
                if (value <= 0)
                {
                    _PageNumber = 1;
                }
                else
                {
                    _PageNumber = value;
                }
            }
        }

        private int _PageSize;
        /// <summary>
        /// 每页的多少条数据
        /// </summary>
        public int PageSize
        {
            get
            {
                return this._PageSize;
            }
            set
            {
                this._PageSize = (value == 0 ? 20 : value);
            }
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public string Where { get; set; }

        /// <summary>
        /// 初始化读取信息
        /// </summary>
        /// <param name="context"></param>
        public EasyUIGridRequest(HttpContextBase context)
        {
            SortName = string.IsNullOrEmpty(context.Request.Form["sort"])?"":context.Request.Form["sort"];
            SortOrder =string.IsNullOrEmpty(context.Request.Form["order"])?false:context.Request.Form["order"] == "desc";
            _PageNumber = string.IsNullOrEmpty(context.Request.Form["page"])?0:Convert.ToInt32(context.Request.Form["page"]);
            PageSize = string.IsNullOrEmpty(context.Request.Form["rows"])? 0 : Convert.ToInt32(context.Request.Form["rows"]);
            IdList = (context.Request.Form["idList"] == null ? "" : context.Request.Form["idList"]).Split(',');
            Ids = context.Request.Form["idList"] == null ? "" : context.Request.Form["idList"];
        }
    }
}
