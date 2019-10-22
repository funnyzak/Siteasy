using System;
using System.Web;
using System.Collections.Generic;
using System.Text;

using STA.Entity;
using STA.Common;
using STA.Core;
using STA.Data;
using STA.Config;

namespace STA.Page
{
    public class OAuthLogin : PageBase
    {
        protected override void PageShow()
        {
            if (userid > 0)
            {
                HttpContext.Current.Response.Redirect(siteurl + "/user/");
                return;
            }

            if (HttpContext.Current.Session["oauth_name"] == null || HttpContext.Current.Session["oauth_access_token"] == null || HttpContext.Current.Session["oauth_openid"] == null)
            {
                PageInfo("登录失败，用户授权已过期，请重新登录！", siteurl + "/login.aspx");
                return;
            }

            UserconnectInfo connectinfo = Connects.GetUserconnect(HttpContext.Current.Session["oauth_openid"].ToString(), HttpContext.Current.Session["oauth_name"].ToString());

            if (connectinfo != null)
            {
                //检查用户是否存在
                UserInfo uinfo = Users.GetUser(connectinfo.Uid);

                if (uinfo == null)
                {
                    PageInfo("登录失败，授权用户不存在或已被删除！", siteurl + "/login.aspx");
                    return;
                }

                Globals.UpdateLoginStatus(uinfo);
                ConUtils.WriteUserCookie(uinfo, 999999);

                //更新最新的Access Token
                connectinfo.Token = HttpContext.Current.Session["oauth_access_token"].ToString();
                Connects.EditUserconnect(connectinfo);

                HttpContext.Current.Response.Redirect(sitedir + "/user/");

                return;
            }
        }
    }
}
