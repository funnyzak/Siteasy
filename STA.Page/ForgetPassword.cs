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
    public class ForgetPassword : PageBase
    {
        protected override void PageShow()
        {
            if (ConUtils.GetOnlineUser().Userid > 0)
                PageInfo("您当前已经登录！");

            if (ispost)
            {
                string username = STARequest.GetString("username");
                string email = STARequest.GetString("email");
                if (ConUtils.IsCrossSitePost())
                {
                    PageInfo("您的请求来路不正确，无法提交！", 7);
                    return;
                }
                if (STARequest.GetString("vcode").ToLower() != Utils.GetCookie("forgetpassword").ToLower())
                {
                    AddErrLine("验证码输入有误！");
                    return;
                }
                if (username == "" || email == "")
                {
                    AddErrLine("用户名或邮件地址不能为空！");
                    return;
                }

                UserInfo uinfo = Users.GetUser(username);
                if (uinfo == null)
                {
                    AddErrLine("该用户名不存在！");
                    return;
                }
                if (uinfo.Email.Trim() != email)
                {
                    AddErrLine("用户名与邮箱不匹配！");
                    return;
                }

                UserauthInfo uactinfo = new UserauthInfo();
                uactinfo.Userid = uinfo.Id;
                uactinfo.Username = uinfo.Username;
                uactinfo.Email = uinfo.Email;
                uactinfo.Atype = AuthType.重置密码;
                uactinfo.Code = ConUtils.CreateAuthStr(30);
                Users.AddUserauth(uactinfo);
                UserUtils.RetSetPwdMail(username, email, STARequest.GetInt("question", -1), STARequest.GetString("answer"), uactinfo.Code, config);
                PageInfo("密码重置链接已经通过 Email 发送到您的信箱中,<br />请在 3 天之内修改您的密码！", siteurl, 5);
            }
        }
    }
}
