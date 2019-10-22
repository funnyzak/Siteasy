using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

using STA.Common;
using STA.Entity;
using STA.Config;
using STA.Core;
using STA.Data;

namespace STA.Web.Admin
{
    public partial class mailsubcribe : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                int id = STARequest.GetFormInt("hidValue", 0);
                switch (action)
                {
                    case "delsubmail":
                        Del(id);
                        Message();
                        break;
                }
                hidAction.Value = "";
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            if (!IsPostBack)
            {
                RedirectMessage();
                txtStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ddlForgroup.AddTableData(Mails.GetSubmailGroups(), "forgroup", "forgroup", "全部,");
                LoadData(1);
            }
            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { LoadData(pageIndex); };
        }

        void Del(int id)
        {
            string mail = STARequest.GetFormString("mail" + id.ToString());
            Mails.DelSubmail(mail);
            InsertLog(2, "删除邮件订阅", string.Format("订阅人:{0},邮件地址:{1}", STARequest.GetFormString("name" + id.ToString()), mail));
        }

        private void LoadData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Mails.GetSubmailDataPage("*", pageIndex, managelistcount, TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);
            pGuide.PageCount = pageCount;
            pGuide.RecordCount = recordCount;
            ViewState["pageIndex"] = pGuide.PageIndex = pageIndex;
            rptData.Visible = true;
            rptData.DataSource = dt;
            rptData.DataBind();
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    Del(id);
                }
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message();
            }
        }

        void OutBtn_Click(object sender, EventArgs e)
        {
            DataTable list = Mails.GetSubMailList("name as 订阅人,addtime as 订阅时间,mail as 邮件地址,ip as 来源IP,forgroup as 分组");
            Utils.CreateExecl(list, "邮件订阅列表", "邮件订阅列表_"+DateTime.Now.ToString("yyyy_MM_dd"));
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ViewState["condition"] = Mails.GetSubmailSearchCondition(TypeParse.StrToInt(ddlStatus.SelectedValue, -1), txtName.Text, txtMail.Text, txtStartDate.Text, txtEndDate.Text, txtIp.Text, ddlForgroup.SelectedValue);
            LoadData(1);
        }

        #region Web 窗体设计器生成的代码
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.DelBtn.Click += new EventHandler(this.DelBtn_Click);
            this.OutBtn.Click += new EventHandler(OutBtn_Click);
            this.btnSearch.Click += new EventHandler(this.BtnSearch_Click);
        }
        #endregion
    }
}