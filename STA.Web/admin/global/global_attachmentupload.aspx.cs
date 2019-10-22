using System;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class attachmentupload : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string over = "忽略";
            string water = "不添加";
            string namefile = "按系统默认";
            string savepath = "按系统默认";
            string filename = "未填写(系统默认)";
            string filedesc = "未填写(系统默认)";
            if (STARequest.GetQueryString("over") == "o1")
                over = "覆盖";
            if (STARequest.GetQueryString("water") != "w0")
                water = "按系统默认";
            if (STARequest.GetQueryString("nway") == "name0")
                namefile = "按原文件名";
            if (STARequest.GetQueryString("path").Length > 0)
                savepath = Utils.UrlDecode(STARequest.GetQueryString("path"));
            if (STARequest.GetQueryString("name") != "")
                filename = Utils.UrlDecode(STARequest.GetQueryString("name"));
            if (STARequest.GetQueryString("desc") != "")
                filedesc = Utils.UrlDecode(STARequest.GetQueryString("desc"));
            filetext.InnerHtml = "相同文件覆盖：" + over + "<br/>"
                                + "图片水印添加：" + water + "<br/>"
                                + "文件命名：" + namefile + "<br/>"
                                + "文件路径：" + GetDescString(savepath)
                                + "文件名称：" + GetDescString(filename)
                                + "文件描述：" + GetDescString(filedesc);
        }

        private string GetDescString(string text)
        {
            return "<span title=\"" + text + "\">" + Utils.GetUnicodeSubString(text, 40, "..") + "</span><br/>";
        }
    }
}