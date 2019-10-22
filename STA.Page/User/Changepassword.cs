using System;
using System.Collections.Generic;
using System.Text;

using STA.Data;
using STA.Common;
using STA.Config;
using STA.Core;
using STA.Entity;

namespace STA.Page.User
{
    public class Changepassword : UserBase
    {
        protected override void PageShow()
        {
            if (!IsLogin()) return;

            if (ispost)
            {
                UserInfo uinfo = Users.CheckUserLogin(user.Username, Utils.MD5(STARequest.GetFormString("oldpwd")), 0);
                if (uinfo == null)
                {
                    AddErrLine("旧密码输入有误,请重试");
                    return;
                }
                else if (STARequest.GetFormString("newpwd").Length < 6)
                {
                    AddErrLine("密码长度必须大于6位");
                    return;
                }
                else if (Utils.MD5(STARequest.GetFormString("oldpwd")) == Utils.MD5(STARequest.GetFormString("newpwd")))
                {
                    AddErrLine("新密码不能和旧密码一样");
                    return;
                }
                else
                {
                    if (config.Thirducenter == 1)
                    {
                        UserfieldInfo ufinfo = Users.GetUserField(uinfo.Id);
                    }
                    uinfo.Password = Utils.MD5(STARequest.GetFormString("newpwd"));
                    Users.EditUser(uinfo);
                }
            }
        }
    }
}
