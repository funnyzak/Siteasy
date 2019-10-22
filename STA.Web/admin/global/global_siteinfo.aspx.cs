using System;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class siteinfo : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            txtWebName.Text = info.Webname;
            txtWebTitle.Text = info.Webtitle;
            txtWebUrl.Text = info.Weburl;
            txtDescription.Text = info.Description;
            txtKeywords.Text = info.Keywords;
            txtExtCode.Text = info.Extcode;
            txtIcp.Text = info.Icp;
            txtAdminmail.Text = info.Adminmail;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            info.Webname = txtWebName.Text;
            info.Webtitle = txtWebTitle.Text;
            info.Weburl = txtWebUrl.Text;
            info.Description = txtDescription.Text;
            info.Keywords = txtKeywords.Text;
            info.Extcode = txtExtCode.Text;
            info.Icp = txtIcp.Text;
            info.Adminmail = txtAdminmail.Text;
            ConUtils.InsertLog(2, this.userid, this.username, this.admingroupid, this.admingroupname, STARequest.GetIP(), "站点信息", "");

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