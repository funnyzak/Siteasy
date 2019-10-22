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
    public class Register : PageBase
    {
        public UserInfo info = new UserInfo();
        protected override void PageShow()
        {
            if (ConUtils.GetOnlineUser().Userid > 0)
                Redirect(sitedir + "/"); //PageInfo("您当前已经登录！"); ;
            if (config.Openreg != 1)
                PageInfo("网站当前未开放新用户注册！", "login.aspx");

            if (ispost && !ConUtils.IsCrossSitePost())
            {
                info.Username = STARequest.GetFormString("username");
                info.Nickname = STARequest.GetFormString("nickname");
                info.Password = STARequest.GetFormString("password").Trim();
                info.Email = STARequest.GetFormString("email");

                if (config.Vcodemods.IndexOf("1") >= 0 && STARequest.GetString("vcode").ToLower() != Utils.GetCookie("register").ToLower())
                {
                    AddErrLine("验证码输入有误!");
                    return;
                }
                if (info.Email == "")
                {
                    AddErrLine("邮件地址不能为空!");
                    return;
                }
                if (info.Password.Length < 6)
                {
                    AddErrLine("密码必须大于等于6位!");
                    return;
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(info.Username, @"^[a-z0-9]{6,20}$"))
                {
                    AddErrLine("用户名有误,须6-20位小写字母、数字组成!");
                    return;
                }
                if (config.Forbiduserwords.IndexOf(info.Username) >= 0 || Users.CheckUserNameExist(info.Username) > 0)
                {
                    AddErrLine("用户名已经存在,请使用其他用户名!");
                    return;
                }
                if (config.Emailmultuser == 0 && Users.CheckUserEmailExist(info.Email) > 0)
                {
                    AddErrLine("邮箱已经存在,请使用其他邮箱注册!");
                    return;
                }
                info.Password = Utils.MD5(info.Password);

                if (config.Userverifyway == 0)
                {
                    info = UserUtils.InitUserGroup(info);
                    info.Ischeck = 1;
                }

                info.Regip = STARequest.GetIP();
                info.Id = Users.AddUser(info);
                if (info.Id > 0)
                {

                    UserfieldInfo ufinfo = new UserfieldInfo();
 
                    ufinfo.Uid = info.Id;
                    Users.AddUserField(ufinfo);

                    if (config.Userverifyway == 2)
                    {
                        UserauthInfo uactinfo = new UserauthInfo();
                        uactinfo.Userid = info.Id;
                        uactinfo.Username = info.Username;
                        uactinfo.Email = info.Email;
                        uactinfo.Code = ConUtils.CreateAuthStr(30);
                        Users.AddUserauth(uactinfo);

                        Emails.STASmtpRegisterMail(info.Username, info.Email, info.Password, uactinfo.Code);
                    }

                    if (info.Ischeck == 1)
                        PageInfo("注册成功，返回登录页", baseconfig.Sitepath + "/login.aspx");
                    else if (config.Userverifyway == 1)
                        PageInfo("注册成功, 但需要系统管理员审核您的帐户后才可登录使用", baseconfig.Sitepath + "/");
                    else
                        PageInfo("注册成功, 请您到您的邮箱中点击激活链接来激活您的帐号", baseconfig.Sitepath + "/");
                }
                else
                {
                    PageInfo("注册失败，请稍后重试！", "");
                }
            }
        }
    }
}
