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
    public class Useractivation : PageBase
    {
        public string code = STARequest.GetString("code");
        protected override void PageShow()
        {
            UserauthInfo uainfo = Users.GetUserauth(code, AuthType.用户激活);
            if (uainfo == null)
            {
                PageInfo("激活链接已经失效！");
            }
            else
            {
                UserInfo uinfo = Users.GetUser(uainfo.Userid);
                if (uinfo == null)
                {
                    PageInfo("激活用户不存在！");
                }
                uinfo.Ischeck = 1;
                uinfo = UserUtils.InitUserGroup(uinfo);
                Users.EditUser(uinfo);
                ConUtils.WriteUserCookie(uinfo, 999999);
                Users.DelUserauth(uainfo.Id);
                PageInfo("激活成功,现在转向用户首页", "user/");
            }
        }
    }
}
