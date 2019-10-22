using System;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Core.Publish;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class createprogress : AdminPage
    {
        string typename = Utils.UrlDecode(STARequest.GetString("type"));
        public string ptitle = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            switch (typename)
            {
                case "rss": ptitle = "RSS文件生成"; break;
                case "channel": ptitle = "频道发布"; break;
                case "special": ptitle = "专题发布"; break;
                case "content": ptitle = "文档发布"; break;
                case "page": ptitle = "单页发布"; break;
                case "sitemap": ptitle = "站点地图发布"; break;
                case "onekey": ptitle = "全站一键发布"; break;
                default: ptitle = "首页发布"; break;
            }
        }
    }
}