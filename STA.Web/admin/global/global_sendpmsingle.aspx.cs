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
    public partial class sendpmsingle : AdminPage
    {
        public string groupidlist = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            string uids = STARequest.GetQueryString("uid");
            if (uids.Trim() != "" && Utils.IsNumericList(uids))
            {
                foreach (DataRow username in DatabaseProvider.GetInstance().GetUserListByUids("username", uids).Rows)
                {
                    if (msgto.Text.IndexOf(username["username"].ToString().Trim()) < 0)
                        msgto.Text += username["username"].ToString().Trim() + ",";
                }
            }
            if (STARequest.GetString("username") != "")
            {
                msgto.Text += STARequest.GetString("username");
            }
        }

        private void Send_Click(object sender, EventArgs e)
        {
            DataTable dt = Users.GetUserListByUsernames("id,username", msgto.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PrivateMessageInfo pm = new PrivateMessageInfo();
                    pm.Msgfrom = username;
                    pm.Msgfromid = userid;
                    pm.Msgto = dr["username"].ToString().Replace("'", "''");
                    pm.Msgtoid = TypeParse.StrToInt(dr["id"]);
                    pm.Folder = Folder.收件;
                    pm.Subject = subject.Text;
                    pm.Content = message.Text;
                    pm.New = 1;//标记为未读
                    STA.Core.PrivateMessages.CreatePrivateMessage(pm, 0);
                }
                InsertLog(2, "单发短消息", string.Format("接收人：{0}", msgto.Text.TrimEnd(',')));
                msgto.Text = subject.Text = message.Text = "";
                Message(dt.Rows.Count > 0 ? string.Format("共发送了 {0} 条短信！", dt.Rows.Count) : "发送失败！");
            }
            else
            {
                Message("没有找到有效的接收人！");
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