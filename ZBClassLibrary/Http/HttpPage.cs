using System.Collections.Generic;
using System.Web;

namespace ZbClassLibrary
{
    // ReSharper disable once UnusedTypeParameter
    public class HttpPage<T>
    {
        public HttpPage()
        {
            int pageIndex = 1;
            int.TryParse(HttpContext.Current.Request.Form["page"] ?? HttpContext.Current.Request["page"], out pageIndex);
            this.PageIndex = pageIndex == 0 ? 1 : pageIndex;
            int pageSize = 10;
            int.TryParse(HttpContext.Current.Request.Form["rows"] ?? HttpContext.Current.Request["rows"], out pageSize);
            this.PageSize = pageSize == 0 ? 10 : pageSize;
            this.Parameter = HttpContext.Current.Request.Form ?? HttpContext.Current.Request.Params;
            string ordering = HttpContext.Current.Request.Form["ordering"] ?? HttpContext.Current.Request["ordering"];
            this.Ordering = !string.IsNullOrEmpty(ordering) ? ordering : "Id";
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public System.Collections.Specialized.NameValueCollection Parameter { get; set; }
        public IList<T> ResultList { get; set; }
        public string Ordering { get; set; }
    }


}