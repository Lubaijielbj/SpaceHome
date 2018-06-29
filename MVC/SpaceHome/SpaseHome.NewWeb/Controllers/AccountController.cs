using SpaceHome.BLLContainer;
using SpaseHome.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpaseHome.Model;
using SpaseHome.NewWeb.Models;
using System.Web.Script.Serialization;
using SpaseHome.Common;
using System.Text.RegularExpressions;

namespace SpaseHome.NewWeb.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService accountService = UnityIOC.Resolve<IAccountService>();
        private IUserInfoService userInfoService = UnityIOC.Resolve<IUserInfoService>();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login()
        {
            string loginType = Request["logintype"];
            string userName = Request["username"];
            string pwd = Request["pwd"];
            bool isRememberPwd = bool.Parse(Request["isremember"]);

            ReturnState returnState = new ReturnState();

            if (loginType == "0")
            {
                var accountList = accountService.LoadEnities(a => a.PhoneNum == userName);
                Account loginAccount = accountList.FirstOrDefault();
                if (loginAccount == null)
                {
                    returnState.stateType = StateType.error;
                    returnState.message = "账号或者密码错误";
                }
                else
                {
                    if (loginAccount.PassWord != pwd)
                    {
                        returnState.stateType = StateType.error;
                        returnState.message = "账号或者密码错误";
                    }
                    else
                    {
                        returnState.stateType = StateType.success;

                        Session["account"] = loginAccount;

                        SetCookie(isRememberPwd, 15,
                            new CookiesDictionary<string, string>[] {
                                new CookiesDictionary<string, string> { Key = "username", Value = userName },
                                new CookiesDictionary<string, string> { Key = "pwd", Value = pwd }
                            });
                    }
                }
            }else if (loginType == "1")
            {
                var accountList = accountService.LoadEnities(a => a.UserName == userName);
                Account loginAccount = accountList.FirstOrDefault();
                if (loginAccount == null)
                {
                    returnState.stateType = StateType.error;
                    returnState.message = "账号或者密码错误";
                }
                else
                {
                    if (loginAccount.PassWord != pwd)
                    {
                        returnState.stateType = StateType.error;
                        returnState.message = "账号或者密码错误";
                    }
                    else
                    {
                        returnState.stateType = StateType.success;

                        Session["account"] = loginAccount;

                        SetCookie(isRememberPwd, 15, 
                            new CookiesDictionary<string, string>[] {
                                new CookiesDictionary<string, string> { Key = "username", Value = userName },
                                new CookiesDictionary<string, string> { Key = "pwd", Value = pwd }
                            });
                    }
                }
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Content(jss.Serialize(returnState));
        }

        /// <summary>
        /// 设置记住密码cookie
        /// </summary>
        /// <param name="isRememberPwd">是否记住密码</param>
        /// <param name="time">记住密码的时间（天）</param>
        /// <param name="cookies">cookie字典数组（cookie的key和value）</param>
        private void SetCookie(bool isRememberPwd,int time,params CookiesDictionary<string,string>[] cookies)
        {
            if (isRememberPwd)
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
                    if (Request.Cookies[cookies[i].Key].Value != null)
                    {
                        Response.Cookies[cookies[i].Key].Expires = DateTime.Now.AddDays(-1);
                    }
                }
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(Account account)
        {
            ReturnState returnState = new ReturnState();

            if (account.UserName == string.Empty || account.UserName.Trim() == string.Empty)
            {
                returnState.stateType = StateType.error;
                returnState.message = "用户名不合法！";
            }
            else if(!Regex.IsMatch(account.PhoneNum, @"^1[3|4|5|7|8]\d{9}"))
            {
                returnState.stateType = StateType.error;
                returnState.message = "手机号格式错误！";
            }
            else
            {
                account.AccountId = Guid.NewGuid();
                account.DelFlag = 0;
                account.RegTime = DateTime.Now;
                account.LastLoginTime = DateTime.Now;
                account.LoginErrorTimes = 0;
               
                accountService.AddEnity(account);
                if (accountService.dbSession.SaveChanges() <= 0)
                {
                    returnState.stateType = StateType.error;
                    returnState.message = "注册失败，请重试";
                }
                else
                {
                    var accountList = accountService.LoadEnities(a => a.AccountId == account.AccountId);
                    if (accountList.FirstOrDefault() == null)
                    {
                        returnState.stateType = StateType.error;
                        returnState.message = "注册失败，请重试";
                    }
                    else
                    {
                        returnState.stateType = StateType.success;
                    }
                }
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Content(jss.Serialize(returnState));
        }

        /// <summary>
        /// 验证用户名
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VerifyUserName(string username)
        {
            ReturnState returnState = new ReturnState();

            if (username == string.Empty || username.Trim() == string.Empty)
            {
                returnState.stateType = StateType.error;
                returnState.message = "用户名不合法";
            }
            else
            {
                var accountList = accountService.LoadEnities(a => a.UserName == username);
                if (accountList.FirstOrDefault() != null)
                {
                    returnState.stateType = StateType.error;
                    returnState.message = "用户名已存在";
                }
                else
                {
                    returnState.stateType = StateType.success;
                }
            }          

            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Content(jss.Serialize(returnState));
        }

        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="userphone">手机号</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VerifyUserPhone(string userphone)
        {
            ReturnState returnState = new ReturnState();

            if (userphone == null || userphone.Trim() == null)
            {
                returnState.stateType = StateType.error;
            }
            else if (!Regex.IsMatch(userphone, @"^1[3|4|5|7|8]\d{9}"))
            {
                returnState.stateType = StateType.error;
                returnState.message = "手机号格式不正确!";
            }
            else
            {
                var accountList = accountService.LoadEnities(a => a.PhoneNum == userphone);
                if (accountList.FirstOrDefault() != null)
                {
                    returnState.stateType = StateType.error;
                    returnState.message = "改手机号已注册！";
                }
                else
                {
                    returnState.stateType = StateType.success;
                }
            }
            

            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Content(jss.Serialize(returnState));
        }
    }
}

public struct CookiesDictionary<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
}