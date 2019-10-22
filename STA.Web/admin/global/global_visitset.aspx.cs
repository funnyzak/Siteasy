using System;
using System.Collections.Generic;

using STA.Common;
using STA.Config;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class visitset : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            txtAdminipaccess.Text = info.Adminipaccess;
            txtIpaccess.Text = info.Ipaccess;
            txtIpdenyaccess.Text = info.Ipdenyaccess;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            GeneralConfigInfo info = GeneralConfigs.GetConfig();

            info.Adminipaccess = txtAdminipaccess.Text;
            info.Ipdenyaccess = txtIpdenyaccess.Text;
            info.Ipaccess = txtIpaccess.Text;

            ConUtils.InsertLog(2, this.userid, this.username, this.admingroupid, this.admingroupname, STARequest.GetIP(), "网站访问控制", "");

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