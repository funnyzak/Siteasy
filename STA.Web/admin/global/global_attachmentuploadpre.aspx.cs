using System;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class attachmentuploadpre : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {

            if (!Regex.IsMatch(txtSavepath.Text, @"^(\/[\w\/]+\/)*$") && txtSavepath.Text != "/")
            {
                Message("保存路径填写有误，请填写正确的格式！");
                return;
            }
            Redirect("global_attachmentupload.aspx?" + string.Format("path={0}&over={1}&water={2}&nway={3}&name={4}&desc={5}", Utils.UrlEncode(txtSavepath.Text),
                      rblOver.SelectedValue, rblWater.SelectedValue, rblOrfilename.SelectedValue, Utils.UrlEncode(txtName.Text), Utils.UrlEncode(txtDesc.Text)));
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