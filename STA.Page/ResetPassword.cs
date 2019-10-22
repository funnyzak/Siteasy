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
    public class ResetPassword : PageBase
    {
        public string code = STARequest.GetString("code");
        protected override void PageShow()
        {
            if (ConUtils.GetOnlineUser().Userid > 0)
                PageInfo("您当前已经登录！");

            if (code == "")
            {
                PageInfo("重置链接非法,现转入首页", weburl);
            }
            UserauthInfo uainfo = Users.GetUserauth(code, AuthType.重置密码);
            if (uainfo == null)
            {
                PageInfo("当前重置链接已失效！", weburl);
            }
            if (uainfo.Addtime.AddMinutes(uainfo.Expirs) < DateTime.Now)
            {
                PageInfo("重置链接已经失效,请重复发送填写重置信息!", "forgetpassword.aspx");
            }
            if (ispost)
            {
                string password = STARequest.GetString("password");
                string repassword = STARequest.GetString("repassword");


                if (ConUtils.IsCrossSitePost())
                {
                    PageInfo("您的请求来路不正确，无法提交", weburl);
                    return;
                }
                if (STARequest.GetString("vcode").ToLower() != Utils.GetCookie("resetpassword").ToLower())
                {
                    AddErrLine("验证码输入有误！");
                    return;
                }
                if (password != repassword)
                {
                    AddErrLine("两次输入的密码不一致！");
                    return;
                }
                UserInfo uinfo = Users.GetUser(uainfo.Username);
                if (uinfo == null)
                {
                    PageInfo("用户不存在,您无法重置密码！", weburl);
                }
                Users.DelUserauth(uainfo.Id);
                uinfo.Password = Utils.MD5(password);
                Users.EditUser(uinfo);
                ConUtils.WriteUserCookie(uinfo, 999999);
                PageInfo("密码重置成功,现在转入用户首页", "user/");
            }
        }
    }
}
