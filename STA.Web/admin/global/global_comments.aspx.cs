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
    public partial class comments : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = STARequest.GetString("hidAction");
            if (IsPostBack && action != string.Empty)
            {
                switch (action)
                {
                    case "delcomment":
                        InsertLog(2, "删除评论", "评论ID：" + STARequest.GetString("hidValue"));
                        Del(STARequest.GetFormInt("hidValue", 0));
                        Message();
                        break;
                }
                hidAction.Value = "";
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
            }
            if (!IsPostBack)
            {
                txtContitle.Text = Utils.UrlDecode(STARequest.GetQueryString("contitle"));

                ddlCtype.SelectedValue = STARequest.GetQueryString("ctype");
                ViewState["condition"] = Contents.GetCommentSearchCondition(-1, TypeParse.StrToInt(ddlCtype.SelectedValue), 0, "", "", "", "", txtContitle.Text, "");

                txtPageSize.Text = managelistcount.ToString();
                txtStartDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                txtEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LoadData(1);
            }

            this.pGuide.OnPageChange += delegate(object send, int pageIndex) { LoadData(pageIndex); };
        }

        private void Del(int id)
        {
            if (id <= 0) return;
            int ctype = STARequest.GetFormInt("ctype" + id.ToString(), 1);
            Contents.DelComment(id, ctype);
            InsertLog(2, "删除评论", "评论ID：" + id.ToString());
        }
        private void LoadData(int pageIndex)
        {
            int pageCount, recordCount;
            DataTable dt = Contents.GetCommentDataPage(pageIndex, TypeParse.StrToInt(ViewState["pagesize"], managelistcount), TypeParse.ObjToString(ViewState["condition"]), out pageCount, out recordCount);

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

        private void VerifyOk_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    Contents.CommentStatus(id, CommentStatus.通过);
                    InsertLog(2, "审核通过评论", "评论ID：" + d);
                }
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message();
            }
        }


        private void VerifyNo_Click(object sender, EventArgs e)
        {
            if (STARequest.GetString("cbid") != "")
            {
                string ids = STARequest.GetString("cbid");
                string[] idlist = ids.Split(',');
                foreach (string d in idlist)
                {
                    int id = int.Parse(d);
                    if (id == 0) continue;
                    Contents.CommentStatus(id, CommentStatus.屏蔽);
                    InsertLog(2, "审核屏蔽评论", "评论ID：" + d);
                }
                LoadData(TypeParse.ObjectToInt(ViewState["pageIndex"], 1));
                Message();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchcondition = Contents.GetCommentSearchCondition(TypeParse.StrToInt(ddlStatus.SelectedValue, -1), TypeParse.StrToInt(ddlCtype.SelectedValue, 0), 0, txtUsername.Text, txtIp.Text, txtStartDate.Text, txtEndDate.Text, txtContitle.Text, txtKeywords.Text);
            ViewState["condition"] = searchcondition;
            ViewState["pagesize"] = txtPageSize.Text;
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
            this.VerifyOk.Click += new EventHandler(this.VerifyOk_Click);
            this.VerifyNo.Click += new EventHandler(this.VerifyNo_Click);
            this.btnSearch.Click += new EventHandler(this.BtnSearch_Click);
        }
        #endregion
    }
}