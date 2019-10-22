using System;
using System.Data;
using System.Web;
using System.Text.RegularExpressions;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin.Tools
{
    public partial class createtag : AdminPage
    {
        string tagname = Utils.UrlDecode(STARequest.GetString("name"));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            txtName.Text = tagname;
            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetInt("id", 0) > 0)) return;
            hidAction.Value = "edit";
            hidId.Value = STARequest.GetInt("id", 0).ToString();
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                bool success = false;
                if (hidAction.Value == "add")
                    success = Contents.AddTag(txtName.Text) > 0;
                else
                    success = Contents.EditTag(TypeParse.StrToInt(hidId.Value, 0), txtName.Text);
                if (!success)
                    base.RegisterStartupScript("close", "parent.$('#editbox').jqmHide();parent.SAlert(\"标签已存在，已取消操作！\")");
                else
                    base.RegisterStartupScript("close", "parent.$('#editbox').jqmHide();parent.SAlert(\"操作已成功执行！\", 1, function(){parent.SubmitForm('flush')})"); ;
            }
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            isShowSysMenu = false;
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
        }
        #endregion
    }
}