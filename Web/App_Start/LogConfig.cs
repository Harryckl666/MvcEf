using Easy.Framework.Logging.Log4Net;
using Easy.Framework.Utils.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.App_Start
{
    /// <summary>
    /// 日志初始化
    /// </summary>
    public static class LogConfig
    {
        public static void Initialize()
        {
            Log4NetLoggingInitializer.Initialize(new LoggingConfig(true, LogLevel.All));
        }
    }
}