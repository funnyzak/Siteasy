using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Data;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Cache;
using System.Collections.Generic;

namespace STA.Core
{
    public class UserUtils
    {
        private static object SynObject = new object();

        public static bool CheckUserByCache(int uid, string password, UserInfo userinfo, int admin)
        {
            if (userinfo == null) return false;
            if (!(userinfo.Id == uid && userinfo.Password == password)) return false;
            if (admin > 0)
            {
                if (userinfo.Adminid > 0) return true;
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string UserLogin(GeneralConfigInfo config, string username, string password, int expires)
        {

            UserInfo uinfo = Users.CheckUserLogin(username, Utils.MD5(password), 0);
            if (uinfo == null)
            {
                return "您输入的账户名和密码不匹配，请重新输入";
            }

            if (uinfo.Ischeck == 0)
            {
                if (GeneralConfigs.GetConfig().Userverifyway == 1)
                {
                    return "您的帐号还没有通过管理员审核";
                }
                else if (GeneralConfigs.GetConfig().Userverifyway == 2)
                {
                    return "您的帐号还没有激活,请登录您的激活邮箱进行帐号激活";
                }
            }

            if (uinfo.Locked == 1)
            {
                return "您的账号已经被锁定,要解锁请联系管理员:" + GeneralConfigs.GetConfig().Adminmail;
            }

            //uinfo.Safecode = Rand.Str(32);
            Globals.UpdateLoginStatus(uinfo);
            ConUtils.WriteUserCookie(uinfo, expires);
            return "";
        }

        public static UserInfo InitUserGroup(UserInfo uinfo)
        {
            UserGroupInfo uginfo = Users.GetUserGroupZeroScoreId();
            if (uginfo != null)
            {
                uinfo.Groupid = uginfo.Id;
                uinfo.Groupname = uginfo.Name;
            }
            return uinfo;
        }


        /// <summary>
        /// 用户在线信息维护。判断当前用户的身份(会员还是游客),是否在在线列表中存在,如果存在则更新会员的当前动,不存在则建立.
        /// </summary>
        public static OnlineUserInfo UpdateOnlineUserInfo(OnlineUserInfo olinfo, int expires)
        {
            lock (SynObject)
            {
                if (olinfo == null || olinfo.Id < 0)
                {
                    olinfo = CreateGuestUser();
                }

                HttpCookie cookie = new HttpCookie("sta");
                cookie.Values["userid"] = olinfo.Userid.ToString();
                cookie.Values["nickname"] = Utils.UrlEncode(olinfo.Nickname).Trim();
                cookie.Values["username"] = Utils.UrlEncode(olinfo.Username.ToString()).Trim();
                cookie.Values["safecode"] = Utils.UrlEncode(olinfo.Safecode).Trim();
                //cookie.Values["password"] = Utils.UrlEncode(olinfo.Password.ToString()).Trim();
                cookie.Values["adminid"] = olinfo.Adminid.ToString();
                cookie.Values["groupid"] = olinfo.Groupid.ToString();
                cookie.Values["admingroupname"] = Utils.UrlEncode(olinfo.Admingroupname).Trim();
                cookie.Values["groupname"] = Utils.UrlEncode(olinfo.Groupname).Trim();
                cookie.Values["lastsearchtime"] = Utils.UrlEncode(olinfo.Lastsearchtime);
                cookie.Values["expires"] = expires.ToString();
                cookie.Values["ip"] = olinfo.Ip;

                if (expires > 0)
                {
                    cookie.Expires = DateTime.Now.AddMinutes(expires);
                }
                string cookieDomain = GeneralConfigs.GetConfig().Domaincookie.Trim();
                if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain.TrimStart('.')) > -1 && ConUtils.IsValidDomain(HttpContext.Current.Request.Url.Host))
                {
                    cookie.Domain = cookieDomain;
                }
                HttpContext.Current.Response.AppendCookie(cookie);

                return olinfo;
            }
        }


        /// <summary>
        /// Cookie中没有用户ID或则存的的用户ID无效时在在线表中增加一个游客.
        /// </summary>
        public static OnlineUserInfo CreateGuestUser()
        {
            OnlineUserInfo onlineuserinfo = new OnlineUserInfo();
            onlineuserinfo.Userid = 0;
            onlineuserinfo.Username = "网友";
            onlineuserinfo.Nickname = "网友";
            onlineuserinfo.Password = "";
            onlineuserinfo.Groupid = 0;
            onlineuserinfo.Ip = STARequest.GetIP();
            onlineuserinfo.Lastsearchtime = "1900-1-1 00:00:00";
            return onlineuserinfo;
        }


        public static bool RetSetPwdMail(string username, string email, int questionid, string answer, string authstr, GeneralConfigInfo config)
        {
            string weburl = "http://" + STARequest.GetCurrentFullHost() + BaseConfigs.GetSitePath.ToLower();
            StringBuilder body = new StringBuilder(username);
            body.AppendFormat("您好!<br />这封信是由 {0}", config.Webname);
            body.Append(" 发送的.<br /><br />您收到这封邮件,是因为在我们的网站上这个邮箱地址被登记为用户邮箱,且该用户请求使用 Email 密码重置功能所致.");
            body.Append("<br /><br />----------------------------------------------------------------------");
            body.Append("<br />重要！");
            body.Append("<br /><br />----------------------------------------------------------------------");
            body.Append("<br /><br />如果您没有提交密码重置的请求,请立即忽略并删除这封邮件.只在您确认需要重置密码的情况下,才继续阅读下面的内容.");
            body.Append("<br /><br />----------------------------------------------------------------------");
            body.Append("<br />密码重置说明");
            body.Append("<br /><br />----------------------------------------------------------------------");
            body.Append("<br /><br />您只需在提交请求后的三天之内,通过点击下面的链接重置您的密码:<br /><br />");
            body.AppendFormat("<a href={0}/resetpassword.aspx?code={1} target=_blank>{0}", weburl, authstr);
            body.AppendFormat("/resetpassword.aspx?code={0}</a>", authstr);
            body.Append("<br /><br />(如果上面不是链接形式,请将地址手工粘贴到浏览器地址栏再访问)");
            body.AppendFormat("<br /><br />本请求提交者的 IP 为 {0}<br /><br /><br /><br />", STARequest.GetIP());
            body.AppendFormat("<br />此致 <br /><br />{0} 管理团队.<br />{1}<br /><br />", config.Webtitle, weburl);

            Emails.STASmtpMailToUser(email, config.Webtitle + " 取回密码说明", body.ToString());
            return true;
        }

        /// <summary>
        /// 获取指定用户组的用户并向其发送短信息
        /// </summary>
        /// <param name="groupidlist">用户组</param>
        /// <param name="topNumber">获取前N条记录</param>
        /// <param name="msgfrom">谁发的短消息</param>
        /// <param name="msguid">发短消息人的UID</param>
        /// <param name="folder">短消息文件夹</param>
        /// <param name="subject">主题</param>
        /// <param name="postdatetime">发送时间</param>
        /// <param name="message">短消息内容</param>
        /// <returns></returns>
        public static int SendPMByGroupidList(string groupidlist, int topnumber, ref int start_uid, string msgfrom, int msguid, int folder, string subject, string postdatetime, string message)
        {
            DataTable dt = Data.Users.GetUserListByGroupid(topnumber, "id,username", start_uid, groupidlist);
            foreach (DataRow dr in dt.Rows)
            {
                PrivateMessageInfo pm = new PrivateMessageInfo();
                pm.Msgfrom = msgfrom.Replace("'", "''");
                pm.Msgfromid = msguid;
                pm.Msgto = dr["username"].ToString().Replace("'", "''");
                pm.Msgtoid = TypeParse.StrToInt(dr["id"]);
                pm.Folder = (Folder)folder;
                pm.Subject = subject;
                pm.Addtime = TypeParse.StrToDateTime(postdatetime);
                pm.Content = message;
                pm.New = 1;//标记为未读
                PrivateMessages.CreatePrivateMessage(pm, 0);

                start_uid = pm.Msgtoid;
            }
            return dt.Rows.Count;
        }

        /// <summary>
        /// 获取指定用户组的用户并向其发送邮件
        /// </summary>
        /// <param name="groupidlist">用户组</param>
        /// <param name="topnumber">获取前N条记录</param>
        /// <param name="start_uid">大于该uid的用户记录</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <returns></returns>
        public static int SendEmailByGroupidList(string groupidlist, int topnumber, ref int start_uid, string subject, string body)
        {
            DataTable dt = Data.Users.GetUserListByGroupid(topnumber, "id,username,email", start_uid, groupidlist);
            foreach (DataRow dr in dt.Rows)
            {
                if (!string.IsNullOrEmpty(dr["Email"].ToString().Trim()))
                {
                    EmailMultiThread emt = new EmailMultiThread(dr["UserName"].ToString().Trim(), dr["Email"].ToString().Trim(), subject, body);
                    new System.Threading.Thread(new System.Threading.ThreadStart(emt.Send)).Start();
                }
                start_uid = TypeParse.ObjectToInt(dr["id"]);
            }
            return dt.Rows.Count;
        }
    }
}
