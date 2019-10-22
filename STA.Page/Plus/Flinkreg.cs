using System;
using System.Collections.Generic;
using System.Text;

using STA.Data;
using STA.Common;
using STA.Config;
using STA.Entity;
using STA.Entity.Plus;
using System.Web;

namespace STA.Page.Plus
{
    public class Flinkreg : PageBase
    {

        private string cookiename = "staflinkreg";

        protected override void PageShow()
        {
            if (ispost && !STA.Core.ConUtils.IsCrossSitePost())
            {
                if (STARequest.GetString("action") != "ajax" && config.Vcodemods.IndexOf("4") >= 0 && (STARequest.GetString("vcode").ToLower() == "" || STARequest.GetString("vcode").ToLower() != Utils.GetCookie(cookiename).ToLower()))
                {
                    PageInfo("验证码输入有误!", "back");
                    return;
                }

                LinkInfo info = new LinkInfo();
                info.Name = Utils.HtmlEncode(STARequest.GetFormString("name").Trim());
                info.Typeid = STARequest.GetFormInt("typeid", 0);
                info.Url = Utils.HtmlEncode(STARequest.GetFormString("url"));
                info.Logo = Utils.HtmlEncode(STARequest.GetFormString("logo"));
                info.Status = 0;
                info.Email = Utils.HtmlEncode(STARequest.GetFormString("email"));
                info.Description = Utils.HtmlEncode(STARequest.GetString("description"));

                bool success = false;
                if (info.Name != "" && info.Url != "")
                    success = Contents.AddLink(info) > 0;

                if (STARequest.GetString("action") == "ajax")
                {
                    HttpContext.Current.Response.Write(success.ToString());
                }
                else
                {
                    if (success) PageInfo("您提交的链接我们已收到,我们会尽快审核回复您！", STARequest.GetString("returnurl"));
                    else PageInfo("信息填写不完整,提交失败!", "back");
                }
            }
        }


    }

}
