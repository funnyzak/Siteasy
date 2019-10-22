using System;
using System.Collections.Generic;

using STA.Common;
using STA.Config;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class optimizeset : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            txtCacheinterval.Text = info.Cacheinterval.ToString();
            rblContentpage.SelectedValue = info.Contentpage.ToString();
            rblUpdateclick.SelectedValue = info.Updateclick.ToString();
            rblOpensearch.SelectedValue = info.Opensearch.ToString();
            txtReflushinterval.Text = info.Reflushinterval.ToString();
            txtSearchcachetime.Text = info.Searchcachetime.ToString();
            txtSearchinterval.Text = info.Searchinterval.ToString();
            txtForbidswords.Text = info.Forbidswords;
            rblHtmlcompress.SelectedValue = info.Htmlcompress.ToString();
            rblOpencomment.SelectedValue = info.Opencomment.ToString();
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            GeneralConfigInfo info = GeneralConfigs.GetConfig();

            info.Cacheinterval = TypeParse.StrToInt(txtCacheinterval.Text, 1440);
            info.Contentpage = TypeParse.StrToInt(rblContentpage.SelectedValue, 1);
            info.Searchcachetime = TypeParse.StrToInt(txtSearchcachetime.Text, 30);
            if (info.Searchcachetime < 10) info.Searchcachetime = 10;
            info.Searchinterval = TypeParse.StrToInt(txtSearchinterval.Text, 0);
            if (info.Searchinterval < 0) info.Searchinterval = 0;
            info.Updateclick = TypeParse.StrToInt(rblUpdateclick.SelectedValue, 1);
            info.Opensearch = TypeParse.StrToInt(rblOpensearch.SelectedValue, 1);
            info.Htmlcompress = TypeParse.StrToInt(rblHtmlcompress.SelectedValue, 0);
            info.Reflushinterval = TypeParse.StrToInt(txtReflushinterval.Text, 0);
            info.Forbidswords = txtForbidswords.Text;
            info.Opencomment = TypeParse.StrToInt(rblOpencomment.Text, 1);

            ConUtils.InsertLog(2, this.userid, this.username, this.admingroupid, this.admingroupname, STARequest.GetIP(), "性能优化", "");
            Globals.CreateJsConfigFile("", info);

            Message(GeneralConfigs.SaveConfig(info));
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