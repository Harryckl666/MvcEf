using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Mvc;
using System.Web.Routing;
using Easy.Framework.Utils.Logging;
using Web.App_Start;

namespace Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            LogConfig.Initialize();
            ILogger logger = LogManager.GetLogger(typeof(MvcApplication));
            logger.Debug("网站启动");
            CastleConfig.Initialize();
            logger.Debug("依赖注入初始化完成");
            DatabaseConfig.Initialize();
            logger.Debug("数据库初始化完成");
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}