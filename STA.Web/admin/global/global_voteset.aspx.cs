using System;
using System.Collections.Generic;

using STA.Common;
using STA.Config;
using STA.Core;

namespace STA.Web.Admin
{
    public partial class voteset : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            VoteConfigInfo info = VoteConfigs.GetConfig();
            rblVcode.SelectedValue = info.Vcode.ToString();
            rblVoteonlymember.SelectedValue = info.Login.ToString();
            rblVotephoneverify.SelectedValue = info.Phoneverify.ToString();
            txtVotetimeinterval.Text = info.Timeinterval.ToString();
            txtVotestime.Text = info.Timeslot.Split('|')[0];
            txtVoteetime.Text = info.Timeslot.Split('|')[1];
            rblVoteinfoinput.SelectedValue = info.Infoinput.ToString();
            txtVoteinfos.Text = info.Infofields;
            txtVoteforbidips.Text = info.Forbidips;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            VoteConfigInfo info = new VoteConfigInfo();
            info.Vcode = TypeParse.StrToInt(rblVcode.SelectedValue, 0);
            info.Login = TypeParse.StrToInt(rblVoteonlymember.SelectedValue, 0);
            info.Phoneverify = TypeParse.StrToInt(rblVotephoneverify.SelectedValue, 0);
            info.Timeinterval = TypeParse.StrToInt(txtVotetimeinterval.Text, 1440);
            info.Timeslot = txtVotestime.Text + "|" + txtVoteetime.Text;
            info.Infoinput = TypeParse.StrToInt(rblVoteinfoinput.SelectedValue, 0);
            info.Infofields = txtVoteinfos.Text;
            info.Forbidips = txtVoteforbidips.Text;

            ConUtils.InsertLog(2, this.userid, this.username, this.admingroupid, this.admingroupname, STARequest.GetIP(), "投票设置", "");

            Message(VoteConfigs.SaveConfig(info));
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