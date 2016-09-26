using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //启用Attribute路由
            //routes.MapMvcAttributeRoutes(); 

           // routes.Add("DomainRouteForManage", new DomainRoute(
           //       "www.abc.com",                             // 固定的二级域名
           //       "{controller}/{action}/{id}",                  // URL with parameters
           //       new
           //       {
           //           host = "",
           //           controller = "Default",
           //           action = "Index",
           //           id = UrlParameter.Optional,
           //           Namespaces = new string[] { "Easy.Framework.Site.Web.Controllers" }
           //       }
           //   ));

           // routes.Add("DomainRouteForManage1", new DomainRoute(
           //    "{host}.abc.com",                             // 固定的二级域名
           //    "{controller}/{action}/{id}",                  // URL with parameters
           //    new
           //    {
           //        host = "",
           //        controller = "Default",
           //        action = "Index",
           //        id = UrlParameter.Optional,
           //        Namespaces = new string[] { "Easy.Framework.Site.Web.Controllers" }
           //    }
           //));

           // routes.MapRoute(
           //     name: "Default",
           //     url: "{controller}/{action}/{id}",
           //     defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional },
           //     namespaces: new string[] { "Easy.Framework.Site.Web.Controllers" }
           // );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}