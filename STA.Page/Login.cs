using System;
using System.Collections.Generic;
using System.Text;

using STA.Entity;
using STA.Common;
using STA.Core;
using STA.Data;
using STA.Config;

namespace STA.Page
{
    public class Login : PageBase
    {
        public string returnurl = Utils.UrlDecode(STARequest.GetString("returnurl"));
        protected override void PageShow()
        {
            if (ispost)
            {
                username = STARequest.GetString("username");
                password = STARequest.GetString("password");

                if (config.Vcodemods.IndexOf("2") >= 0 && (STARequest.GetString("vcode").ToLower() == "" || STARequest.GetString("vcode").ToLower() != Utils.GetCookie("userlogin").ToLower()))
                {
                    AddErrLine("验证码输入有误");
                    return;
                }

                if (username == "" || password == "")
                {
                    AddErrLine("登录信息填写不完整");
                    return;
                }

                msgtext = UserUtils.UserLogin(config, username, password, STARequest.GetInt("expires", -1));
                if (msgtext != "")
                {
                    errors++;
                }
            }
        }
    }
}
