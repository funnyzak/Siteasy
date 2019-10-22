using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class pluginzip : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }


        private void SaveInfo_Click(object sender, EventArgs e)
        {
            PluginInfo info = null;
            string ret = Globals.UploadPluginZip(config, "zipfile", userid, username, ref info);
            if (info != null && ret == "")
                Message(string.Format("扩展安装{0}！", Globals.InstallPlugin(info, rblOver.SelectedValue == "1") ? "成功" : "失败"));
            else
                Message(ret);
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