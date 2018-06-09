using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using SpaceHome.BLLContainer;
using SpaseHome.IBLL;
using SpaseHome.Model;
using SpaseHome.Common;
using SpaseHome.Web.Models;

namespace SpaseHome.Web.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login

        private IAccountService accountService = UnityIOC.Resolve<IAccountService>();//创建AccountService BLL层接口

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            #region 填充cookies
            if (Request.Cookies["user"] != null)
            {
                ViewBag.user = Request.Cookies["user"].Value;
            }

            if (Request.Cookies["pwd"] != null)
            {
                ViewBag.pwd = Request.Cookies["pwd"].Value;
            }
            #endregion 
            return View();
        }

        /// <summary>
        /// 登录数据获取及登录
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginData()
        {
            string user = Request["userData"];
            string pwd = Request["pwdData"];
            string isRem = Request["isRem"];

            //查询用户名
            //使用手机号登录
            if(Regex.IsMatch(user, @"^[1]+[3,4,5,7,8]+\d{9}")&&user.Length==11)
            {
                var accounts = accountService.LoadEnities(u => u.UserPhone == user);//查找账号
                if (accounts.FirstOrDefault() != null)
                {
                    var account = accounts.FirstOrDefault();
                    if (account.PassWord == MD5Helper.MD5Encrypt64(pwd))
                    {
                        SetCookies(isRem, 15, new CookiesDictionary<string, string> { Key=nameof(user),Value=user},new CookiesDictionary<string, string> { Key=nameof(pwd),Value=pwd});
                        return Content("success");//登录成功
                    }
                    else
                    {
                        return Content("errorPwd");//密码错误
                    }
                }
                else
                {
                    return Content("errorAccount");//账号不存在
                }

                
            }
            //使用邮箱登录
            else if(Regex.IsMatch(user, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
            {
                //TODO
                return Content("邮箱！");
            }
            //使用用户名登录
            else
            {
                var accounts = accountService.LoadEnities(u => u.UserName == user);
                if (accounts.FirstOrDefault() != null)
                {
                    Account account = accounts.FirstOrDefault();
                    if (string.Equals(MD5Helper.MD5Encrypt64(pwd), account.PassWord))
                    {
                        return Content("success");//登陆成功
                    }
                    else
                    {
                        return Content("errorPwd");//密码错误
                    }
                }
                else
                {
                    return Content("errorAccount");//账号不存在
                }
            }
        }

        /// <summary>
        /// 设置cookies
        /// </summary>
        /// <param name="isRem">是储存cookies</param>
        /// <param name="time">cookies过期时间</param>
        /// <param name="cookies">cookies的key和value</param>
        private void SetCookies(string isRem, double time, params CookiesDictionary<string,string>[] cookies)
        {            
            if (string.Equals(isRem, "true", StringComparison.CurrentCultureIgnoreCase))
            {
                for (int i = 0; i < cookies.Length; i++)
                {
                    Response.Cookies[cookies[i].Key].Value = cookies[i].Value;
                    Response.Cookies[cookies[i].Key].Expires = DateTime.Now.AddDays(time);
                }               
            }
            else
            {
                for (int i = 0; i < cookies.Length; i++)
                {
                    if (Request.Cookies[cookies[i].Key] != null)
                    {
                        Response.Cookies[cookies[i].Key].Expires = DateTime.Now.AddDays(-1);
                    }
                }                
            }
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
        /// 注册数据
        /// </summary>
        /// <returns></returns>
        public ActionResult RegData()
        {
            
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

        

        public ActionResult Test()
        {
            //IAccountService account = UnityIOC.Resolve<IAccountService>();
            //ViewBag.account = account.Test();
            return View();
        }
    }
}