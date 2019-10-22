using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Config;
using STA.Data;
using STA.Core.Publish;

namespace STA.Web.Admin
{
    public partial class createpages : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            txtStartDate.Text = DateTime.Now.AddMonths(-12).ToString("yyyy-MM-dd");
            txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            if (config.Dynamiced == 0)
                StaticPublish.pgedt = Contents.GetPublishPageTable(Contents.GetPageSearchCondition(txtStartDate.Text, txtEndDate.Text,
                                                                                                          TypeParse.StrToInt(txtIdmin.Text, 0),
                                                                                                          TypeParse.StrToInt(txtMax.Text, 0), ""));

            Redirect("global_createprogress.aspx?type=page&start=yes");
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