using IModelRespoity;
using ModelToSql.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Framework.Core.Security.Authentication;
using Easy.Framework.Web.Mvc;
using Web.Areas.BidMag.MyUiController;
using ZbClassLibrary.DTcms;

namespace Web.Areas.BidMag.Controllers
{
    public class LoginController : MyUiControllerBase
    {
        private readonly ISys_UserService _Isys_userservice;
        private readonly ISys_UserRepository _Isys_user;
        //
        // GET: /BidMag/Login/
        public LoginController(ISys_UserService Isys_userservice, ISys_UserRepository Isys_user)
        {
            _Isys_userservice = Isys_userservice;
            _Isys_user = Isys_user;
        }
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult LoginIn(Sys_User httpmodel)
        {
            HttpCookie cookie = Request.Cookies[DTKeys.SESSION_ADMIN_INFO + "_" + Request.Url.Port];
            var user = _Isys_user.GetAll(p => p.SysRealName == httpmodel.SysRealName && p.Password == httpmodel.Password).FirstOrDefault<Sys_User>();
            if (user == null)
            {
                //throw new Exception("用户名或密码错误！");
                return AjaxOkResponse("用户名或密码错误");
            }
            return AjaxOkResponse("登陆成功！");
        }
        public Sys_User Login()
        {
            return _Isys_user.GetAll(p => p.IsSuperManager == true).FirstOrDefault<Sys_User>();
        }
    }
}
