using IModelRespoity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.BidMag.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /BidMag/Home/

        private readonly ISys_UserService _Isys_userservice;
        private readonly ISys_UserRepository _Isys_user;
        public HomeController(ISys_UserService Isys_userservice, ISys_UserRepository Isys_user)
        {
            _Isys_userservice = Isys_userservice;
            _Isys_user = Isys_user;
        }
        public ActionResult Index()
        {
            return View();
        }

    }
}
