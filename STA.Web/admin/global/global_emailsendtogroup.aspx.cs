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
    public partial class emailsendtogroup : AdminPage
    {
        public string groupidlist = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            cblGlist.AddTableData(Users.GetUserGroupTable(), "name", "id");
        }


        private void Send_Click(object sender, EventArgs e)
        {
            if (subject.Text == "" || message.Text == "")
            {
                Message("请设置邮件内容！");
                return;
            }
            if (cblGlist.GetSelectString() == "")
            {
                Message("请选择接收邮件的用户组！");
                return;
            }
            groupidlist = cblGlist.GetSelectString();
            ClientScript.RegisterStartupScript(this.GetType(), "Page", "<script>SendEmail_Call();</script>");
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Send.Click += new EventHandler(this.Send_Click);
        }
        #endregion
    }
}