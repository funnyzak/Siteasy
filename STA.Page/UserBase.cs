using System;
using System.Collections.Generic;
using System.Text;

using STA.Entity;
using STA.Common;
using STA.Core;
using STA.Data;
namespace STA.Page
{
    public class UserBase : PageBase
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo user;

        public UserBase()
        {
            seotitle = string.Format("用户管理中心 - {0}", config.Webname);
        }

        protected internal bool IsLogin()
        {
            string loginurl = sitedir + "/login.aspx?returnurl=" + Utils.UrlEncode(cururl);
            if (userid <= 0)
            {
                Redirect(loginurl);
                return false;
            }

            user = Users.GetUser(userid);
            string temperrtxt = CheckUserStatus();

            if (temperrtxt != "")
            {
                ConUtils.ClearUserCookie();
                PageInfo(temperrtxt, loginurl);
                return false;
            }
            return true;
        }

        string CheckUserStatus()
        {
            if (user == null)
            {
                return "用户信息有误,请重新登录";
            }

            //if (oluser.Safecode != user.Safecode)
            //{
            //    return "您的账号已在别处登录";
            //}

            if (user.Ischeck == 0 && config.Userverifyway != 0)
            {
                if (config.Userverifyway == 1)
                {
                    return "您的帐号还没有通过管理员审核";
                }
                else if (config.Userverifyway == 2)
                {
                    return "您的帐号还没有激活,请登录您的激活邮箱(" + user.Email + ")进行帐号激活";
                }
            }

            if (user.Locked == 1)
            {
                return "您的账号已经被锁定,要解锁请联系管理员:" + config.Adminmail;
            }
            return "";
        }
    }
}
