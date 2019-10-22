using System;

using STA.Common;
using STA.Config;
using STA.Data;
using STA.Core;

namespace STA.Web.Admin.Frame
{
    public partial class main : AdminPage
    {
        public string lastlogintime = "";
        public int contentcount = 0;
        public int channelcount = 0;
        public int speccount = 0;
        public int commentcount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (STARequest.GetBrowser().IndexOf("ie6") >= 0 && ConUtils.GetCookie("brower_ie6") == "")
            {
                Message("您当前使用的是IE6，为了更好的操作体验，请升级IE浏览器，您的系统可以升级到IE8！ <a href='http://windows.microsoft.com/zh-"
                                        + "CN/internet-explorer/downloads/ie-8' target='_blank' style='color:#ff0000;'><b>点击升级</b></a>", 180);
                ConUtils.WriteCookie("brower_ie6", "ie6");
            }
            lastlogintime = TypeParse.StrToDateTime(Users.LastLoginTime(userid)).ToString("yyyy-MM-dd HH:mm:ss");
            contentcount = DatabaseProvider.GetInstance().ContentCount();
            channelcount = DatabaseProvider.GetInstance().ChannelCount();
            speccount = DatabaseProvider.GetInstance().SpecialCount();
            commentcount = DatabaseProvider.GetInstance().CommentCount();
        }
    }
}