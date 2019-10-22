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
    public partial class voterecords : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int tid = STARequest.GetQueryInt("tid", 0);

                ViewState["condition"] = Votes.GetVoterecordSearchCondition("", "", "", tid, "", "", "");
                txtTopicids.Text = tid.ToString();
                txtPageSize.Text = managelistcount.ToString();
                txtStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LoadData(1);
            }

            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { LoadData(pageIndex); };
        }

        private void LoadData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Votes.GetVoterecordDataPage(pageIndex, TypeParse.StrToInt(ViewState["pagesize"], managelistcount), TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);

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
                    Votes.DelVoterecord(id);
                    InsertLog(2, "删除投票记录", "ID：" + d);
                }
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            ViewState["condition"] = Votes.GetVoterecordSearchCondition(txtStartDate.Text, txtEndDate.Text, txtIp.Text, TypeParse.StrToInt(txtTopicids.Text), txtIdcard.Text, txtPhone.Text, txtKeywords.Text);
            ViewState["pagesize"] = txtPageSize.Text;
            LoadData(1);
        }

        private void EmptyBtn_Click(object sender, EventArgs e)
        {
            Message(Votes.DelVoterecordWhere(TypeParse.ObjToString(ViewState["condition"])) > 0);
            InsertLog(2, "清空符合条件的投票记录", "条件:" + TypeParse.ObjToString(ViewState["condition"]));
            LoadData(1);
        }

        private void EmptyAllBtn_Click(object sender, EventArgs e)
        {
            Message(Votes.DelVoterecordWhere(Votes.GetVoterecordSearchCondition("", "", "", 0, "", "", "")) > 0);
            InsertLog(2, "清空投票记录", "");
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
            this.btnSearch.Click += new EventHandler(this.BtnSearch_Click);
            this.EmptyAllBtn.Click += new EventHandler(this.EmptyAllBtn_Click);
            this.EmptyBtn.Click += new EventHandler(this.EmptyBtn_Click);
        }
        #endregion
    }
}