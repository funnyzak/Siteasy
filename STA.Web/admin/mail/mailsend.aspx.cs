using System;
using System.Data;
using System.Web;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Data;
using STA.Core;
using System.Threading;

namespace STA.Web.Admin
{
    public partial class mailsend : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            cblGlist.AddTableData(Mails.GetSubmailGroups(), "forgroup", "forgroup");
            MailogInfo info = Mails.GetMailog(STARequest.GetInt("id", 0));
            if (info != null)
            {
                txtTitle.Text = info.Title;
                txtUsers.Text = info.Mails;
                cblGlist.SetSelectByID(info.Rgroup);
                txtCont.Text = info.Content;
            }
        }

        private void Send_Click(object sender, EventArgs e)
        {
            if (cblGlist.GetSelectString() == "")
            {
                Message("请选择接收邮件的分组！");
                return;
            }

            int count = 0;
            int percount = 5; //每多少记录为一次等待
            int successcount = 0;
            DataTable dt = Mails.GetSubMailList("name,mail,forgroup,safecode,status");
            Thread[] lThreads = new Thread[dt.Rows.Count];

            foreach (DataRow dr in dt.Rows)
            {
                if (Utils.InArray(dr["forgroup"].ToString().Trim(), cblGlist.GetSelectString()) && dr["status"].ToString() == "1")
                {
                    string body = txtCont.Text, title = txtTitle.Text;
                    body = body.Replace("{unsubscribeurl}", config.Weburl + "/unsubscribe.aspx?m=" + Utils.UrlEncode(dr["mail"].ToString()) + "&s=" + Utils.UrlEncode(dr["safecode"].ToString()));
                    body = body.Replace("{receiver}", dr["name"].ToString());

                    title = title.Replace("{receiver}", dr["name"].ToString());

                    EmailMultiThread emt = new EmailMultiThread(dr["name"].ToString(), dr["mail"].ToString(), title, body);
                    lThreads[count] = new Thread(new ThreadStart(emt.Send));
                    lThreads[count].Start();

                    if (count >= percount)
                    {
                        Thread.Sleep(5000);
                        count = 0;
                    }
                    count++;
                    successcount++;
                }
            }

            if (successcount > 0)
            {
                MailogInfo info = new MailogInfo();
                info.Title = txtTitle.Text;
                info.Rgroup = cblGlist.GetSelectString();
                info.Mails = txtUsers.Text;
                info.Content = txtCont.Text;
                info.Userid = userid;
                info.Username = username;
                Mails.AddMailog(info);

                ConUtils.InsertLog(2, userid, username, admingroupid, admingroupname, STARequest.GetIP(), "订阅邮件发送", string.Format("主题:{0}", txtTitle.Text));
                Message(string.Format("共有 <b>{0}</b> 个订阅邮件接收者,已全部发送完毕！", successcount), 2);
            }
            else
            {
                Message("您选择接收邮件组未能查找到相应的订阅用户,因此邮件无法发送");
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