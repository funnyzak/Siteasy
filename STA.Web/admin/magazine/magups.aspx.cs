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
    public partial class magazineup : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MagazineInfo info = Contents.GetMagazine(STARequest.GetInt("id", 0));
            if (info == null)
            {
                Redirect("maglist.aspx");
                return;
            }
            info.Ratio += ",";
            filetext.InnerHtml = "杂志名称：" + GetDescString(info.Name)
                                + "创建时间：" + info.Addtime.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>"
                                + "杂志标识：" + info.Likeid + "<br/>"
                                + "阅读尺寸：" + string.Format("宽：{0}&nbsp;高：{1}", info.Ratio.Split(',')[0], info.Ratio.Split(',')[1]) + "<br/>"
                                + "开放状态：" + (info.Status == 1 ? "正常" : "关闭");
        }

        private string GetDescString(string text)
        {
            return "<span title=\"" + text + "\">" + Utils.GetUnicodeSubString(text, 40, "..") + "</span><br/>";
        }
    }
}