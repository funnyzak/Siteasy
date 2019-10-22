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
    public class Changeemail : UserBase
    {
        public string pwd = STARequest.GetString("pwd");
        public string email = STARequest.GetString("newemail");
        protected override void PageShow()
        {
            if (!IsLogin()) return;

            if (ispost)
            {
                UserInfo uinfo = Users.CheckUserLogin(user.Username, Utils.MD5(pwd), 0);
                if (uinfo == null)
                {
                    AddErrLine("密码输入有误,请重试");
                    return;
                }
                else if (user.Email == email)
                {
                    AddErrLine("请设置一个新的邮箱");
                    return;
                }
                else if (config.Emailmultuser == 0 && Users.CheckUserEmailExist(email) > 0)
                {
                    AddErrLine("该邮箱已经被别人使用");
                    return;
                }
                else
                {
                    if (config.Thirducenter == 1)
                    {
                        UserfieldInfo ufinfo = Users.GetUserField(uinfo.Id);
                    }
                    uinfo.Email = email;
                    Users.EditUser(uinfo);
                }
            }
        }
    }
}
