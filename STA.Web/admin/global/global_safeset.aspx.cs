using System;
using System.Collections.Generic;

using STA.Common;
using STA.Config;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class safeset : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            cblVcodemods.SetSelectByID(info.Vcodemods);
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            info.Vcodemods = cblVcodemods.GetSelectString();
            ConUtils.InsertLog(2, this.userid, this.username, this.admingroupid, this.admingroupname, STARequest.GetIP(), "安全控制", "");

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