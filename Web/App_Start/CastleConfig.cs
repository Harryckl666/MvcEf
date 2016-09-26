using System;
using Easy.Framework.Core.Dependency;
using Easy.Framework.Core.Security.Authentication;
using Easy.Framework.Web.Mvc.Dependency;
using Easy.Framework.Web.Mvc.Security.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.App_Start
{
    /// <summary>
    /// 依赖注入(IOC)容器初始化
    /// </summary>
    public static class CastleConfig
    {
        public static void Initialize()
        {
            MvcBootstrapper mvcBootstrapper = new MvcBootstrapper(new ConventionalRegistrarConfig());
            //依赖注入初始化
            mvcBootstrapper.Initialize();
            mvcBootstrapper.IocManager.Register<ISignInManager, IdentitySignInManager>(DependencyLifeStyle.Transient);
        }
    }
}