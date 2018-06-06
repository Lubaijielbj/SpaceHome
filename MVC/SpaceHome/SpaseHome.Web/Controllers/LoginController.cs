using SpaceHome.BLLContainer;
using SpaseHome.IBLL;
using SpaseHome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpaseHome.Common;

namespace SpaseHome.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult GetCheckCode()
        {
            string checkCode = Common.CheckCode.CreateCheckCode(4);
            Session["checkCode"] = checkCode;
            byte[] b = Common.CheckCode.CheckCodeImg(checkCode);
            return File(b, "image/jpg");
        }

        public bool VerifyCheckCode()
        {
            string verifyCheckCode = Request["checkCode"];
            string checkCode = Session["checkCode"].ToString();

            return string.Equals(verifyCheckCode, checkCode, StringComparison.CurrentCultureIgnoreCase);
        }

        public ActionResult RegData()
        {
            IAccountService accountService = UnityIOC.Resolve<IAccountService>();

            Account account = new Account { RegTime = DateTime.Now, NickName = Guid.NewGuid().ToString() };

            string type = Request["type"];

            if (string.Equals(type, "phone"))
            {
                string phone = Request["phone"];
                string pwd = Request["pwd"];

                account.UserPhone = phone;
                account.PassWord = MD5Helper.MD5Encrypt64(pwd);

                var s = accountService.LoadEnities(u => u.UserPhone == account.UserPhone);

                string aa = "zhangmanerhuo";
            }
            if (string.Equals(type, "mail"))
            {
                string mail = Request["mail"];
                string pwd = Request["pwd"];
            }

            return Content("eeror");
        }

        public ActionResult Test()
        {
            IAccountService account = UnityIOC.Resolve<IAccountService>();
            ViewBag.account = account.Test();
            return View();
        }
    }
}