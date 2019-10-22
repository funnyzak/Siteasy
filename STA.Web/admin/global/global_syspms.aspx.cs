using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;
using STA.Cache;

namespace STA.Web.Admin
{
    public partial class syspms : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (!IsPostBack)
            {
                BindData(1);
            }
            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { BindData(pageIndex); };
        }

        private void BindData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = STA.Data.PrivateMessages.GetAnnouncePrivateMessageDataPage("", pageIndex, managelistcount, out pageCount, out recordCount);
            pGuide.PageCount = pageCount;
            pGuide.RecordCount = recordCount;
            ViewState["pageIndex"] = pGuide.PageIndex = pageIndex;
            rptData.Visible = true;
            rptData.DataSource = dt;
            rptData.DataBind();
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            STA.Core.PrivateMessages.DelPrivateMessages(0, STARequest.GetString("cbid").Split(','));
            InsertLog(2, "删除系统消息", "删除了" + STARequest.GetString("cbid").Split(',').Length + "条");
            Message();
            BindData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
        }

        void BtnSend_Click(object sender, EventArgs e)
        {
            PrivateMessageInfo info = new PrivateMessageInfo();
            if (txtSubject.Text == "" || txtContent.Text == "")
            {
                Message("标题或内容不能为空");
                return;
            }
            info.Subject = txtSubject.Text;
            info.Content = txtContent.Text;
            STA.Data.PrivateMessages.AddPrivateMessage(info);
            txtContent.Text = txtSubject.Text = "";
            InsertLog(2, "发送系统消息", "标题:" + info.Subject);
            BindData(1);
            STACache.GetCacheService().RemoveObject("/STA/AnnouncePrivateMessageCount");
            Message("系统消息已发送");
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.BtnDel.Click += new EventHandler(this.BtnDel_Click);
            this.BtnSend.Click += new EventHandler(BtnSend_Click);
        }
        #endregion
    }
}