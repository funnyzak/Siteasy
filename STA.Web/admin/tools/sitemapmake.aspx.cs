using System;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Core.Publish;

namespace STA.Web.Admin.Tools
{
    public partial class sitemapmake : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            SitemapCreate.Create(ddlFrequency.SelectedValue, ddlPriority.SelectedValue, TypeParse.StrToInt(txtNewcount.Text, 1000));
            Message(string.Format("Sitemap文件已成功生成！<a href='{0}/sitemap.xml' target='_blank' class='red'>查看地图</a> ", config.Weburl + sitepath), 30);
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
        }
        #endregion
    }
}