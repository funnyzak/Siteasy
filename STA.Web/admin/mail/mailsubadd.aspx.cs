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
    public partial class mailsubadd : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            hidAction.Value = "add";
            grouplist.InnerText = ConUtils.DataTableToString(Mails.GetSubmailGroups(), 0, ",");
            if (!(STARequest.GetQueryString("action").Equals("edit") && STARequest.GetString("mail") != "")) return;
            LoadData(STARequest.GetString("mail"));
        }

        private void LoadData(string mail)
        {
            #region 加载页面数据
            MailsubInfo info = Mails.GetSubmail(mail.Trim());
            if (info == null) return;
            txtName.Text = info.Name;
            txtEmail.Text = info.Mail;
            txtEmail.Enabled = false;
            rblStatus.SelectedValue = ((int)info.Status).ToString();
            txtSafecode.Text = info.Status == 0 ? "" : info.Safecode;
            txtForgroup.Text = info.Forgroup;
            rblStatus.SelectedValue = ((int)info.Status).ToString();
            hidId.Value = info.Id.ToString();
            hidAction.Value = "edit";
            #endregion
        }
        private MailsubInfo CreateInfo()
        {
            MailsubInfo info = new MailsubInfo();
            if (hidAction.Value == "edit")
                info = Mails.GetSubmail(STARequest.GetString("mail"));
            info.Name = txtName.Text;
            info.Forgroup = txtForgroup.Text;
            info.Safecode = txtSafecode.Text.Trim();
            info.Mail = txtEmail.Text;
            info.Status = Byte.Parse(TypeParse.StrToInt(rblStatus.SelectedValue, 1).ToString());
            return info;
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            MailsubInfo info = CreateInfo();
            if (hidAction.Value == "add")
            {
                string msg = Mails.AddSubmail(info);
                if (msg != "")
                {
                    Message(msg);
                    return;
                }
                else
                {
                    if (info.Status == 1)
                        Emails.STASmtpSubcribeMail(info);
                }
            }
            else
            {
                Mails.EditSubmail(info);
            }

            InsertLog(2, (info.Id == 0 ? "添加" : "修改") + "邮件订阅", string.Format("订阅人:{0},邮件地址:{1}", info.Name, info.Mail));
            Redirect("mailsubcribe.aspx?msg=" + string.Format("<b>{0}</b> 的邮件订阅已成功{1}！", info.Name, info.Id == 0 ? "创建" : "修改"));
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