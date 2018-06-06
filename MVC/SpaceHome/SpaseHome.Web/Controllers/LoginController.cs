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

        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCheckCode()
        {
            string checkCode = Common.CheckCode.CreateCheckCode(4);
            Session["checkCode"] = checkCode;
            byte[] b = Common.CheckCode.CheckCodeImg(checkCode);
            return File(b, "image/jpg");
        }

        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <returns></returns>
        public bool VerifyCheckCode()
        {
            string verifyCheckCode = Request["checkCode"];
            string checkCode = Session["checkCode"].ToString();

            return string.Equals(verifyCheckCode, checkCode, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 注册数据
        /// </summary>
        /// <returns></returns>
        public ActionResult RegData()
        {
            IAccountService accountService = UnityIOC.Resolve<IAccountService>();//创建AccountService BLL层接口

            Account account = new Account { RegTime = DateTime.Now, NickName = Guid.NewGuid().ToString() };//Model

            string type = Request["type"];
            //使用手机号方式注册
            if (string.Equals(type, "phone"))
            {
                string phone = Request["phone"];
                string pwd = Request["pwd"];

                account.UserPhone = phone;
                account.PassWord = MD5Helper.MD5Encrypt64(pwd);

                //查询需要注册的手机号是否已经注册过
                if (accountService.QueryEnity(u => u.UserPhone == account.UserPhone))
                {
                    return Content("exist");
                }
                //添加数据，注册
                accountService.AddEnity(account);
                if (accountService.dbSession.SaveChanges() > 0)
                {
                    return Content("success");
                }
                
            }
            //使用邮件方式注册
            //TODO
            if (string.Equals(type, "mail"))
            {
                string mail = Request["mail"];
                string pwd = Request["pwd"];
            }

            return Content("eeror");
        }

        public ActionResult Test()
        {
            //IAccountService account = UnityIOC.Resolve<IAccountService>();
            //ViewBag.account = account.Test();
            return View();
        }
    }
}