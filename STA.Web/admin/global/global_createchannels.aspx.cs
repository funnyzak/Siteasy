using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Config;
using STA.Data;
using STA.Core.Publish;

namespace STA.Web.Admin
{
    public partial class createchannels : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            lbChannels.BuildTree(Contents.GetChannelDataTable(), "name", "id");
            lbChannels.SetSelectByID(STARequest.GetQueryString("chlid"));
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            if (config.Dynamiced == 0)
                StaticPublish.chldt = Contents.GetPublishChannelTable(lbChannels.GetSelectString());
            Redirect("global_createprogress.aspx?type=channel&start=yes");
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