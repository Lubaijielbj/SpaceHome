using SpaceHome.BLLContainer;
using SpaseHome.IBLL;
using SpaseHome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaseHome.Web.Controllers
{
    public class UserController : BaseController
    {
        IUserInfoService userInfoService = UnityIOC.Resolve<IUserInfoService>();//创建UserInfoServic BLL接口层
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserInfo()
        {
            Account account = HttpContext.Session["account"] as Account;
            var userInfoList = userInfoService.LoadEnities(u => u.UserId == account.UserId);
            UserInfo userInfo = userInfoList.FirstOrDefault();
            if (userInfo == null)
            {
                return View(Url.Action("Index", "Login"));
            }
            else
            {
                return View(userInfo);
            }
            
        }
    }
}