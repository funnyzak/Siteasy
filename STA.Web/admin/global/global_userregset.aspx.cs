using System;
using System.Collections.Generic;

using STA.Common;
using STA.Config;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class userregset : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            rblOpenreg.SelectedValue = config.Openreg.ToString();
            rblEmailmultuser.SelectedValue = config.Emailmultuser.ToString();
            txtForbiduserwords.Text = config.Forbiduserwords;
            rblUserverifyway.SelectedValue = config.Userverifyway.ToString();
            txtUserverifyemailcontent.Text = config.Userverifyemailcontent;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            info.Openreg = TypeParse.StrToInt(rblOpenreg.SelectedValue, 1);
            info.Emailmultuser = TypeParse.StrToInt(rblEmailmultuser.SelectedValue, 0);
            info.Forbiduserwords = txtForbiduserwords.Text;
            info.Userverifyway = TypeParse.StrToInt(rblUserverifyway.SelectedValue, 2);
            info.Userverifyemailcontent = txtUserverifyemailcontent.Text;

            ConUtils.InsertLog(2, this.userid, this.username, this.admingroupid, this.admingroupname, STARequest.GetIP(), "用户注册设置", "");
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