using System;
using System.Collections.Generic;

using STA.Common;
using STA.Config;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class interactset : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            rblCommentverify.SelectedValue = info.Commentverify.ToString();
            txtCommentinterval.Text = info.Commentinterval.ToString();
            txtCommentlength.Text = info.Commentlength.ToString();
            txtCommentfloor.Text = info.Commentfloor.ToString();
            rblCommentlogin.SelectedValue = info.Commentlogin.ToString();
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            GeneralConfigInfo info = GeneralConfigs.GetConfig();
            info.Commentverify = TypeParse.StrToInt(rblCommentverify.SelectedValue, 1);
            info.Commentinterval = TypeParse.StrToInt(txtCommentinterval.Text, 0);
            info.Commentlength = TypeParse.StrToInt(txtCommentlength.Text, 3000);
            info.Commentfloor = TypeParse.StrToInt(txtCommentfloor.Text, 0);
            info.Commentlogin = TypeParse.StrToInt(rblCommentlogin.SelectedValue, 0);
            ConUtils.InsertLog(2, this.userid, this.username, this.admingroupid, this.admingroupname, STARequest.GetIP(), "互动设置", "");
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