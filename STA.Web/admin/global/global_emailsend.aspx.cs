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
    public partial class emailsend : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            string uids = STARequest.GetQueryString("uid");
            if (uids.Trim() != "" && Utils.IsNumericList(uids))
            {
                foreach (DataRow umail in DatabaseProvider.GetInstance().GetUserListByUids("email",uids).Rows)
                {
                    if (txtUsers.Text.IndexOf(umail["email"].ToString().Trim()) < 0)
                        txtUsers.Text += umail["email"].ToString().Trim() + ",";
                }
            }
            if (STARequest.GetString("email") != "")
            {
                txtUsers.Text += STARequest.GetString("email");
            }
        }


        private void Send_Click(object sender, EventArgs e)
        {
            if (!(txtUsers.Text == "" || txtTitle.Text == "" || txtContent.Text == ""))
            {
                string maillist = Utils.GetEmailList(txtUsers.Text);
                Emails.SendMail(maillist, txtTitle.Text, txtContent.Text);
                ConUtils.InsertLog(2, userid, username, admingroupid, admingroupname, STARequest.GetIP(), "邮件群发", string.Format("主题:{0},邮件:{1}", txtTitle.Text, maillist));
                Message("邮件已经发送完毕！", 3);
            }
            else
            {
                Message("您的填写不完整，请填写完整再发送！");
            }
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