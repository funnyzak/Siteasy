using System;
using System.Web;
using System.Data;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;
using System.Text.RegularExpressions;

namespace STA.Web.Admin.Tools
{
    public partial class voteoptionmuladd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            MatchCollection channels = Regex.Matches(txtItems.Text, @"(([^\(\)\r\n]+)(\((\w+)\))?)");
            foreach (Match item in channels)
            {
                if (item.Groups[0].Value == "") continue;
                VoteoptionInfo vpinfo = new VoteoptionInfo();
                vpinfo.Name = item.Groups[2].Value;
                vpinfo.Count = TypeParse.StrToInt(item.Groups[4].Value);
                vpinfo.Topicid = STARequest.GetQueryInt("id", 0);
                Votes.AddVoteoption(vpinfo);
            }
            InsertLog(2, "批量添加投票选项", txtItems.Text);
            FlushData();
        }

        private void FlushData()
        {
            base.RegisterStartupScript("cgscript", "window.parent.SubmitForm('flush');");
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            isShowSysMenu = false;
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
        }
        #endregion
    }
}