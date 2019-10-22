using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Config;
using STA.Data;
using STA.Core.Publish;

namespace STA.Web.Admin
{
    public partial class createcontents : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            ddrChannels.BuildTree(Contents.GetChannelDataTable(), "name", "id");
            ddrChannels.SelectedValue = STARequest.GetQueryInt("id", 0).ToString();
            txtStartDate.Text = DateTime.Now.AddMonths(-2).ToString("yyyy-MM-dd");
            txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            if (config.Dynamiced == 0)
                StaticPublish.condt = Contents.GetPublishContentTable(Contents.GetContentPublishCondition(txtStartDate.Text, txtEndDate.Text,
                                                                                                          TypeParse.StrToInt(txtIdmin.Text, 0),
                                                                                                          TypeParse.StrToInt(txtMax.Text, 0),
                                                                                                          TypeParse.StrToInt(ddrChannels.SelectedValue, 0)));

            Redirect("global_createprogress.aspx?type=content&start=yes");
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