using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Web.UI;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class login : System.Web.UI.Page
    {
        public string script = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                UserLogin();
            else
                UserAction();
        }

        private void UserLogin()
        {
            string username = STARequest.GetFormString("username");
            string password = Utils.MD5(STARequest.GetFormString("password"));
            string vcode = STARequest.GetFormString("vcode").ToLower(); ;
            if (vcode != Utils.GetCookie("syslogin"))
            {
                script = string.Format("<script type=\"text/javascript\">{0}</script>", "ErrorTips(\"输入的验证码不正确！\");");
                return;
            }
            UserInfo userinfo = Users.CheckUserLogin(username, password, 1);
            if (userinfo != null && userinfo.Locked != 1)
            {
                Utils.ClearCookie("syslogin");
                userinfo.Safecode = Rand.Str(32);
                Globals.UpdateLoginStatus(userinfo);
                ConUtils.WriteUserCookie(userinfo, 999999);

                HttpCookie cookie = new HttpCookie("staadmin");
                cookie.Expires = DateTime.Now.AddMinutes(60);  //为了测试设置超长过期时间
                cookie.Values["userid"] = userinfo.Id.ToString();
                HttpContext.Current.Response.AppendCookie(cookie);

                CheckUserSet(userinfo.Id);
                ConUtils.InsertLog(1, userinfo.Id, userinfo.Username, userinfo.Adminid, userinfo.Admingroupname, STARequest.GetIP(), "系统登录", "");
                if (STARequest.GetQueryString("returnurl") != string.Empty)
                {
                    Response.Write("<script type=\"text/javascript\">top.location.href='" + Utils.UrlDecode(STARequest.GetQueryString("returnurl")) + "';</script>");
                    Response.End();
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\">top.location.href='" + BaseConfigs.GetSitePath + BaseConfigs.GetAdminPath + "';</script>");
                    Response.End();
                }
            }
            else
            {
                script = string.Format("<script type=\"text/javascript\">{0}</script>", "ErrorTips(\"用户名或密码错误或被锁定！\");");
            }
        }

        private void CheckUserSet(int userid)
        {
            string usersetxmlpath = ConUtils.UserLikeXmlPath(userid, BaseConfigs.GetBaseConfig());
            if (!File.Exists(usersetxmlpath))
            {
                FileUtil.WriteFile(usersetxmlpath, FileUtil.ReadFile(usersetxmlpath.Replace("_" + userid.ToString() + ".config", ".config")), System.Text.Encoding.UTF8);
            }
        }

        private void UserAction()
        {
            if (STARequest.GetString("action") == "loginout")
            {
                ConUtils.ClearUserCookie();
                HttpCookie cookie = new HttpCookie("staadmin");
                HttpContext.Current.Response.AppendCookie(cookie);
                return;
            }

            OnlineUserInfo oluserinfo = ConUtils.GetOnlineUser();
            if (oluserinfo.Userid > 0 && oluserinfo.Username != "")
            {
                username.Text = oluserinfo.Username;
                //username.AddAttributes("readonly", "true");
            }

            if (oluserinfo.Userid > 0 && oluserinfo.Adminid > 0 && Context.Request.Cookies["staadmin"] != null && Context.Request.Cookies["staadmin"]["userid"] != null)
            {

                //Response.Write("_" + Context.Request.Cookies["staadmin"]["userid"] == null + "_");
                Response.Redirect("index.aspx");
                return;
            }
        }
    }
}