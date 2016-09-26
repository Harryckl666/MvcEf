using System.Web.Mvc;

namespace Web.Areas.BidMag
{
    public class BidMagAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BidMag";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BidMag_default",
                "BidMag/{controller}/{action}/{id}",
                new { Controller = "Login", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Web.Areas.BidMag.Controllers" }
            );
        }
    }
}
