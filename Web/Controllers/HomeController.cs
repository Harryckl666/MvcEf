using IModelRespoity;
using ModelToSql.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ISys_UserService _Isysuserservice;
        private readonly ISys_UserRepository _Isysuser;
        private readonly ISys_lcyRepository _Isyslcy;
        private readonly ISys_lcyService _isys_lcyService;
        public HomeController(ISys_UserService Isysuserservice, ISys_lcyService isys_lcyService, ISys_lcyRepository Isyslcy, ISys_UserRepository Isysuser)
        {
            _Isysuserservice = Isysuserservice;
            _isys_lcyService = isys_lcyService;
            _Isyslcy = Isyslcy;
            _Isysuser = Isysuser;
        }
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //Sys_lcy model = new Sys_lcy { lName = "lcy" };
            //_isys_lcyService.Save(model);
            var lcyd = _isys_lcyService.getdata();
            List<Sys_lcy> list = _Isyslcy.GetAll(p => p.IsDeleted == false && p.Id.ToString() == "F5447A9A-C65D-4B21-A540-A67D00B9F8F0").ToList();
            List<Sys_lcy> nlist = _Isyslcy.GetAll(p => p.IsDeleted == false && p.Id.ToString() == "D7F7767B-553B-40BC-BF66-A67D00BA4AE6").ToList();
            var nmodel = _Isysuser.GetAll().Where<Sys_User>(p => p.IsDeleted == false).FirstOrDefault<Sys_User>();
            nmodel.syslcy = list;
            _Isysuserservice.Save(nmodel);

            var newmodel = new Sys_User { syslcy = nlist, SysRealName = "李宇春" };
            _Isysuserservice.Save(newmodel);
            var n = _Isysuserservice.getdata();
            return Content(n.ResultList[0].SysRealName.ToString() + lcyd.ResultList[0].lName.ToString());
        }

    }
}
