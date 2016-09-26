using ModelToSql;
using ModelToSql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Web.App_Start
{
    /// <summary>
    /// 数据库初始化
    /// </summary>
    public static class DatabaseConfig
    {
        public static void Initialize()
        {
            //数据库初始化
            //Database.SetInitializer(new DatabaseInitializeStrategy());
            //new DemoDbContext().Database.Initialize(true);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EasyDbContext, DatabaseInitializeStrategy>());
        }
    }
}